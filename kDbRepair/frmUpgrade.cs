using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dbModels;
using System.Data.SqlClient;
using SQLSVN;
using System.Data.OleDb;

namespace kDbRepair
{
    public partial class frmUpgrade : Form
    {
        public int projectId { get; set; }
        public string migrationFilePath { get; set; }
        public frmUpgrade()
        {
            InitializeComponent();
        }

        private void frmNewProject_Load(object sender, EventArgs e)
        {
            cbAuthType.DataSource = new List<tAuthType>()
            {
                new tAuthType()
                {
                    Id = 1,
                    Text = "Windows Authentication"
                },
                new tAuthType()
                {
                    Id = 2,
                    Text = "SQL Server Authentication"
                }
            };
            cbAuthType.DisplayMember = "Text";
            cbAuthType.ValueMember = "Id";
        }

        private void cbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tAuthType item = (tAuthType)cbAuthType.SelectedItem;
            if (item.Id == 1)
            {
                txtUserId.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUserId.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            tAuthType item = (tAuthType)cbAuthType.SelectedItem;
            string server = txtServer.Text.Trim();
            string userId = txtUserId.Text.Trim();
            string password = txtPassword.Text.Trim();
            string dbName = txtDatabase.Text.Trim();
            byte authType = item.Id;
            if (string.IsNullOrEmpty(migrationFilePath))
            {
                MessageBox.Show("please select migration file");
                return;
            }
            if (string.IsNullOrEmpty(server))
            {
                MessageBox.Show("enter server name");
                return;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("enter database name");
                return;
            }
            try
            {
                OleDbConnection connection = dbFactoryStatic.connectionMaker(migrationFilePath);
                dbFactory db = new dbFactory(connection);

                SqlConnection targetConnection = dbFactoryStatic.connectionMaker(txtServer.Text, item.Id, txtUserId.Text, txtPassword.Text, txtDatabase.Text);
                dbFactory dbTarget = new dbFactory(targetConnection);
                dbTarget.initialDb(projectId);
                tSQLSVNVersion version = dbTarget.Get<tSQLSVNVersion>(projectId);         
                
                List<tScript> scripts = db.GetAll<tScript>().Where(x => x.tProjectId == projectId && x.scriptId > version.VerNumber).OrderBy(x => x.scriptId).ToList();
                if (scripts.Count == 0)
                {
                    MessageBox.Show(string.Format("Your database is latest version. version : {0}", version.VerNumber));
                    return;
                }

                dbTarget.connOpen();
                int scriptId = version.VerNumber;
                try
                {
                    progressBarUpgrade.Visible = true;
                    progressBarUpgrade.Minimum = scripts.Min(x => x.scriptId);
                    progressBarUpgrade.Maximum = scripts.Max(x => x.scriptId);
                    progressBarUpgrade.Step = 1;
                    foreach (tScript script in scripts)
                    {
                        scriptId = script.scriptId;
                        progressBarUpgrade.Value = scriptId;
                        dbTarget.beginTrans();
                        foreach (string command in script.sql.Split(new string[] { "GO\r\n", "\r\nGO" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            dbTarget.Execute(command);
                        }
                        version.VerNumber = scriptId;
                        dbTarget.Update<tSQLSVNVersion>(version);
                        dbTarget.commitTrans();
                    }
                    MessageBox.Show(string.Format("Successfully upgraded to version : {0}", scriptId));
                }
                catch (Exception ex)
                {
                    dbTarget.rollbackTrans();
                    string errorMessage = string.Empty;
                    errorMessage += string.Format("Error\nScript Id:{0}\n\n{1}", scriptId, ex.Message);
                    MessageBox.Show(errorMessage);
                }
                dbTarget.connClose();
                progressBarUpgrade.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in upgrade: \n" + ex.Message);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                migrationFilePath = openFileDialog1.FileName;
                OleDbConnection connection = dbFactoryStatic.connectionMaker(migrationFilePath);
                try
                {
                    dbFactory db = new dbFactory(connection);
                    tScript firstQuery = db.GetAll<tScript>().FirstOrDefault();
                    if (firstQuery == null)
                    {
                        MessageBox.Show("File is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    projectId = firstQuery.tProjectId;
                    MessageBox.Show("File is valid");
                }
                catch
                {
                    migrationFilePath = string.Empty;
                    projectId = 0;
                    MessageBox.Show("File is not valid!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
