namespace Server
{
    partial class MainForm
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
            this.protoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.Output = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.sendButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.watchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // protoRichTextBox
            // 
            this.protoRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.protoRichTextBox.Location = new System.Drawing.Point(12, 29);
            this.protoRichTextBox.Name = "protoRichTextBox";
            this.protoRichTextBox.Size = new System.Drawing.Size(507, 284);
            this.protoRichTextBox.TabIndex = 0;
            this.protoRichTextBox.Text = "";
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.Location = new System.Drawing.Point(12, 319);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(507, 312);
            this.Output.TabIndex = 1;
            this.Output.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = ".proto spec";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(525, 29);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(242, 570);
            this.propertyGrid1.TabIndex = 3;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(688, 605);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(79, 26);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // watchButton
            // 
            this.watchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.watchButton.Location = new System.Drawing.Point(607, 608);
            this.watchButton.Name = "watchButton";
            this.watchButton.Size = new System.Drawing.Size(75, 23);
            this.watchButton.TabIndex = 5;
            this.watchButton.Text = "Watch";
            this.watchButton.UseVisualStyleBackColor = true;
            this.watchButton.Click += new System.EventHandler(this.watchButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 643);
            this.Controls.Add(this.watchButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.protoRichTextBox);
            this.Name = "MainForm";
            this.Text = "Test Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox protoRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.Button watchButton;
    }
}

