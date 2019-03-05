namespace SECCCU
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
            this.uiScanCardButton = new System.Windows.Forms.Button();
            this.uiCardNumberLabel = new System.Windows.Forms.Label();
            this.uiCheckLoginStatusButton = new System.Windows.Forms.Button();
            this.uiCheckLoginTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiScanCardButton
            // 
            this.uiScanCardButton.Location = new System.Drawing.Point(12, 12);
            this.uiScanCardButton.Name = "uiScanCardButton";
            this.uiScanCardButton.Size = new System.Drawing.Size(111, 23);
            this.uiScanCardButton.TabIndex = 0;
            this.uiScanCardButton.Text = "Trigger Card scan";
            this.uiScanCardButton.UseVisualStyleBackColor = true;
            this.uiScanCardButton.Click += new System.EventHandler(this.uiScanCardButton_click);
            // 
            // uiCardNumberLabel
            // 
            this.uiCardNumberLabel.AutoSize = true;
            this.uiCardNumberLabel.Location = new System.Drawing.Point(17, 34);
            this.uiCardNumberLabel.Name = "uiCardNumberLabel";
            this.uiCardNumberLabel.Size = new System.Drawing.Size(0, 13);
            this.uiCardNumberLabel.TabIndex = 1;
            // 
            // uiCheckLoginStatusButton
            // 
            this.uiCheckLoginStatusButton.Location = new System.Drawing.Point(12, 104);
            this.uiCheckLoginStatusButton.Name = "uiCheckLoginStatusButton";
            this.uiCheckLoginStatusButton.Size = new System.Drawing.Size(111, 41);
            this.uiCheckLoginStatusButton.TabIndex = 2;
            this.uiCheckLoginStatusButton.Text = "Check current attendance status";
            this.uiCheckLoginStatusButton.UseVisualStyleBackColor = true;
            this.uiCheckLoginStatusButton.Click += new System.EventHandler(this.uiCheckLoginStatusButton_Click);
            // 
            // uiCheckLoginTextBox
            // 
            this.uiCheckLoginTextBox.Location = new System.Drawing.Point(12, 78);
            this.uiCheckLoginTextBox.Name = "uiCheckLoginTextBox";
            this.uiCheckLoginTextBox.Size = new System.Drawing.Size(111, 20);
            this.uiCheckLoginTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Student ID number";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(129, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(957, 501);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(944, 472);
            this.listBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Get full report for student";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Programme ID";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 52);
            this.button2.TabIndex = 9;
            this.button2.Text = "Get attendance report for programme";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 215);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(111, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 525);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiCheckLoginTextBox);
            this.Controls.Add(this.uiCheckLoginStatusButton);
            this.Controls.Add(this.uiCardNumberLabel);
            this.Controls.Add(this.uiScanCardButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uiScanCardButton;
        private System.Windows.Forms.Label uiCardNumberLabel;
        private System.Windows.Forms.Button uiCheckLoginStatusButton;
        private System.Windows.Forms.TextBox uiCheckLoginTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

