using EncryptDecrypt.Properties;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.Security;

namespace EncryptDecrypt
{
    public partial class EnD : Form
    {
        static readonly string LOGFILEPATH = "EncryptDecrypt.log"; // Specify the log file path
        static string[]? commandlineArgs;
        public EnD(string[] args)
        {
            InitializeComponent();

            // Overwrite the log file with an empty file
            File.WriteAllText(LOGFILEPATH, string.Empty);

            // Restore form size and position
            this.StartPosition = FormStartPosition.Manual;
            this.Width = Settings.Default.FormWidth;
            this.Height = Settings.Default.FormHeight;
            this.Left = Settings.Default.FormLeft;
            this.Top = Settings.Default.FormTop;
            // Restore window state
            if (Enum.TryParse(Properties.Settings.Default.FormWindowState, out FormWindowState windowState))
            {
                this.WindowState = windowState;
            }

            // Ensure the form is within the screen bounds
            /*var screen = Screen.FromControl(this);
            if (this.Left < screen.Bounds.Left || this.Right > screen.Bounds.Right ||
                this.Top < screen.Bounds.Top || this.Bottom > screen.Bounds.Bottom)
            {
                this.Left = screen.Bounds.Left + 100;
                this.Top = screen.Bounds.Top + 100;
            }*/


            //Initialise form controls
            checkBoxOverwrite.Checked = Settings.Default.Overwrite;
            groupBoxOverwrite.Visible = Settings.Default.Overwrite ? false : true;
            textBoxOverwriteString.Text = Settings.Default.OverwritePrefixSuffixName;
            radioButtonPrefix.Checked = Settings.Default.OverwritePrefixSuffix == 'P' ? true : false;
            radioButtonSuffix.Checked = Settings.Default.OverwritePrefixSuffix == 'S' ? true : false;
            textBoxOverwriteString.PlaceholderText = "Enter " + (Settings.Default.OverwritePrefixSuffix == 'P' ? "Prefix" : "Suffix");
            checkBoxRecurseDir.Checked = Settings.Default.RecurseDirectory;
            textBoxPassword.Text = "Enter Password";
            textBoxPassword.Focus();
            textBoxPassword.SelectAll();


            // Set the linkLabelFolderName text to the directory path of the first item, if applicable
            if (args.Length > 0 && args[0].Contains('\\'))
            {
                linkLabelFolderName.Text = args[0].Substring(0, args[0].LastIndexOf('\\'));
                commandlineArgs = args;
            }
        }
        private void EnD_Shown(object sender, EventArgs e)
        {
            // Populate the ListView with the files/folders passed as arguments
            if (commandlineArgs != null)
            {
                // Run the long-running process on a background thread
#if RELEASE
                Task.Run(() => PopulateFilesFoldersList(commandlineArgs));
#elif DEBUG
                PopulateFilesFoldersList(commandlineArgs);
#endif
            }
        }

        private void EnD_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save Form window state
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.FormWidth = this.Width;
                Settings.Default.FormHeight = this.Height;
                Settings.Default.FormLeft = this.Left;
                Settings.Default.FormTop = this.Top;
            }
            else
            {
                Settings.Default.FormWidth = this.RestoreBounds.Width;
                Settings.Default.FormHeight = this.RestoreBounds.Height;
                Settings.Default.FormLeft = this.RestoreBounds.Left;
                Settings.Default.FormTop = this.RestoreBounds.Top;
            }

            Settings.Default.FormWindowState = this.WindowState.ToString();
            // Save the current settings
            Settings.Default.Overwrite = checkBoxOverwrite.Checked;
            Settings.Default.OverwritePrefixSuffix = radioButtonPrefix.Checked ? 'P' : 'S';
            Settings.Default.OverwritePrefixSuffixName = textBoxOverwriteString.Text;
            Settings.Default.RecurseDirectory = checkBoxRecurseDir.Checked;
            Settings.Default.Save();
        }

        private static void LogMessage(string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}"; // Create a formatted log entry

            File.AppendAllText(LOGFILEPATH, logEntry + Environment.NewLine); // Append the log entry to the file

            // Append the log entry to the file
            using (StreamWriter writer = new(LOGFILEPATH, true))
            {
                writer.WriteLine(logEntry);
            }
        }

        private static string GetSecuritySettings(PdfSecuritySettings securitySettings)
        {
            return $"Encryption Level: {securitySettings.DocumentSecurityLevel} | " +
                   $"Print Document: {(securitySettings.PermitPrint ? "Allowed" : "Not Allowed")} | " +
                   $"Quality Print: {(securitySettings.PermitFullQualityPrint ? "Allowed" : "Not Allowed")} | " +
                   $"Assemble Doc: {(securitySettings.PermitAssembleDocument ? "Allowed" : "Not Allowed")} | " +
                   $"Copy Content: {(securitySettings.PermitExtractContent ? "Allowed" : "Not Allowed")} | " +
                   $"Accessibility Copy: {(securitySettings.PermitAccessibilityExtractContent ? "Allowed" : "Not Allowed")} | " +
                   $"Modify Document: {(securitySettings.PermitModifyDocument ? "Allowed" : "Not Allowed")} | " +
                   $"Add Annotations: {(securitySettings.PermitAnnotations ? "Allowed" : "Not Allowed")} | " +
                   $"Fill Forms: {(securitySettings.PermitFormsFill ? "Allowed" : "Not Allowed")}";
        }

        private static (int code, string State, string Comment) GetFileState(string directoryName, string fileName)
        {
            try
            {
                var isEncrypted = true;

                // Opening the document with invalid password to check if it is encrypted at all; if it opens it is not encrypted

                var pdfDocTest1 = PdfReader.Open($"{directoryName}\\{fileName}", "") as PdfDocument;
                isEncrypted = false;

                if (isEncrypted)  // Encrypted
                {
                    return (2, "Password Protected", "The document is password protected.");
                }
                else // else check for security settings for pdf without password
                {
                    // Attempt to open the PDF document
                    var pdfDocTest = PdfReader.Open($"{directoryName}\\{fileName}", PdfDocumentOpenMode.ReadOnly);

                    // Check if the document is encrypted
                    if (pdfDocTest.SecuritySettings.DocumentSecurityLevel == PdfDocumentSecurityLevel.None)
                    {
                        // PDF is not password protected
                        return (0, "No Security", "The document is not encrypted.");
                    }
                    else
                    {
                        // PDF is encrypted, extract security settings
                        var securitySettings = pdfDocTest.SecuritySettings;
                        string settings = GetSecuritySettings(securitySettings);

                        return (1, "Security Setting", settings);
                    }
                }
            }
            catch (PdfReaderException ex)
            {
                if (ex.Message.Contains("password is invalid"))
                {
                    // Incorrect password, or the document is password-protected and cannot be opened without it
                    return (2, "Password Protected", "The document is password protected and cannot be opened.");
                }
                else if (ex.Message.Contains("owner password"))
                {
                    // Attempt to open the PDF document
                    var pdfDocTest = PdfReader.Open($"{directoryName}\\{fileName}", PdfDocumentOpenMode.ReadOnly);

                    // Check if the document is encrypted
                    {
                        // PDF is encrypted, extract security settings
                        var securitySettings = pdfDocTest.SecuritySettings;
                        string settings = GetSecuritySettings(securitySettings);

                        return (1, "Security Setting", settings);
                    }
                }
                else
                {
                    // Other PDF-related errors
                    var cleanedMessage = ex.Message.Replace("If you think this is a bug in PDFsharp, please send us your PDF file.", "").TrimEnd();
                    return (-2, "PDF Error", cleanedMessage);
                }
            }
            catch (Exception ex)
            {
                // General error handling
                var cleanedMessage = ex.Message.Replace("If you think this is a bug in PDFsharp, please send us your PDF file.", "").TrimEnd();
                return (-3, "Error", cleanedMessage);
            }
        }
        private void AddItemtoFileList(string index, string fileName)
        {
            var colour = Color.DimGray; // No security

            var (code, state, comment) = GetFileState(linkLabelFolderName.Text, fileName);

            if (code < 0)
            {
                colour = Color.RosyBrown; //Error
            }
            else if (code == 2)
            {
                colour = Color.LightGreen; //Password Protected
            }
            else if (code == 1)
            {
                colour = Color.LightSteelBlue; //Security Setting
            }

            // Create a new ListViewItem for each file
            var item = new ListViewItem(index)
            {
                SubItems = { fileName, state, comment },
                ForeColor = colour
            };

            // Add the item to the ListView
            listFilesFolders.Items.Add(item);
        }


        private void PopulateFilesFoldersList(string[] paths)
        {
            listFilesFolders.Items.Clear(); // Clear existing items before populating
            var i = 1;
            var rootPath = linkLabelFolderName.Text;

            foreach (var path in paths)
            {
                // Check if the path is a directory
                if (Directory.Exists(path))
                {
                    // Get all files in the directory
                    var searchOption = checkBoxRecurseDir.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                    //string[] files = Directory.GetFiles(path);
                    string[] files = Directory.GetFiles(path, "*.pdf", searchOption);
                    foreach (var file in files)
                    {
                        // Extract the file name and directory name
                        string fileName = Path.GetFileName(file);
                        // Remove the root path from the file path
                        string relativePath = file.Replace(rootPath, "").TrimStart('\\');
                        // Get the directory name from the relative path
                        string directoryName = Path.GetDirectoryName(relativePath) ?? string.Empty;

                        // Create a new ListViewItem for each file
                        AddItemtoFileList(i.ToString(), $"{directoryName}\\{fileName}");
                        i++;
                    }
                }
                else if (File.Exists(path) && Path.GetExtension(path).Equals(".pdf", StringComparison.OrdinalIgnoreCase)) // Check if the path is a file and has a .pdf extension
                {
                    // Extract just the file name
                    string fileName = Path.GetFileName(path);

                    // Create a new ListViewItem for the file
                    AddItemtoFileList(i.ToString(), fileName);
                    i++;
                }
                if (i % 20 == 0)
                {
                    // Adjust the column width to fit the content
                    listFilesFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listFilesFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                Application.DoEvents();
            }
/*
            // Set the linkLabelFolderName text to the directory path of the first item, if applicable
            if (paths.Length > 0 && paths[0].Contains("\\"))
            {
                linkLabelFolderName.Text = paths[0].Substring(0, paths[0].LastIndexOf('\\'));
            }
*/
            // Adjust the column width to fit the content
            listFilesFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listFilesFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            columnHeaderComment.Width = 300;
        }

        private void linkLabelFolderName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Directory.Exists(linkLabelFolderName.Text))
            {
                System.Diagnostics.Process.Start("explorer.exe", linkLabelFolderName.Text);
            }
        }
        private void radioButtonPrefix_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOverwriteString.PlaceholderText = "Enter Prefix";
            textBoxOverwriteString.Text = "decrypted_";
        }

        private void radioButtonSuffix_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOverwriteString.PlaceholderText = "Enter Suffix";
            textBoxOverwriteString.Text = "_decrypted";
        }

        private void checkBoxOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxOverwrite.Visible = !checkBoxOverwrite.Checked;
        }

        private void listFilesFolders_ItemActivate(object sender, EventArgs e)
        {
            if (listFilesFolders.SelectedItems.Count > 0)
            {
                var selectedItem = listFilesFolders.SelectedItems[0];
                var state = selectedItem.SubItems[2].Text;  // state
                var fileName = selectedItem.SubItems[1].Text;  // filename
                var comment = selectedItem.SubItems[3].Text; // comment
                var msg = "File: " + fileName + "\n\n" + comment;

                if (state == "Security Setting")
                {
                    // Replace all occurrences of ":" with ":\t" and "," with "\n"
                    var securitySettings = comment.Replace(":", ":\t").Replace(" | ", "\n");

                    msg = "File: " + fileName + "\n\n" + securitySettings;

                }
                columnHeaderComment.Width = 300;
                // Iterate through run subitems (columns) and show their content
                for (int i = 4; i < selectedItem.SubItems.Count; i++)
                {
                    msg += $"\n\n Run{i - 3}: {selectedItem.SubItems[i].Text}";
                }


                MessageBox.Show(msg, "PDF Security Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string ConstructDecryptedFilePath(string filePath)
        {
            string newFilePath = filePath;

            if (!checkBoxOverwrite.Checked)
            {

                string directoryName = Path.GetDirectoryName(filePath) ?? string.Empty;

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);
                string overwriteName = textBoxOverwriteString.Text;

                if (radioButtonPrefix.Checked)
                {
                    newFilePath = Path.Combine(directoryName, $"{overwriteName}{fileNameWithoutExtension}{extension}");
                }
                else
                {
                    newFilePath = Path.Combine(directoryName, $"{fileNameWithoutExtension}{overwriteName}{extension}");
                }
            }

            return newFilePath;
        }

        private (bool returnVal, string errormsg) DecryptPDFFile(string directoryName, string fileName, string password)
        {
            try
            {
                var filePath = Path.Combine(directoryName, fileName);

                // Open PDF using PdfSharpCore
                using (var pdfDocImport = PdfReader.Open(filePath, password, PdfDocumentOpenMode.Import))
                {
                    // Create a new document
                    using (var document = new PdfDocument())
                    {
                        // Import pages into the new document
                        foreach (var page in pdfDocImport.Pages)
                        {
                            document.AddPage(page);
                        }

                        // Remove security settings
                        document.SecuritySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.None;
                        // Additional permissions can be set as needed
                        document.SecuritySettings.PermitPrint = true;
                        document.SecuritySettings.PermitFullQualityPrint = true;
                        document.SecuritySettings.PermitAssembleDocument = true;
                        document.SecuritySettings.PermitExtractContent = true;
                        document.SecuritySettings.PermitAccessibilityExtractContent = true;
                        document.SecuritySettings.PermitModifyDocument = true;
                        document.SecuritySettings.PermitAnnotations = true;
                        document.SecuritySettings.PermitFormsFill = true;

                        // Save Decrypted File
                        // Construct the new file path based on the controls' values
                        string newFilePath = ConstructDecryptedFilePath(filePath);

                        document.Save(newFilePath);
                    }
                }
                return (true, "Success");
            }
            catch (Exception ex)
            {
                LogMessage($"Error decrypting file: {fileName} - {ex.Message}");
                return (false, ex.Message);
            }
        }

        private List<ListViewItem> GetPasswordProtectedFileList()
        {
            return listFilesFolders.Items
                .Cast<ListViewItem>()
                .Where(item => item.SubItems[2].Text == "Password Protected")
                .ToList();
        }

        static int RunIndex = 1;
        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            columnHeaderComment.Width = 300;


            var passwordProtectedItems = GetPasswordProtectedFileList();

            if (passwordProtectedItems.Count != 0)
            {
                // Add the "Run" column
                var columnHeaderRun = new ColumnHeader
                {
                    Text = $"Run{RunIndex}",
                    Width = 100 // Set an appropriate width
                };
                listFilesFolders.Columns.Add(columnHeaderRun);
            }
            else
            {
                MessageBox.Show("No password-protected files found.", "No Files", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Check if ActiveForm is not null before accessing its properties
                if (EnD.ActiveForm != null)
                {
                    EnD.ActiveForm.AcceptButton = buttonClose;
                    EnD.ActiveForm.CancelButton = buttonClose;
                    buttonClose.Focus();
                }
                return;
            }

            foreach (var item in passwordProtectedItems)
            {
                var directoryName = linkLabelFolderName.Text;
                var fileName = item.SubItems[1].Text;
                var password = textBoxPassword.Text;
                var listItem = listFilesFolders.Items[int.Parse(item.SubItems[0].Text) - 1];

                var (success, errmsg) = DecryptPDFFile(directoryName, fileName, password); // Replace "password" with the actual password
                if (success)
                {
                    listItem.SubItems[2].Text = "Decrypted"; // Update "State" column
                    listItem.SubItems.Add("Success"); // Update "Run" column
                    listItem.ForeColor = Color.WhiteSmoke;
                }
                else
                {
                    listItem.SubItems.Add($"Error: {errmsg}"); // Update "Run" column
                }
                Console.WriteLine($"Index: {item.SubItems[0].Text}, FileName: {item.SubItems[1].Text}, State: {item.SubItems[2].Text}, Comment: {item.SubItems[3].Text}");
            }
            RunIndex++;
            textBoxPassword.Focus();
            textBoxPassword.SelectAll();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            EnD.ActiveForm?.Close();
        }

        private void textBoxPassword_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPassword.SelectAll();
        }

        private void checkBoxRecurseDir_CheckedChanged(object sender, EventArgs e)
        {
            // Populate the ListView with the files/folders passed as arguments
            if (commandlineArgs != null)
            {
                // Run the long-running process on a background thread
                // Conditional execution based on build configuration
#if RELEASE
                Task.Run(() => PopulateFilesFoldersList(commandlineArgs));
#elif DEBUG
                PopulateFilesFoldersList(commandlineArgs);
#endif
            }
        }
    }
}
