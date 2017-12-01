using dbModels;
using SQLSVN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kDbMigration
{
    public partial class frmMain : Form
    {
        dbFactory db = new dbFactory();
        int projectId = 0;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (projectId == 0)
            {
                frmSelectProject frmPopup = new frmSelectProject();
                frmPopup.ShowDialog();
                projectId = frmPopup.projectId;
                if (projectId == 0)
                    Application.Exit();
                else
                    bindInfo();
            }
        }

        private void bindInfo()
        {
            tProject project = db.Get<tProject>(projectId);
            bindScripts(projectId);
        }

        private void bindScripts(int projectId)
        {
            int lastId = (db.GetAll<tScript>().Where(x => x.tProjectId == projectId).OrderByDescending(x => x.scriptId).FirstOrDefault()?.scriptId ?? 0);
            txtScriptId.Text = (lastId + 1).ToString();
            txtScript.Text = string.Empty;
        }

        private void btnSaveScript_Click(object sender, EventArgs e)
        {
            string sql = txtScript.Text.Trim();
            int scriptId = txtScriptId.Text.ToType<int>(0);
            tScript script = db.GetAll<tScript>().FirstOrDefault(x => x.tProjectId == projectId && x.scriptId == scriptId);
            if (script is null)
            {
                script = new tScript() { sql = sql, tProjectId = projectId, scriptId = scriptId };
                db.Insert(script);
            }
            else
            {
                script.sql = sql;
                db.Update(script);
            }
            bindScripts(projectId);
        }

        private void txtScriptId_TextChanged(object sender, EventArgs e)
        {
            int scriptId = txtScriptId.Text.ToType<int>(0);
            tScript script = db.GetAll<tScript>().FirstOrDefault(x => x.tProjectId == projectId && x.scriptId == scriptId);
            string sql = script?.sql ?? string.Empty;
            txtScript.Text = sql;
            btnDeleteScript.Enabled = script != null;
        }

        private void txtScriptId_KeyDown(object sender, KeyEventArgs e)
        {
            int scriptId = txtScriptId.Text.ToType<int>(0);
            if (e.KeyCode == Keys.Up)
            {
                scriptId++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                scriptId--;
            }
            if (scriptId < 1)
                scriptId = 1;
            txtScriptId.Text = scriptId.ToString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void changeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectProject frmPopup = new frmSelectProject();
            frmPopup.ShowDialog();
            if (frmPopup.projectId != 0)
            {
                projectId = frmPopup.projectId;
                bindInfo();
            }
        }

        private void btnDeleteScript_Click(object sender, EventArgs e)
        {
            string sql = string.Format("--DELETED SCRIPT");
            int scriptId = txtScriptId.Text.ToType<int>(0);
            tScript script = db.GetAll<tScript>().FirstOrDefault(x => x.tProjectId == projectId && x.scriptId == scriptId);
            if (script != null)
            {
                script.sql = sql;
                db.Update(script);
            }
            bindScripts(projectId);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = saveFileDialog1.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;

                List<string> sqlScripts = new List<string>() { };
                List<tScript> scripts = db.GetAll<tScript>().Where(x => x.tProjectId == projectId).OrderBy(x => x.scriptId).ToList();
                foreach (tScript script in scripts)
                {
                    sqlScripts.Add(string.Format("\nINSERT INTO [tScript] ([scriptId], [sql], [tProjectId]) VALUES({0}, '{1}', {2});", script.scriptId, dbFactoryStatic.escapeQuery(script.sql), script.tProjectId));
                }
                bool exportResult = db.exportFile(sqlScripts, fileName);
                if (exportResult)
                {
                    MessageBox.Show("Exported");
                }
                else
                {
                    MessageBox.Show("Can't Export File!!! There is some error!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
