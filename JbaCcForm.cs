using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JBACodeTest
{
    public partial class JbaCcForm : Form
    {
        DbManager dbManager;

        public JbaCcForm(DbManager dbManager)
        {
            InitializeComponent();

            buttonImport.Enabled = false;
            textBoxInputFileName.Text = @"C:\Users\Ed\Documents\GitHub\JBACodeTest\jba-software-code-challenge-data-transformation\cru-ts-2-10.1991-2000-cutdown.pre";

            ClearStatus();
            //AddStatusLine("Started app");
        }

        private void AddStatusLine(string line)
        {
            textBoxStatus.Text += ("\n" + line);
        }

        private void ClearStatus()
        {
            textBoxStatus.Text = "";
        }

        private void buttonChooseInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select input file";
            openFileDialog.DefaultExt = "pre";
            openFileDialog.Filter = "pre files (*.pre)|*.pre|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxInputFileName.Text = openFileDialog.FileName;
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {

        }

        private void buttonTestDb_Click(object sender, EventArgs e)
        {

        }

        private void textBoxInputFileName_TextChanged(object sender, EventArgs e)
        {
            bool enable = (textBoxInputFileName.Text != "");
            buttonImport.Enabled = enable;
        }
    }
}
