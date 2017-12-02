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
using SQLSVN;

namespace kDbMigration
{
    public partial class frmSelectProject : Form
    {
        dbFactory db = new dbFactory();
        public Guid projectCode { get; set; }
        public frmSelectProject()
        {
            InitializeComponent();
        }

        private void frmSelectProject_Load(object sender, EventArgs e)
        {
            bindProjects();
        }
        private void bindProjects()
        {
            IEnumerable<tProject> projects = db.GetAll<tProject>().OrderByDescending(x => x.ProjectName).ToList();
            cbProject.DataSource = projects;
            cbProject.ValueMember = "Code";
            cbProject.DisplayMember = "ProjectName";
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            projectCode = cbProject.SelectedValue.ToType<Guid>(Guid.Empty);
            if (projectCode == Guid.Empty)
            {
                MessageBox.Show("please select a project or create a new one");
                return;
            }
            Close();
        }

    }
}
