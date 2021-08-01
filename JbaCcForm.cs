using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            textBoxStatus.Text += ("\r\n" + line);
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
            AddStatusLine("Parsing file " + textBoxInputFileName.Text);
            List<RainfallEntry> entries = ParseFile(textBoxInputFileName.Text);
            AddStatusLine("Parse complete, returned " + entries.Count.ToString() + " entries");

            AddStatusLine("Inserting into DB, please wait...");
            var result = _dbManager.InsertData(textBoxDbPath.Text, entries);
            AddStatusLine("Finished import: " + result);
        }

        private List<RainfallEntry> ParseFile(string inputFile)
        {
            List<RainfallEntry> entries = new List<RainfallEntry>();

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(inputFile))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                int startYear = 0, endYear = 0;

                String line;
                int lineNum = 1;
                int lineNumInBlock = -1;
                int xVal = -1, yVal = -1;

                while ((line = streamReader.ReadLine()) != null)
                {
                    // Process line
                    if (lineNum < 5)
                    {
                        // skip these header lines
                    }
                    else if (lineNum == 5)
                    {
                        // regex find the "Years" block
                        var yearsBlock = Regex.Match(line.ToLowerInvariant(), "\\[years=.*?\\]"); // NB toLower
                        if (!yearsBlock.Success)
                            throw new Exception("Couldn't find Years in line 5 of header");

                        var years2 = Regex.Match(yearsBlock.Value, @"\d{4}\s*-\s*\d{4}"); // not sure if this regex use is overkill but using in an attempt to second guess variations in the file format, e.g. whitespace
                        if (!years2.Success)
                            throw new Exception("Couldn't find xxxx-yyyy in years block");

                        var yrs = years2.Value.Split(new char[] { '-' });
                        if (!Int32.TryParse(yrs[0], out startYear))
                            throw new Exception("YEARS parse error (start)");
                        if (!Int32.TryParse(yrs[1], out endYear))
                            throw new Exception("YEARS parse error (end)");


                    }
                    else
                    {

                        var gridrefBlock = Regex.Match(line.ToLowerInvariant(), "grid-ref=.*"); // NB toLower
                        if (gridrefBlock.Success) /// new block start
                        {

                            var eqpos = gridrefBlock.Value.IndexOf('=');
                            var commapos = gridrefBlock.Value.IndexOf(',');
                            var xStr = gridrefBlock.Value.Substring(eqpos + 1, commapos - eqpos - 1);
                            var yStr = gridrefBlock.Value.Substring(commapos + 1);


                            if (!Int32.TryParse(xStr, out xVal))
                                throw new Exception("x parse error");
                            if (!Int32.TryParse(yStr, out yVal))
                                throw new Exception("y parse error");

                            lineNumInBlock = 0;
                        }
                        else
                        {
                            // assume this is a line of 12 ints
                            var monthlyVals = ParseRainfallLine(line);

                            for (int m = 0; m < monthlyVals.Length; ++m)
                            {
                                int year = startYear + lineNumInBlock;
                                RainfallEntry e = new RainfallEntry() { _x = xVal, _y = yVal, _date = new DateTime(year, m+1, 1), _amount = monthlyVals[m] };
                                entries.Add(e);
                            }

                            lineNumInBlock++;
                        }
                    }


                    lineNum++;
                }
            }

            return entries;
        }

        private int[] ParseRainfallLine(string line)    // (Note: I initially assumed this is a line of 12 ints separated by spaces or tabs, but turns out it's just 12 x 5 character blocks!)
        {
            var vals = new int[12];

            if (line.Length != 60)
                throw new Exception("Unexpected line length");
            
            
            for (int i = 0; i < 12; ++i)
            {
                var mstr = line.Substring(i * 5, 5);

                if (!Int32.TryParse(mstr, out vals[i]))
                    throw new Exception("monthly value parse error");
            }

            return vals;
        }

        private void buttonTestDb_Click(object sender, EventArgs e)
        {
            AddStatusLine(_dbManager.Report(textBoxDbPath.Text));
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
