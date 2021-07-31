using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JBACodeTest
{
    public partial class JbaCcForm : Form
    {
        DbManager _dbManager;

        public JbaCcForm(DbManager dbManager)
        {
            _dbManager = dbManager;

            InitializeComponent();

            buttonImport.Enabled = false;
            textBoxInputFileName.Text = @"C:\Users\Ed\Documents\GitHub\JBACodeTest\jba-software-code-challenge-data-transformation\cru-ts-2-10.1991-2000-cutdown.pre";
            textBoxDbPath.Text = @"C:\Users\Ed\Documents\GitHub\JBACodeTest\TestDatabase.mdf";

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

        private void buttonBrowseDb_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select database";
            openFileDialog.DefaultExt = "mdb";
            openFileDialog.Filter = "Microsoft Access Database files (*.mdb)|*.pre";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDbPath.Text = openFileDialog.FileName;
            }
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
            _dbManager.Connect(textBoxDbPath.Text);

        }

        private void buttonTestDb_Click(object sender, EventArgs e)
        {

        }

        private void textBoxInputFileName_TextChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool enable = false;
            
            if (textBoxInputFileName.Text != "" && File.Exists(textBoxInputFileName.Text))
            {
                if (textBoxDbPath.Text != "" && File.Exists(textBoxDbPath.Text))
                {
                    enable = true;
                }
            }
            buttonImport.Enabled = enable;
        }

        private void textBoxDbPath_TextChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

    }
}
