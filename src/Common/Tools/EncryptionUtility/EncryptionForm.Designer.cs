namespace AlwaysMoveForward.Common.Tools.EncryptionUtility
{
    /// <summary>
    /// Encryption Form
    /// </summary>
    public partial class EncryptionForm
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.keyFileEncryptionTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txtEncryptedText = new System.Windows.Forms.TextBox();
            this.txtClearText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrivateKeyFile = new System.Windows.Forms.Button();
            this.btnPublicKeyFile = new System.Windows.Forms.Button();
            this.txtKeyFilePassword = new System.Windows.Forms.TextBox();
            this.txtPrivateKeyFile = new System.Windows.Forms.TextBox();
            this.txtPublicKeyFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.keyStoreEncryptionTab = new System.Windows.Forms.TabPage();
            this.txtCertificateName = new System.Windows.Forms.TextBox();
            this.txtStoreLocation = new System.Windows.Forms.TextBox();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnKeyStoreDecrypt = new System.Windows.Forms.Button();
            this.btnKeyStoreEncrypt = new System.Windows.Forms.Button();
            this.txtKeyStoreEncryptedText = new System.Windows.Forms.TextBox();
            this.txtKeyStoreClearText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.aesEncryptionTab = new System.Windows.Forms.TabPage();
            this.txtAESSalt = new System.Windows.Forms.TextBox();
            this.txtAESEncryptionKey = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAESDecrypt = new System.Windows.Forms.Button();
            this.btnAESEncrypt = new System.Windows.Forms.Button();
            this.txtAESEncryptedText = new System.Windows.Forms.TextBox();
            this.txtAESClearText = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.btnGenerateSalt = new System.Windows.Forms.Button();
            this.keyFileEncryptionTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.keyStoreEncryptionTab.SuspendLayout();
            this.aesEncryptionTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // keyFileEncryptionTab
            // 
            this.keyFileEncryptionTab.Controls.Add(this.tabPage1);
            this.keyFileEncryptionTab.Controls.Add(this.keyStoreEncryptionTab);
            this.keyFileEncryptionTab.Controls.Add(this.aesEncryptionTab);
            this.keyFileEncryptionTab.Location = new System.Drawing.Point(1, 13);
            this.keyFileEncryptionTab.Name = "keyFileEncryptionTab";
            this.keyFileEncryptionTab.SelectedIndex = 0;
            this.keyFileEncryptionTab.Size = new System.Drawing.Size(446, 274);
            this.keyFileEncryptionTab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDecrypt);
            this.tabPage1.Controls.Add(this.btnEncrypt);
            this.tabPage1.Controls.Add(this.txtEncryptedText);
            this.tabPage1.Controls.Add(this.txtClearText);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnPrivateKeyFile);
            this.tabPage1.Controls.Add(this.btnPublicKeyFile);
            this.tabPage1.Controls.Add(this.txtKeyFilePassword);
            this.tabPage1.Controls.Add(this.txtPrivateKeyFile);
            this.tabPage1.Controls.Add(this.txtPublicKeyFile);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(438, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Key File Encryption";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(311, 147);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(66, 23);
            this.btnDecrypt.TabIndex = 13;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(311, 124);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(66, 23);
            this.btnEncrypt.TabIndex = 12;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txtEncryptedText
            // 
            this.txtEncryptedText.Location = new System.Drawing.Point(123, 150);
            this.txtEncryptedText.Name = "txtEncryptedText";
            this.txtEncryptedText.Size = new System.Drawing.Size(182, 20);
            this.txtEncryptedText.TabIndex = 11;
            // 
            // txtClearText
            // 
            this.txtClearText.Location = new System.Drawing.Point(123, 126);
            this.txtClearText.Name = "txtClearText";
            this.txtClearText.Size = new System.Drawing.Size(182, 20);
            this.txtClearText.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Encrypted Text";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Clear Text";
            // 
            // btnPrivateKeyFile
            // 
            this.btnPrivateKeyFile.Location = new System.Drawing.Point(311, 42);
            this.btnPrivateKeyFile.Name = "btnPrivateKeyFile";
            this.btnPrivateKeyFile.Size = new System.Drawing.Size(33, 23);
            this.btnPrivateKeyFile.TabIndex = 7;
            this.btnPrivateKeyFile.Text = "...";
            this.btnPrivateKeyFile.UseVisualStyleBackColor = true;
            this.btnPrivateKeyFile.Click += new System.EventHandler(this.btnPrivateKeyFile_Click);
            // 
            // btnPublicKeyFile
            // 
            this.btnPublicKeyFile.Location = new System.Drawing.Point(311, 18);
            this.btnPublicKeyFile.Name = "btnPublicKeyFile";
            this.btnPublicKeyFile.Size = new System.Drawing.Size(33, 23);
            this.btnPublicKeyFile.TabIndex = 6;
            this.btnPublicKeyFile.Text = "...";
            this.btnPublicKeyFile.UseVisualStyleBackColor = true;
            this.btnPublicKeyFile.Click += new System.EventHandler(this.btnPublicKeyFile_Click);
            // 
            // txtKeyFilePassword
            // 
            this.txtKeyFilePassword.Location = new System.Drawing.Point(123, 64);
            this.txtKeyFilePassword.Name = "txtKeyFilePassword";
            this.txtKeyFilePassword.Size = new System.Drawing.Size(182, 20);
            this.txtKeyFilePassword.TabIndex = 5;
            // 
            // txtPrivateKeyFile
            // 
            this.txtPrivateKeyFile.Location = new System.Drawing.Point(123, 42);
            this.txtPrivateKeyFile.Name = "txtPrivateKeyFile";
            this.txtPrivateKeyFile.ReadOnly = true;
            this.txtPrivateKeyFile.Size = new System.Drawing.Size(182, 20);
            this.txtPrivateKeyFile.TabIndex = 4;
            // 
            // txtPublicKeyFile
            // 
            this.txtPublicKeyFile.Location = new System.Drawing.Point(123, 18);
            this.txtPublicKeyFile.Name = "txtPublicKeyFile";
            this.txtPublicKeyFile.ReadOnly = true;
            this.txtPublicKeyFile.Size = new System.Drawing.Size(182, 20);
            this.txtPublicKeyFile.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Private Key Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Private Key File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Public Key File";
            // 
            // keyStoreEncryptionTab
            // 
            this.keyStoreEncryptionTab.Controls.Add(this.txtCertificateName);
            this.keyStoreEncryptionTab.Controls.Add(this.txtStoreLocation);
            this.keyStoreEncryptionTab.Controls.Add(this.txtStoreName);
            this.keyStoreEncryptionTab.Controls.Add(this.label10);
            this.keyStoreEncryptionTab.Controls.Add(this.label9);
            this.keyStoreEncryptionTab.Controls.Add(this.label8);
            this.keyStoreEncryptionTab.Controls.Add(this.btnKeyStoreDecrypt);
            this.keyStoreEncryptionTab.Controls.Add(this.btnKeyStoreEncrypt);
            this.keyStoreEncryptionTab.Controls.Add(this.txtKeyStoreEncryptedText);
            this.keyStoreEncryptionTab.Controls.Add(this.txtKeyStoreClearText);
            this.keyStoreEncryptionTab.Controls.Add(this.label6);
            this.keyStoreEncryptionTab.Controls.Add(this.label7);
            this.keyStoreEncryptionTab.Location = new System.Drawing.Point(4, 22);
            this.keyStoreEncryptionTab.Name = "keyStoreEncryptionTab";
            this.keyStoreEncryptionTab.Padding = new System.Windows.Forms.Padding(3);
            this.keyStoreEncryptionTab.Size = new System.Drawing.Size(438, 248);
            this.keyStoreEncryptionTab.TabIndex = 1;
            this.keyStoreEncryptionTab.Text = "Key Store Encryption";
            this.keyStoreEncryptionTab.UseVisualStyleBackColor = true;
            // 
            // txtCertificateName
            // 
            this.txtCertificateName.Location = new System.Drawing.Point(130, 49);
            this.txtCertificateName.Name = "txtCertificateName";
            this.txtCertificateName.Size = new System.Drawing.Size(182, 20);
            this.txtCertificateName.TabIndex = 25;
            // 
            // txtStoreLocation
            // 
            this.txtStoreLocation.Location = new System.Drawing.Point(130, 28);
            this.txtStoreLocation.Name = "txtStoreLocation";
            this.txtStoreLocation.Size = new System.Drawing.Size(182, 20);
            this.txtStoreLocation.TabIndex = 24;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Location = new System.Drawing.Point(130, 7);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(182, 20);
            this.txtStoreName.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Certificate Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Store Location";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Store Name";
            // 
            // btnKeyStoreDecrypt
            // 
            this.btnKeyStoreDecrypt.Location = new System.Drawing.Point(318, 171);
            this.btnKeyStoreDecrypt.Name = "btnKeyStoreDecrypt";
            this.btnKeyStoreDecrypt.Size = new System.Drawing.Size(66, 23);
            this.btnKeyStoreDecrypt.TabIndex = 19;
            this.btnKeyStoreDecrypt.Text = "Decrypt";
            this.btnKeyStoreDecrypt.UseVisualStyleBackColor = true;
            this.btnKeyStoreDecrypt.Click += new System.EventHandler(this.btnKeyStoreDecrypt_Click);
            // 
            // btnKeyStoreEncrypt
            // 
            this.btnKeyStoreEncrypt.Location = new System.Drawing.Point(318, 148);
            this.btnKeyStoreEncrypt.Name = "btnKeyStoreEncrypt";
            this.btnKeyStoreEncrypt.Size = new System.Drawing.Size(66, 23);
            this.btnKeyStoreEncrypt.TabIndex = 18;
            this.btnKeyStoreEncrypt.Text = "Encrypt";
            this.btnKeyStoreEncrypt.UseVisualStyleBackColor = true;
            this.btnKeyStoreEncrypt.Click += new System.EventHandler(this.btnKeyStoreEncrypt_Click);
            // 
            // txtKeyStoreEncryptedText
            // 
            this.txtKeyStoreEncryptedText.Location = new System.Drawing.Point(130, 174);
            this.txtKeyStoreEncryptedText.Name = "txtKeyStoreEncryptedText";
            this.txtKeyStoreEncryptedText.Size = new System.Drawing.Size(182, 20);
            this.txtKeyStoreEncryptedText.TabIndex = 17;
            // 
            // txtKeyStoreClearText
            // 
            this.txtKeyStoreClearText.Location = new System.Drawing.Point(130, 150);
            this.txtKeyStoreClearText.Name = "txtKeyStoreClearText";
            this.txtKeyStoreClearText.Size = new System.Drawing.Size(182, 20);
            this.txtKeyStoreClearText.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Encrypted Text";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Clear Text";
            // 
            // aesEncryptionTab
            // 
            this.aesEncryptionTab.Controls.Add(this.btnGenerateSalt);
            this.aesEncryptionTab.Controls.Add(this.btnGenerateKey);
            this.aesEncryptionTab.Controls.Add(this.txtAESSalt);
            this.aesEncryptionTab.Controls.Add(this.txtAESEncryptionKey);
            this.aesEncryptionTab.Controls.Add(this.label12);
            this.aesEncryptionTab.Controls.Add(this.label13);
            this.aesEncryptionTab.Controls.Add(this.btnAESDecrypt);
            this.aesEncryptionTab.Controls.Add(this.btnAESEncrypt);
            this.aesEncryptionTab.Controls.Add(this.txtAESEncryptedText);
            this.aesEncryptionTab.Controls.Add(this.txtAESClearText);
            this.aesEncryptionTab.Controls.Add(this.label14);
            this.aesEncryptionTab.Controls.Add(this.label15);
            this.aesEncryptionTab.Location = new System.Drawing.Point(4, 22);
            this.aesEncryptionTab.Name = "aesEncryptionTab";
            this.aesEncryptionTab.Padding = new System.Windows.Forms.Padding(3);
            this.aesEncryptionTab.Size = new System.Drawing.Size(438, 248);
            this.aesEncryptionTab.TabIndex = 2;
            this.aesEncryptionTab.Text = "AES Encryption";
            this.aesEncryptionTab.UseVisualStyleBackColor = true;
            // 
            // txtAESSalt
            // 
            this.txtAESSalt.Location = new System.Drawing.Point(125, 32);
            this.txtAESSalt.Name = "txtAESSalt";
            this.txtAESSalt.Size = new System.Drawing.Size(182, 20);
            this.txtAESSalt.TabIndex = 36;
            // 
            // txtAESEncryptionKey
            // 
            this.txtAESEncryptionKey.Location = new System.Drawing.Point(125, 11);
            this.txtAESEncryptionKey.Name = "txtAESEncryptionKey";
            this.txtAESEncryptionKey.Size = new System.Drawing.Size(182, 20);
            this.txtAESEncryptionKey.TabIndex = 35;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Salt";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Encryption Key";
            // 
            // btnAESDecrypt
            // 
            this.btnAESDecrypt.Location = new System.Drawing.Point(313, 175);
            this.btnAESDecrypt.Name = "btnAESDecrypt";
            this.btnAESDecrypt.Size = new System.Drawing.Size(66, 23);
            this.btnAESDecrypt.TabIndex = 31;
            this.btnAESDecrypt.Text = "Decrypt";
            this.btnAESDecrypt.UseVisualStyleBackColor = true;
            this.btnAESDecrypt.Click += new System.EventHandler(this.btnAESDecrypt_Click);
            // 
            // btnAESEncrypt
            // 
            this.btnAESEncrypt.Location = new System.Drawing.Point(313, 152);
            this.btnAESEncrypt.Name = "btnAESEncrypt";
            this.btnAESEncrypt.Size = new System.Drawing.Size(66, 23);
            this.btnAESEncrypt.TabIndex = 30;
            this.btnAESEncrypt.Text = "Encrypt";
            this.btnAESEncrypt.UseVisualStyleBackColor = true;
            this.btnAESEncrypt.Click += new System.EventHandler(this.btnAESEncrypt_Click);
            // 
            // txtAESEncryptedText
            // 
            this.txtAESEncryptedText.Location = new System.Drawing.Point(125, 178);
            this.txtAESEncryptedText.Name = "txtAESEncryptedText";
            this.txtAESEncryptedText.Size = new System.Drawing.Size(182, 20);
            this.txtAESEncryptedText.TabIndex = 29;
            // 
            // txtAESClearText
            // 
            this.txtAESClearText.Location = new System.Drawing.Point(125, 154);
            this.txtAESClearText.Name = "txtAESClearText";
            this.txtAESClearText.Size = new System.Drawing.Size(182, 20);
            this.txtAESClearText.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 181);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Encrypted Text";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 157);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Clear Text";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnGenerateKey
            // 
            this.btnGenerateKey.Location = new System.Drawing.Point(313, 6);
            this.btnGenerateKey.Name = "btnGenerateKey";
            this.btnGenerateKey.Size = new System.Drawing.Size(110, 23);
            this.btnGenerateKey.TabIndex = 37;
            this.btnGenerateKey.Text = "Generate Key";
            this.btnGenerateKey.UseVisualStyleBackColor = true;
            this.btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);
            // 
            // btnGenerateSalt
            // 
            this.btnGenerateSalt.Location = new System.Drawing.Point(313, 29);
            this.btnGenerateSalt.Name = "btnGenerateSalt";
            this.btnGenerateSalt.Size = new System.Drawing.Size(110, 23);
            this.btnGenerateSalt.TabIndex = 38;
            this.btnGenerateSalt.Text = "Generate Salt";
            this.btnGenerateSalt.UseVisualStyleBackColor = true;
            this.btnGenerateSalt.Click += new System.EventHandler(this.btnGenerateSalt_Click);
            // 
            // EncryptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 289);
            this.Controls.Add(this.keyFileEncryptionTab);
            this.Name = "EncryptionForm";
            this.Text = "Form1";
            this.keyFileEncryptionTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.keyStoreEncryptionTab.ResumeLayout(false);
            this.keyStoreEncryptionTab.PerformLayout();
            this.aesEncryptionTab.ResumeLayout(false);
            this.aesEncryptionTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl keyFileEncryptionTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage keyStoreEncryptionTab;
        private System.Windows.Forms.TabPage aesEncryptionTab;
        private System.Windows.Forms.TextBox txtKeyFilePassword;
        private System.Windows.Forms.TextBox txtPrivateKeyFile;
        private System.Windows.Forms.TextBox txtPublicKeyFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnPrivateKeyFile;
        private System.Windows.Forms.Button btnPublicKeyFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txtEncryptedText;
        private System.Windows.Forms.TextBox txtClearText;
        private System.Windows.Forms.TextBox txtCertificateName;
        private System.Windows.Forms.TextBox txtStoreLocation;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnKeyStoreDecrypt;
        private System.Windows.Forms.Button btnKeyStoreEncrypt;
        private System.Windows.Forms.TextBox txtKeyStoreEncryptedText;
        private System.Windows.Forms.TextBox txtKeyStoreClearText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAESSalt;
        private System.Windows.Forms.TextBox txtAESEncryptionKey;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAESDecrypt;
        private System.Windows.Forms.Button btnAESEncrypt;
        private System.Windows.Forms.TextBox txtAESEncryptedText;
        private System.Windows.Forms.TextBox txtAESClearText;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnGenerateSalt;
        private System.Windows.Forms.Button btnGenerateKey;
    }
}