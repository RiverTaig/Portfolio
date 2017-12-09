namespace PuzzlePalServer
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.numAnchor = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numRPM = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCalls = new System.Windows.Forms.Label();
            this.lblVertexCount = new System.Windows.Forms.Label();
            this.lblXY = new System.Windows.Forms.Label();
            this.lblDegXY = new System.Windows.Forms.Label();
            this.numAngle = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numAnchor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // numAnchor
            // 
            this.numAnchor.Location = new System.Drawing.Point(101, 50);
            this.numAnchor.Name = "numAnchor";
            this.numAnchor.Size = new System.Drawing.Size(71, 20);
            this.numAnchor.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Anchor Vertex:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Speed";
            // 
            // numRPM
            // 
            this.numRPM.Location = new System.Drawing.Point(232, 50);
            this.numRPM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRPM.Name = "numRPM";
            this.numRPM.Size = new System.Drawing.Size(71, 20);
            this.numRPM.TabIndex = 3;
            this.numRPM.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRPM.ValueChanged += new System.EventHandler(this.numRPM_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(433, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Press and hold left mouse button while moving mouse to create a shape with 100 ve" +
    "rticies.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Modify Anchor Vertex and RPM to affect rotation speed.";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCalls
            // 
            this.lblCalls.AutoSize = true;
            this.lblCalls.Location = new System.Drawing.Point(325, 56);
            this.lblCalls.Name = "lblCalls";
            this.lblCalls.Size = new System.Drawing.Size(13, 13);
            this.lblCalls.TabIndex = 7;
            this.lblCalls.Text = "0";
            // 
            // lblVertexCount
            // 
            this.lblVertexCount.AutoSize = true;
            this.lblVertexCount.Location = new System.Drawing.Point(378, 52);
            this.lblVertexCount.Name = "lblVertexCount";
            this.lblVertexCount.Size = new System.Drawing.Size(77, 13);
            this.lblVertexCount.TabIndex = 8;
            this.lblVertexCount.Text = "Anchor Vertex:";
            // 
            // lblXY
            // 
            this.lblXY.AutoSize = true;
            this.lblXY.Location = new System.Drawing.Point(378, 570);
            this.lblXY.Name = "lblXY";
            this.lblXY.Size = new System.Drawing.Size(35, 13);
            this.lblXY.TabIndex = 10;
            this.lblXY.Text = "label5";
            // 
            // lblDegXY
            // 
            this.lblDegXY.AutoSize = true;
            this.lblDegXY.Location = new System.Drawing.Point(18, 560);
            this.lblDegXY.Name = "lblDegXY";
            this.lblDegXY.Size = new System.Drawing.Size(51, 13);
            this.lblDegXY.TabIndex = 11;
            this.lblDegXY.Text = "lblDegXY";
            // 
            // numAngle
            // 
            this.numAngle.Location = new System.Drawing.Point(335, 88);
            this.numAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numAngle.Name = "numAngle";
            this.numAngle.Size = new System.Drawing.Size(120, 20);
            this.numAngle.TabIndex = 12;
            this.numAngle.ValueChanged += new System.EventHandler(this.numAngle_ValueChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 592);
            this.Controls.Add(this.numAngle);
            this.Controls.Add(this.lblDegXY);
            this.Controls.Add(this.lblXY);
            this.Controls.Add(this.lblVertexCount);
            this.Controls.Add(this.lblCalls);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numRPM);
            this.Controls.Add(this.numAnchor);
            this.Name = "Form2";
            this.Text = "Shape Rotator";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.numAnchor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numAnchor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRPM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCalls;
        private System.Windows.Forms.Label lblVertexCount;
        private System.Windows.Forms.Label lblXY;
        private System.Windows.Forms.Label lblDegXY;
        private System.Windows.Forms.NumericUpDown numAngle;
    }
}