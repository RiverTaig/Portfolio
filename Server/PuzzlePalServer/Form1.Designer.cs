namespace PuzzlePalServer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panCurrent = new System.Windows.Forms.Panel();
            this.lblMin = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panMin = new System.Windows.Forms.Panel();
            this.lblMax = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panMax = new System.Windows.Forms.Panel();
            this.lblGreenNotDominant = new System.Windows.Forms.Label();
            this.panGreen = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panBlue = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panRed = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panWhite = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panBlack = new System.Windows.Forms.Panel();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblIsBorder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 521);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(416, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Process Images";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(482, 856);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove_1);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(429, 62);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(317, 472);
            this.listBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 541);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 514);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "125";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 584);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "lblRGB";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(104, 658);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(35, 13);
            this.lblCurrent.TabIndex = 8;
            this.lblCurrent.Text = "label4";
            this.lblCurrent.Click += new System.EventHandler(this.label4_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 494);
            this.panel1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 658);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Current Color";
            // 
            // panCurrent
            // 
            this.panCurrent.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panCurrent.Location = new System.Drawing.Point(36, 674);
            this.panCurrent.Name = "panCurrent";
            this.panCurrent.Size = new System.Drawing.Size(103, 22);
            this.panCurrent.TabIndex = 11;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(248, 658);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(35, 13);
            this.lblMin.TabIndex = 8;
            this.lblMin.Text = "label4";
            this.lblMin.Click += new System.EventHandler(this.label4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 658);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Min";
            // 
            // panMin
            // 
            this.panMin.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panMin.Location = new System.Drawing.Point(180, 674);
            this.panMin.Name = "panMin";
            this.panMin.Size = new System.Drawing.Size(103, 22);
            this.panMin.TabIndex = 11;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(429, 658);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(35, 13);
            this.lblMax.TabIndex = 8;
            this.lblMax.Text = "label4";
            this.lblMax.Click += new System.EventHandler(this.label4_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(358, 658);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Max";
            // 
            // panMax
            // 
            this.panMax.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panMax.Location = new System.Drawing.Point(365, 674);
            this.panMax.Name = "panMax";
            this.panMax.Size = new System.Drawing.Size(99, 22);
            this.panMax.TabIndex = 11;
            // 
            // lblGreenNotDominant
            // 
            this.lblGreenNotDominant.AutoSize = true;
            this.lblGreenNotDominant.Location = new System.Drawing.Point(558, 570);
            this.lblGreenNotDominant.Name = "lblGreenNotDominant";
            this.lblGreenNotDominant.Size = new System.Drawing.Size(108, 13);
            this.lblGreenNotDominant.TabIndex = 12;
            this.lblGreenNotDominant.Text = "Green NOT dominant";
            // 
            // panGreen
            // 
            this.panGreen.BackColor = System.Drawing.Color.Green;
            this.panGreen.Location = new System.Drawing.Point(672, 561);
            this.panGreen.Name = "panGreen";
            this.panGreen.Size = new System.Drawing.Size(99, 22);
            this.panGreen.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(558, 610);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Blue NOT dominant";
            // 
            // panBlue
            // 
            this.panBlue.BackColor = System.Drawing.Color.Blue;
            this.panBlue.Location = new System.Drawing.Point(672, 601);
            this.panBlue.Name = "panBlue";
            this.panBlue.Size = new System.Drawing.Size(99, 22);
            this.panBlue.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(558, 648);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Red NOT dominant";
            // 
            // panRed
            // 
            this.panRed.BackColor = System.Drawing.Color.Red;
            this.panRed.Location = new System.Drawing.Point(672, 639);
            this.panRed.Name = "panRed";
            this.panRed.Size = new System.Drawing.Size(99, 22);
            this.panRed.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(558, 683);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "White NOT dominant";
            // 
            // panWhite
            // 
            this.panWhite.BackColor = System.Drawing.Color.White;
            this.panWhite.Location = new System.Drawing.Point(672, 674);
            this.panWhite.Name = "panWhite";
            this.panWhite.Size = new System.Drawing.Size(99, 22);
            this.panWhite.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(558, 726);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Black NOT dominant";
            // 
            // panBlack
            // 
            this.panBlack.BackColor = System.Drawing.Color.Black;
            this.panBlack.Location = new System.Drawing.Point(672, 717);
            this.panBlack.Name = "panBlack";
            this.panBlack.Size = new System.Drawing.Size(99, 22);
            this.panBlack.TabIndex = 12;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(582, 21);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(109, 23);
            this.btnScan.TabIndex = 13;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblIsBorder
            // 
            this.lblIsBorder.AutoSize = true;
            this.lblIsBorder.Location = new System.Drawing.Point(729, 30);
            this.lblIsBorder.Name = "lblIsBorder";
            this.lblIsBorder.Size = new System.Drawing.Size(45, 13);
            this.lblIsBorder.TabIndex = 14;
            this.lblIsBorder.Text = "isBorder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(813, 816);
            this.Controls.Add(this.lblIsBorder);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.panBlack);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panWhite);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panRed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panBlue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panGreen);
            this.Controls.Add(this.lblGreenNotDominant);
            this.Controls.Add(this.panMax);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panMin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panCurrent);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panCurrent;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panMax;
        private System.Windows.Forms.Label lblGreenNotDominant;
        private System.Windows.Forms.Panel panGreen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panBlue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panRed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panWhite;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panBlack;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label lblIsBorder;
    }
}

