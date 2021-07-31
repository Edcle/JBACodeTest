using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JBACodeTest
{
    static class Program
    {
        static public DbManager dbManager;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            dbManager = new DbManager();

            Application.Run(new JbaCcForm(dbManager));
        }
    }
}
