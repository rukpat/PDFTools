namespace EncryptDecrypt
{
    partial class EnD
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnD));
            listFilesFolders = new ListView();
            columnHeaderNo = new ColumnHeader();
            columnHeaderFilename = new ColumnHeader();
            columnHeaderState = new ColumnHeader();
            columnHeaderComment = new ColumnHeader();
            linkLabelFolderName = new LinkLabel();
            label1 = new Label();
            textBoxPassword = new TextBox();
            groupBoxOverwrite = new GroupBox();
            radioButtonPrefix = new RadioButton();
            textBoxOverwriteString = new TextBox();
            radioButtonSuffix = new RadioButton();
            checkBoxOverwrite = new CheckBox();
            buttonDecrypt = new Button();
            buttonClose = new Button();
            checkBoxRecurseDir = new CheckBox();
            groupBoxOverwrite.SuspendLayout();
            SuspendLayout();
            // 
            // listFilesFolders
            // 
            listFilesFolders.AllowColumnReorder = true;
            listFilesFolders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listFilesFolders.BackColor = Color.FromArgb(40, 40, 40);
            listFilesFolders.BorderStyle = BorderStyle.FixedSingle;
            listFilesFolders.Columns.AddRange(new ColumnHeader[] { columnHeaderNo, columnHeaderFilename, columnHeaderState, columnHeaderComment });
            listFilesFolders.ForeColor = Color.WhiteSmoke;
            listFilesFolders.FullRowSelect = true;
            listFilesFolders.Location = new Point(251, 12);
            listFilesFolders.Name = "listFilesFolders";
            listFilesFolders.ShowItemToolTips = true;
            listFilesFolders.Size = new Size(662, 530);
            listFilesFolders.TabIndex = 1;
            listFilesFolders.UseCompatibleStateImageBehavior = false;
            listFilesFolders.View = View.Details;
            listFilesFolders.ItemActivate += listFilesFolders_ItemActivate;
            // 
            // columnHeaderNo
            // 
            columnHeaderNo.Text = "No";
            // 
            // columnHeaderFilename
            // 
            columnHeaderFilename.Text = "Filename";
            columnHeaderFilename.Width = 200;
            // 
            // columnHeaderState
            // 
            columnHeaderState.Text = "State";
            // 
            // columnHeaderComment
            // 
            columnHeaderComment.Text = "Comment (Double-Click to view)";
            columnHeaderComment.Width = 100;
            // 
            // linkLabelFolderName
            // 
            linkLabelFolderName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkLabelFolderName.AutoSize = true;
            linkLabelFolderName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkLabelFolderName.LinkColor = Color.FromArgb(128, 128, 255);
            linkLabelFolderName.Location = new Point(251, 558);
            linkLabelFolderName.Name = "linkLabelFolderName";
            linkLabelFolderName.Size = new Size(51, 15);
            linkLabelFolderName.TabIndex = 7;
            linkLabelFolderName.TabStop = true;
            linkLabelFolderName.Text = "Filepath";
            linkLabelFolderName.LinkClicked += linkLabelFolderName_LinkClicked;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(788, 558);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 9;
            label1.Text = "Only * .pdf files shown";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxPassword
            // 
            textBoxPassword.AutoCompleteMode = AutoCompleteMode.Append;
            textBoxPassword.BackColor = Color.FromArgb(40, 40, 40);
            textBoxPassword.BorderStyle = BorderStyle.FixedSingle;
            textBoxPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPassword.ForeColor = Color.WhiteSmoke;
            textBoxPassword.Location = new Point(12, 12);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PlaceholderText = "Enter Password";
            textBoxPassword.Size = new Size(225, 27);
            textBoxPassword.TabIndex = 0;
            textBoxPassword.MouseDown += textBoxPassword_MouseDown;
            // 
            // groupBoxOverwrite
            // 
            groupBoxOverwrite.Controls.Add(radioButtonPrefix);
            groupBoxOverwrite.Controls.Add(textBoxOverwriteString);
            groupBoxOverwrite.Controls.Add(radioButtonSuffix);
            groupBoxOverwrite.Controls.Add(checkBoxOverwrite);
            groupBoxOverwrite.FlatStyle = FlatStyle.Popup;
            groupBoxOverwrite.ForeColor = Color.WhiteSmoke;
            groupBoxOverwrite.Location = new Point(12, 115);
            groupBoxOverwrite.Name = "groupBoxOverwrite";
            groupBoxOverwrite.Size = new Size(225, 99);
            groupBoxOverwrite.TabIndex = 10;
            groupBoxOverwrite.TabStop = false;
            groupBoxOverwrite.Text = "     Overwrite        ";
            groupBoxOverwrite.Visible = false;
            // 
            // radioButtonPrefix
            // 
            radioButtonPrefix.AutoSize = true;
            radioButtonPrefix.Checked = true;
            radioButtonPrefix.Location = new Point(17, 28);
            radioButtonPrefix.Name = "radioButtonPrefix";
            radioButtonPrefix.Size = new Size(55, 19);
            radioButtonPrefix.TabIndex = 3;
            radioButtonPrefix.TabStop = true;
            radioButtonPrefix.Text = "Prefix";
            radioButtonPrefix.UseVisualStyleBackColor = true;
            radioButtonPrefix.CheckedChanged += radioButtonPrefix_CheckedChanged;
            // 
            // textBoxOverwriteString
            // 
            textBoxOverwriteString.BackColor = Color.FromArgb(40, 40, 40);
            textBoxOverwriteString.BorderStyle = BorderStyle.FixedSingle;
            textBoxOverwriteString.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxOverwriteString.ForeColor = Color.WhiteSmoke;
            textBoxOverwriteString.Location = new Point(18, 55);
            textBoxOverwriteString.Name = "textBoxOverwriteString";
            textBoxOverwriteString.PlaceholderText = "Enter Prefix";
            textBoxOverwriteString.Size = new Size(191, 27);
            textBoxOverwriteString.TabIndex = 5;
            textBoxOverwriteString.Text = "decrypted_";
            // 
            // radioButtonSuffix
            // 
            radioButtonSuffix.AutoSize = true;
            radioButtonSuffix.Location = new Point(153, 28);
            radioButtonSuffix.Name = "radioButtonSuffix";
            radioButtonSuffix.Size = new Size(55, 19);
            radioButtonSuffix.TabIndex = 4;
            radioButtonSuffix.Text = "Suffix";
            radioButtonSuffix.UseVisualStyleBackColor = true;
            radioButtonSuffix.CheckedChanged += radioButtonSuffix_CheckedChanged;
            // 
            // checkBoxOverwrite
            // 
            checkBoxOverwrite.AutoSize = true;
            checkBoxOverwrite.Checked = true;
            checkBoxOverwrite.CheckState = CheckState.Checked;
            checkBoxOverwrite.Location = new Point(-5, 0);
            checkBoxOverwrite.Name = "checkBoxOverwrite";
            checkBoxOverwrite.RightToLeft = RightToLeft.Yes;
            checkBoxOverwrite.Size = new Size(98, 19);
            checkBoxOverwrite.TabIndex = 2;
            checkBoxOverwrite.Text = "Overwrite File";
            checkBoxOverwrite.UseVisualStyleBackColor = true;
            checkBoxOverwrite.CheckedChanged += checkBoxOverwrite_CheckedChanged;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDecrypt.BackColor = SystemColors.ActiveCaption;
            buttonDecrypt.BackgroundImageLayout = ImageLayout.Zoom;
            buttonDecrypt.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            buttonDecrypt.FlatStyle = FlatStyle.Popup;
            buttonDecrypt.ForeColor = Color.Black;
            buttonDecrypt.Image = Properties.Resources.nS16;
            buttonDecrypt.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDecrypt.Location = new Point(138, 543);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(100, 30);
            buttonDecrypt.TabIndex = 2;
            buttonDecrypt.Text = "&Decrypt";
            buttonDecrypt.UseVisualStyleBackColor = false;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonClose.BackColor = Color.Transparent;
            buttonClose.BackgroundImageLayout = ImageLayout.Zoom;
            buttonClose.FlatAppearance.BorderColor = Color.WhiteSmoke;
            buttonClose.FlatStyle = FlatStyle.Popup;
            buttonClose.ForeColor = Color.WhiteSmoke;
            buttonClose.ImageAlign = ContentAlignment.MiddleLeft;
            buttonClose.Location = new Point(12, 543);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(100, 30);
            buttonClose.TabIndex = 6;
            buttonClose.Text = "&Close";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // checkBoxRecurseDir
            // 
            checkBoxRecurseDir.AutoSize = true;
            checkBoxRecurseDir.Location = new Point(7, 282);
            checkBoxRecurseDir.Name = "checkBoxRecurseDir";
            checkBoxRecurseDir.RightToLeft = RightToLeft.Yes;
            checkBoxRecurseDir.Size = new Size(118, 19);
            checkBoxRecurseDir.TabIndex = 11;
            checkBoxRecurseDir.Text = "Recurse Directory";
            checkBoxRecurseDir.UseVisualStyleBackColor = true;
            checkBoxRecurseDir.CheckedChanged += checkBoxRecurseDir_CheckedChanged;
            // 
            // EnD
            // 
            AcceptButton = buttonDecrypt;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            CancelButton = buttonClose;
            ClientSize = new Size(925, 585);
            Controls.Add(checkBoxRecurseDir);
            Controls.Add(buttonClose);
            Controls.Add(buttonDecrypt);
            Controls.Add(textBoxPassword);
            Controls.Add(label1);
            Controls.Add(linkLabelFolderName);
            Controls.Add(listFilesFolders);
            Controls.Add(groupBoxOverwrite);
            ForeColor = Color.WhiteSmoke;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EnD";
            Text = "PDF Tools - Decrypt";
            FormClosing += EnD_FormClosing;
            Shown += EnD_Shown;
            groupBoxOverwrite.ResumeLayout(false);
            groupBoxOverwrite.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listFilesFolders;
        private ColumnHeader columnHeaderNo;
        private ColumnHeader columnHeaderFilename;
        private ColumnHeader columnHeaderState;
        private ColumnHeader columnHeaderComment;
        private LinkLabel linkLabelFolderName;
        private Label label1;
        private TextBox textBoxPassword;
        private GroupBox groupBoxOverwrite;
        private RadioButton radioButtonPrefix;
        private TextBox textBoxOverwriteString;
        private RadioButton radioButtonSuffix;
        private CheckBox checkBoxOverwrite;
        private Button buttonDecrypt;
        private Button buttonClose;
        private CheckBox checkBoxRecurseDir;
    }
}
