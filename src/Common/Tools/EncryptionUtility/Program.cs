using System;
using System.Windows.Forms;

namespace AlwaysMoveForward.Common.Tools.EncryptionUtility
{
    /// <summary>
    /// Program Class for EncryptionUtility
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EncryptionForm());
        }
    }
}
