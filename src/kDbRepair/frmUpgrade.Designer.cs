namespace kDbRepair
{
    partial class frmUpgrade
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpgrade));
            this.cbAuthType = new System.Windows.Forms.ComboBox();
            this.btnUpgrade = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBarUpgrade = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.picError = new System.Windows.Forms.PictureBox();
            this.picOk = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOk)).BeginInit();
            this.SuspendLayout();
            // 
            // cbAuthType
            // 
            this.cbAuthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthType.FormattingEnabled = true;
            this.cbAuthType.Location = new System.Drawing.Point(236, 31);
            this.cbAuthType.Name = "cbAuthType";
            this.cbAuthType.Size = new System.Drawing.Size(214, 27);
            this.cbAuthType.TabIndex = 2;
            this.cbAuthType.SelectedIndexChanged += new System.EventHandler(this.cbAuthType_SelectedIndexChanged);
            // 
            // btnUpgrade
            // 
            this.btnUpgrade.Location = new System.Drawing.Point(16, 232);
            this.btnUpgrade.Name = "btnUpgrade";
            this.btnUpgrade.Size = new System.Drawing.Size(434, 55);
            this.btnUpgrade.TabIndex = 6;
            this.btnUpgrade.Text = "Upgrade";
            this.btnUpgrade.UseVisualStyleBackColor = true;
            this.btnUpgrade.Click += new System.EventHandler(this.btnUpgrade_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server:";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(236, 92);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(214, 27);
            this.txtUserId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Auth Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "User Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(236, 153);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(214, 27);
            this.txtPassword.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Database:";
            // 
            // progressBarUpgrade
            // 
            this.progressBarUpgrade.Location = new System.Drawing.Point(16, 203);
            this.progressBarUpgrade.Name = "progressBarUpgrade";
            this.progressBarUpgrade.Size = new System.Drawing.Size(434, 23);
            this.progressBarUpgrade.TabIndex = 12;
            this.progressBarUpgrade.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Database|*.mdb";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(15, 153);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(182, 27);
            this.btnSelectFile.TabIndex = 5;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "Migration File:";
            // 
            // cbServer
            // 
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(16, 31);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(214, 27);
            this.cbServer.TabIndex = 0;
            this.cbServer.SelectedIndexChanged += new System.EventHandler(this.cbServer_SelectedIndexChanged);
            // 
            // cbDatabase
            // 
            this.cbDatabase.FormattingEnabled = true;
            this.cbDatabase.Location = new System.Drawing.Point(16, 92);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(214, 27);
            this.cbDatabase.TabIndex = 1;
            this.cbDatabase.SelectedIndexChanged += new System.EventHandler(this.cbDatabase_SelectedIndexChanged);
            // 
            // picError
            // 
            this.picError.Image = global::kDbRepair.Properties.Resources.red_cross;
            this.picError.Location = new System.Drawing.Point(203, 153);
            this.picError.Name = "picError";
            this.picError.Size = new System.Drawing.Size(27, 27);
            this.picError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picError.TabIndex = 16;
            this.picError.TabStop = false;
            this.picError.Visible = false;
            // 
            // picOk
            // 
            this.picOk.Image = global::kDbRepair.Properties.Resources.green_tick;
            this.picOk.Location = new System.Drawing.Point(203, 153);
            this.picOk.Name = "picOk";
            this.picOk.Size = new System.Drawing.Size(27, 27);
            this.picOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOk.TabIndex = 15;
            this.picOk.TabStop = false;
            this.picOk.Visible = false;
            // 
            // frmUpgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 299);
            this.Controls.Add(this.picError);
            this.Controls.Add(this.picOk);
            this.Controls.Add(this.cbDatabase);
            this.Controls.Add(this.cbServer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.progressBarUpgrade);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpgrade);
            this.Controls.Add(this.cbAuthType);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmUpgrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upgrade Database";
            this.Load += new System.EventHandler(this.frmUpgrade_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbAuthType;
        private System.Windows.Forms.Button btnUpgrade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBarUpgrade;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.ComboBox cbDatabase;
        private System.Windows.Forms.PictureBox picOk;
        private System.Windows.Forms.PictureBox picError;
    }
}

