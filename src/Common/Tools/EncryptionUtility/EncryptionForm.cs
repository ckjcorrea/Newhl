using System;
using System.IO;
using System.Windows.Forms;
using AlwaysMoveForward.Common.Encryption;

namespace AlwaysMoveForward.Common.Tools.EncryptionUtility
{
    /// <summary>
    /// Encryption Form Class
    /// </summary>
    public partial class EncryptionForm : Form
    {
        public EncryptionForm()
        {
            this.InitializeComponent();
        }

        private string ShowFileSelectDialog(string dialogFilter)
        {
            string retVal = string.Empty;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = dialogFilter;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                retVal = openFileDialog1.FileName;
            }

            return retVal;
        }

        private void btnPublicKeyFile_Click(object sender, EventArgs e)
        {
            this.txtPublicKeyFile.Text = this.ShowFileSelectDialog("cer files (*.cer)|*.cer|All files (*.*)|*.*");
        }

        private void btnPrivateKeyFile_Click(object sender, EventArgs e)
        {
            this.txtPrivateKeyFile.Text = this.ShowFileSelectDialog("pfx files (*.pfx)|*.pfx|All files (*.*)|*.*");
        }

        private bool IsKeyFileEncryptionValid()
        {
            bool retVal = true;

            if (string.IsNullOrEmpty(this.txtPublicKeyFile.Text))
            {
                retVal = false;
                MessageBox.Show("Please select a public key file");
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtClearText.Text))
                {
                    retVal = false;
                    MessageBox.Show("Please enter a string to encrypt");
                }
            }

            return retVal;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (this.IsKeyFileEncryptionValid())
            {
                X509CertificateManager certificateManager = new X509CertificateManager(this.txtPublicKeyFile.Text, string.Empty);
                this.txtEncryptedText.Text = certificateManager.Encrypt(this.txtClearText.Text);
            }
        }

        private bool IsKeyFileDecryptionValid()
        {
            bool retVal = true;

            if (string.IsNullOrEmpty(this.txtPrivateKeyFile.Text))
            {
                retVal = false;
                MessageBox.Show("Please select a private key file");
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtKeyFilePassword.Text))
                {
                    retVal = false;
                    MessageBox.Show("Please enter the password for the private key");
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtEncryptedText.Text))
                    {
                        retVal = false;
                        MessageBox.Show("Please enter a string to decrypt");
                    }
                }
            }

            return retVal;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (this.IsKeyFileDecryptionValid())
            {
                X509CertificateManager certificateManager = new X509CertificateManager(this.txtPrivateKeyFile.Text, this.txtKeyFilePassword.Text);
                this.txtClearText.Text = certificateManager.Decrypt(this.txtEncryptedText.Text);
            }
        }

        private bool IsKeyStoreValid()
        {
            bool retVal = true;

            if (string.IsNullOrEmpty(this.txtStoreName.Text))
            {
                retVal = false;
                MessageBox.Show("Please enter a Store Name");
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtStoreLocation.Text))
                {
                    retVal = false;
                    MessageBox.Show("Please enter a store location");
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtCertificateName.Text))
                    {
                        retVal = false;
                        MessageBox.Show("Please enter a Certificate Name");
                    }
                }
            }

            return retVal;
        }

        private void btnKeyStoreEncrypt_Click(object sender, EventArgs e)
        {
            if (this.IsKeyStoreValid())
            {
                if (!string.IsNullOrEmpty(this.txtKeyStoreClearText.Text))
                {
                    X509CertificateManager certificateManager = new X509CertificateManager(this.txtStoreName.Text, this.txtStoreLocation.Text, this.txtCertificateName.Text);
                    this.txtKeyStoreEncryptedText.Text = certificateManager.Encrypt(this.txtKeyStoreClearText.Text);
                }
            }
        }

        private void btnKeyStoreDecrypt_Click(object sender, EventArgs e)
        {
            if (this.IsKeyStoreValid())
            {
                if (!string.IsNullOrEmpty(this.txtKeyStoreEncryptedText.Text))
                {
                    X509CertificateManager certificateManager = new X509CertificateManager(this.txtStoreName.Text, this.txtStoreLocation.Text, this.txtCertificateName.Text);
                    this.txtKeyStoreClearText.Text = certificateManager.Decrypt(this.txtKeyStoreEncryptedText.Text);
                }
            }
        }

        private bool IsAESValid()
        {
            bool retVal = true;

            if (string.IsNullOrEmpty(this.txtAESEncryptionKey.Text))
            {
                retVal = false;
                MessageBox.Show("Please enter an encryption Key");
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtAESSalt.Text))
                {
                    retVal = false;
                    MessageBox.Show("Please enter a Salt");
                }
            }

            return retVal;
        }

        private void btnAESEncrypt_Click(object sender, EventArgs e)
        {
            if (this.IsAESValid())
            {
                if (!string.IsNullOrEmpty(this.txtAESClearText.Text))
                {
                    AESManager certificateManager = new AESManager(this.txtAESEncryptionKey.Text, this.txtAESSalt.Text);
                    this.txtAESEncryptedText.Text = certificateManager.Encrypt(this.txtAESClearText.Text);
                }
            }
        }

        private void btnAESDecrypt_Click(object sender, EventArgs e)
        {
            if (this.IsAESValid())
            {
                if (!string.IsNullOrEmpty(this.txtAESEncryptedText.Text))
                {
                    AESManager certificateManager = new AESManager(this.txtAESEncryptionKey.Text, this.txtAESSalt.Text);
                    this.txtAESClearText.Text = certificateManager.Decrypt(this.txtAESEncryptedText.Text);
                }
            }           
        }

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            // This is arbitrary, just generate 4 guids and put them together.
            this.txtAESEncryptionKey.Text = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                txtAESEncryptionKey.Text += Guid.NewGuid().ToString("N");
            }
        }

        private void btnGenerateSalt_Click(object sender, EventArgs e)
        {
            // This is arbitrary, just generate 2 guids and put them together.
            this.txtAESSalt.Text = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                txtAESSalt.Text += Guid.NewGuid().ToString("N");
            }

        }
    }
}
