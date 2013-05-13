namespace NetSecSET
{
    partial class Main
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
            this.startBut = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pValue = new System.Windows.Forms.TextBox();
            this.qValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.enTextBox = new System.Windows.Forms.TextBox();
            this.dnTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.eValue = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startBut
            // 
            this.startBut.Location = new System.Drawing.Point(16, 206);
            this.startBut.Margin = new System.Windows.Forms.Padding(4);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(197, 28);
            this.startBut.TabIndex = 0;
            this.startBut.Text = "Start";
            this.startBut.UseVisualStyleBackColor = true;
            this.startBut.Click += new System.EventHandler(this.startBut_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 242);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "Simulate Man-in-the-Middle";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pValue
            // 
            this.pValue.Location = new System.Drawing.Point(81, 19);
            this.pValue.Margin = new System.Windows.Forms.Padding(4);
            this.pValue.Name = "pValue";
            this.pValue.Size = new System.Drawing.Size(132, 22);
            this.pValue.TabIndex = 2;
            this.pValue.Text = "5";
            // 
            // qValue
            // 
            this.qValue.Location = new System.Drawing.Point(80, 51);
            this.qValue.Margin = new System.Windows.Forms.Padding(4);
            this.qValue.Name = "qValue";
            this.qValue.Size = new System.Drawing.Size(132, 22);
            this.qValue.TabIndex = 3;
            this.qValue.Text = "7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "P:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Q:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 123);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "(e, n)";
            // 
            // enTextBox
            // 
            this.enTextBox.Location = new System.Drawing.Point(80, 120);
            this.enTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.enTextBox.Name = "enTextBox";
            this.enTextBox.ReadOnly = true;
            this.enTextBox.Size = new System.Drawing.Size(132, 22);
            this.enTextBox.TabIndex = 7;
            // 
            // dnTextBox
            // 
            this.dnTextBox.Location = new System.Drawing.Point(80, 152);
            this.dnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dnTextBox.Name = "dnTextBox";
            this.dnTextBox.ReadOnly = true;
            this.dnTextBox.Size = new System.Drawing.Size(132, 22);
            this.dnTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 155);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "(d, n)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(374, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "LOG:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logBox);
            this.panel1.Location = new System.Drawing.Point(225, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 275);
            this.panel1.TabIndex = 13;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(0, -1);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(344, 273);
            this.logBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 84);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "E:";
            // 
            // eValue
            // 
            this.eValue.Location = new System.Drawing.Point(80, 81);
            this.eValue.Margin = new System.Windows.Forms.Padding(4);
            this.eValue.Name = "eValue";
            this.eValue.Size = new System.Drawing.Size(132, 22);
            this.eValue.TabIndex = 14;
            this.eValue.Text = "5";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 321);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.eValue);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dnTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.enTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.qValue);
            this.Controls.Add(this.pValue);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startBut);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "SET Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox pValue;
        private System.Windows.Forms.TextBox qValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox enTextBox;
        private System.Windows.Forms.TextBox dnTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox eValue;
    }
}

