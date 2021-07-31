namespace JBACodeTest
{
    partial class JbaCcForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.textBoxInputFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonChooseInputFile = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTestDb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxInputFileName
            // 
            this.textBoxInputFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInputFileName.Location = new System.Drawing.Point(80, 43);
            this.textBoxInputFileName.Name = "textBoxInputFileName";
            this.textBoxInputFileName.Size = new System.Drawing.Size(330, 20);
            this.textBoxInputFileName.TabIndex = 0;
            this.textBoxInputFileName.TextChanged += new System.EventHandler(this.textBoxInputFileName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input file:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonChooseInputFile
            // 
            this.buttonChooseInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChooseInputFile.Location = new System.Drawing.Point(422, 40);
            this.buttonChooseInputFile.Name = "buttonChooseInputFile";
            this.buttonChooseInputFile.Size = new System.Drawing.Size(36, 23);
            this.buttonChooseInputFile.TabIndex = 2;
            this.buttonChooseInputFile.Text = "...";
            this.buttonChooseInputFile.UseVisualStyleBackColor = true;
            this.buttonChooseInputFile.Click += new System.EventHandler(this.buttonChooseInputFile_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImport.Location = new System.Drawing.Point(465, 40);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 3;
            this.buttonImport.Text = "Go";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.Location = new System.Drawing.Point(12, 105);
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxStatus.Size = new System.Drawing.Size(528, 282);
            this.textBoxStatus.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status";
            // 
            // buttonTestDb
            // 
            this.buttonTestDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestDb.Location = new System.Drawing.Point(416, 399);
            this.buttonTestDb.Name = "buttonTestDb";
            this.buttonTestDb.Size = new System.Drawing.Size(124, 23);
            this.buttonTestDb.TabIndex = 6;
            this.buttonTestDb.Text = "Test DB contents";
            this.buttonTestDb.UseVisualStyleBackColor = true;
            this.buttonTestDb.Click += new System.EventHandler(this.buttonTestDb_Click);
            // 
            // JbaCcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 432);
            this.Controls.Add(this.buttonTestDb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonChooseInputFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInputFileName);
            this.Name = "JbaCcForm";
            this.Text = "JBA Code Challenge / Ed Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInputFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonChooseInputFile;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonTestDb;
    }
}

