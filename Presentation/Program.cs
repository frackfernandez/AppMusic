using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PrepareDirectory();
            Application.Run(new UILogin());
        }
        private static void PrepareDirectory()
        {
            string directoryMusic = Directory.GetCurrentDirectory() + @"\Music";
            if (!Directory.Exists(directoryMusic))
            {
                Directory.CreateDirectory(directoryMusic);
            }
        }
    }
}
