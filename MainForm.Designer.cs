
namespace SQLiteBrowse
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
      protected override void Dispose( bool disposing )
      {
         if( disposing && ( components != null ) )
         {
            components.Dispose();
         }
         base.Dispose( disposing );
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.labelQueryInput = new System.Windows.Forms.Label();
         this.textBoxQueryInput = new System.Windows.Forms.TextBox();
         this.buttonRunQuery = new System.Windows.Forms.Button();
         this.textBoxOutput = new System.Windows.Forms.TextBox();
         this.buttonClear = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // labelQueryInput
         // 
         this.labelQueryInput.AutoSize = true;
         this.labelQueryInput.Location = new System.Drawing.Point(12, 9);
         this.labelQueryInput.Name = "labelQueryInput";
         this.labelQueryInput.Size = new System.Drawing.Size(75, 13);
         this.labelQueryInput.TabIndex = 0;
         this.labelQueryInput.Text = "Search Name:";
         // 
         // textBoxQueryInput
         // 
         this.textBoxQueryInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxQueryInput.Location = new System.Drawing.Point(93, 6);
         this.textBoxQueryInput.Name = "textBoxQueryInput";
         this.textBoxQueryInput.Size = new System.Drawing.Size(317, 20);
         this.textBoxQueryInput.TabIndex = 1;
         // 
         // buttonRunQuery
         // 
         this.buttonRunQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonRunQuery.Location = new System.Drawing.Point(416, 4);
         this.buttonRunQuery.Name = "buttonRunQuery";
         this.buttonRunQuery.Size = new System.Drawing.Size(75, 23);
         this.buttonRunQuery.TabIndex = 2;
         this.buttonRunQuery.Text = "Execute";
         this.buttonRunQuery.UseVisualStyleBackColor = true;
         // 
         // textBoxOutput
         // 
         this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxOutput.BackColor = System.Drawing.SystemColors.Window;
         this.textBoxOutput.Location = new System.Drawing.Point(12, 32);
         this.textBoxOutput.Multiline = true;
         this.textBoxOutput.Name = "textBoxOutput";
         this.textBoxOutput.ReadOnly = true;
         this.textBoxOutput.Size = new System.Drawing.Size(560, 317);
         this.textBoxOutput.TabIndex = 3;
         // 
         // buttonClear
         // 
         this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonClear.Location = new System.Drawing.Point(497, 4);
         this.buttonClear.Name = "buttonClear";
         this.buttonClear.Size = new System.Drawing.Size(75, 23);
         this.buttonClear.TabIndex = 4;
         this.buttonClear.Text = "Clear";
         this.buttonClear.UseVisualStyleBackColor = true;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(584, 361);
         this.Controls.Add(this.buttonClear);
         this.Controls.Add(this.textBoxOutput);
         this.Controls.Add(this.buttonRunQuery);
         this.Controls.Add(this.textBoxQueryInput);
         this.Controls.Add(this.labelQueryInput);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label labelQueryInput;
      private System.Windows.Forms.TextBox textBoxQueryInput;
      private System.Windows.Forms.Button buttonRunQuery;
      private System.Windows.Forms.TextBox textBoxOutput;
      private System.Windows.Forms.Button buttonClear;
   }
}

