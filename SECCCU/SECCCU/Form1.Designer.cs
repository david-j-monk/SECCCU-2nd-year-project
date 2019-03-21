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
            this.uiProgrammeComboBox = new System.Windows.Forms.ComboBox();
            this.uiModuleComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uiDateFromPicker = new System.Windows.Forms.DateTimePicker();
            this.uiDateToPicker = new System.Windows.Forms.DateTimePicker();
            this.uiDateFromDatepicker = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiScanCardButton
            // 
            this.uiScanCardButton.Location = new System.Drawing.Point(12, 12);
            this.uiScanCardButton.Name = "uiScanCardButton";
            this.uiScanCardButton.Size = new System.Drawing.Size(132, 23);
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
            this.uiCheckLoginStatusButton.Size = new System.Drawing.Size(132, 41);
            this.uiCheckLoginStatusButton.TabIndex = 2;
            this.uiCheckLoginStatusButton.Text = "Check current attendance status";
            this.uiCheckLoginStatusButton.UseVisualStyleBackColor = true;
            this.uiCheckLoginStatusButton.Click += new System.EventHandler(this.uiCheckLoginStatusButton_Click);
            // 
            // uiCheckLoginTextBox
            // 
            this.uiCheckLoginTextBox.Location = new System.Drawing.Point(12, 78);
            this.uiCheckLoginTextBox.Name = "uiCheckLoginTextBox";
            this.uiCheckLoginTextBox.Size = new System.Drawing.Size(132, 20);
            this.uiCheckLoginTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Student ID number";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(150, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 501);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(742, 472);
            this.listBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Get full report for student";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Programme";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 452);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 52);
            this.button2.TabIndex = 9;
            this.button2.Text = "Create attendance report";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // uiProgrammeComboBox
            // 
            this.uiProgrammeComboBox.FormattingEnabled = true;
            this.uiProgrammeComboBox.Location = new System.Drawing.Point(12, 261);
            this.uiProgrammeComboBox.Name = "uiProgrammeComboBox";
            this.uiProgrammeComboBox.Size = new System.Drawing.Size(132, 21);
            this.uiProgrammeComboBox.TabIndex = 10;
            this.uiProgrammeComboBox.SelectedIndexChanged += new System.EventHandler(this.uiProgrammeComboBox_indexChanged);
            // 
            // uiModuleComboBox
            // 
            this.uiModuleComboBox.FormattingEnabled = true;
            this.uiModuleComboBox.Location = new System.Drawing.Point(12, 312);
            this.uiModuleComboBox.Name = "uiModuleComboBox";
            this.uiModuleComboBox.Size = new System.Drawing.Size(132, 21);
            this.uiModuleComboBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Module";
            // 
            // uiDateFromPicker
            // 
            this.uiDateFromPicker.Location = new System.Drawing.Point(13, 371);
            this.uiDateFromPicker.Name = "uiDateFromPicker";
            this.uiDateFromPicker.Size = new System.Drawing.Size(131, 20);
            this.uiDateFromPicker.TabIndex = 13;
            // 
            // uiDateToPicker
            // 
            this.uiDateToPicker.Location = new System.Drawing.Point(12, 426);
            this.uiDateToPicker.Name = "uiDateToPicker";
            this.uiDateToPicker.Size = new System.Drawing.Size(132, 20);
            this.uiDateToPicker.TabIndex = 14;
            // 
            // uiDateFromDatepicker
            // 
            this.uiDateFromDatepicker.AutoSize = true;
            this.uiDateFromDatepicker.Location = new System.Drawing.Point(49, 355);
            this.uiDateFromDatepicker.Name = "uiDateFromDatepicker";
            this.uiDateFromDatepicker.Size = new System.Drawing.Size(56, 13);
            this.uiDateFromDatepicker.TabIndex = 15;
            this.uiDateFromDatepicker.Text = "Date From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Date To";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(930, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 20);
            this.textBox1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(930, 160);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(132, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.uiProgrammeComboBox_indexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(958, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Programme";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(958, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "First Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(945, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Add student";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(930, 104);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(958, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Surname";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(930, 202);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 35);
            this.button3.TabIndex = 6;
            this.button3.Text = "Add student";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(930, 467);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(132, 35);
            this.button4.TabIndex = 6;
            this.button4.Text = "Send report";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(930, 441);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(132, 20);
            this.textBox3.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(958, 425);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "E-mail address";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 525);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uiDateFromDatepicker);
            this.Controls.Add(this.uiDateToPicker);
            this.Controls.Add(this.uiDateFromPicker);
            this.Controls.Add(this.uiModuleComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.uiProgrammeComboBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
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
        private System.Windows.Forms.ComboBox uiProgrammeComboBox;
        private System.Windows.Forms.ComboBox uiModuleComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker uiDateFromPicker;
        private System.Windows.Forms.DateTimePicker uiDateToPicker;
        private System.Windows.Forms.Label uiDateFromDatepicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label9;
    }
}

