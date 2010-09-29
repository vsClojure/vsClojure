namespace Microsoft.ClojureExtension.Configuration
{
    partial class FrameworkOptions
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.frameworkNameTextBox = new System.Windows.Forms.TextBox();
            this.frameworkLocationTextBox = new System.Windows.Forms.TextBox();
            this.frameworkList = new System.Windows.Forms.ListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FindLocationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // frameworkNameTextBox
            // 
            this.frameworkNameTextBox.Location = new System.Drawing.Point(62, 18);
            this.frameworkNameTextBox.Name = "frameworkNameTextBox";
            this.frameworkNameTextBox.Size = new System.Drawing.Size(266, 20);
            this.frameworkNameTextBox.TabIndex = 0;
            // 
            // frameworkLocationTextBox
            // 
            this.frameworkLocationTextBox.Location = new System.Drawing.Point(62, 45);
            this.frameworkLocationTextBox.Name = "frameworkLocationTextBox";
            this.frameworkLocationTextBox.Size = new System.Drawing.Size(233, 20);
            this.frameworkLocationTextBox.TabIndex = 1;
            // 
            // frameworkList
            // 
            this.frameworkList.FormattingEnabled = true;
            this.frameworkList.Location = new System.Drawing.Point(62, 71);
            this.frameworkList.Name = "frameworkList";
            this.frameworkList.Size = new System.Drawing.Size(266, 173);
            this.frameworkList.TabIndex = 2;
            this.frameworkList.SelectedIndexChanged += new System.EventHandler(this.frameworkList_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(62, 250);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(253, 250);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 4;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-1, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Location";
            // 
            // FindLocationButton
            // 
            this.FindLocationButton.Location = new System.Drawing.Point(301, 43);
            this.FindLocationButton.Name = "FindLocationButton";
            this.FindLocationButton.Size = new System.Drawing.Size(27, 23);
            this.FindLocationButton.TabIndex = 7;
            this.FindLocationButton.UseVisualStyleBackColor = true;
            // 
            // FrameworkOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FindLocationButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.frameworkList);
            this.Controls.Add(this.frameworkLocationTextBox);
            this.Controls.Add(this.frameworkNameTextBox);
            this.Name = "FrameworkOptions";
            this.Size = new System.Drawing.Size(370, 292);
            this.Load += new System.EventHandler(this.FrameworkOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox frameworkNameTextBox;
        private System.Windows.Forms.TextBox frameworkLocationTextBox;
        private System.Windows.Forms.ListBox frameworkList;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FindLocationButton;
    }
}
