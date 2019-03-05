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
            this.SuspendLayout();
            // 
            // uiScanCardButton
            // 
            this.uiScanCardButton.Location = new System.Drawing.Point(12, 12);
            this.uiScanCardButton.Name = "uiScanCardButton";
            this.uiScanCardButton.Size = new System.Drawing.Size(75, 23);
            this.uiScanCardButton.TabIndex = 0;
            this.uiScanCardButton.Text = "Scan Card";
            this.uiScanCardButton.UseVisualStyleBackColor = true;
            this.uiScanCardButton.Click += new System.EventHandler(this.uiScanCardButton_click);
            // 
            // uiCardNumberLabel
            // 
            this.uiCardNumberLabel.AutoSize = true;
            this.uiCardNumberLabel.Location = new System.Drawing.Point(93, 17);
            this.uiCardNumberLabel.Name = "uiCardNumberLabel";
            this.uiCardNumberLabel.Size = new System.Drawing.Size(0, 13);
            this.uiCardNumberLabel.TabIndex = 1;
            // 
            // uiCheckLoginStatusButton
            // 
            this.uiCheckLoginStatusButton.Location = new System.Drawing.Point(13, 42);
            this.uiCheckLoginStatusButton.Name = "uiCheckLoginStatusButton";
            this.uiCheckLoginStatusButton.Size = new System.Drawing.Size(75, 41);
            this.uiCheckLoginStatusButton.TabIndex = 2;
            this.uiCheckLoginStatusButton.Text = "Check Log In Status";
            this.uiCheckLoginStatusButton.UseVisualStyleBackColor = true;
            this.uiCheckLoginStatusButton.Click += new System.EventHandler(this.uiCheckLoginStatusButton_Click);
            // 
            // uiCheckLoginTextBox
            // 
            this.uiCheckLoginTextBox.Location = new System.Drawing.Point(96, 56);
            this.uiCheckLoginTextBox.Name = "uiCheckLoginTextBox";
            this.uiCheckLoginTextBox.Size = new System.Drawing.Size(100, 20);
            this.uiCheckLoginTextBox.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiCheckLoginTextBox);
            this.Controls.Add(this.uiCheckLoginStatusButton);
            this.Controls.Add(this.uiCardNumberLabel);
            this.Controls.Add(this.uiScanCardButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uiScanCardButton;
        private System.Windows.Forms.Label uiCardNumberLabel;
        private System.Windows.Forms.Button uiCheckLoginStatusButton;
        private System.Windows.Forms.TextBox uiCheckLoginTextBox;
    }
}

