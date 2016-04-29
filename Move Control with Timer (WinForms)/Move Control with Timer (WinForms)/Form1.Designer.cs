namespace Move_Control_with_Timer__WinForms_
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
            this.components = new System.ComponentModel.Container();
            this.buttonHorizontal = new System.Windows.Forms.Button();
            this.buttonVertical = new System.Windows.Forms.Button();
            this.timerHorizontal = new System.Windows.Forms.Timer(this.components);
            this.timerVertical = new System.Windows.Forms.Timer(this.components);
            this.buttonDiagonal = new System.Windows.Forms.Button();
            this.timerDiagonal = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonHorizontal
            // 
            this.buttonHorizontal.Location = new System.Drawing.Point(45, 72);
            this.buttonHorizontal.Name = "buttonHorizontal";
            this.buttonHorizontal.Size = new System.Drawing.Size(75, 23);
            this.buttonHorizontal.TabIndex = 0;
            this.buttonHorizontal.Text = "Horizontal";
            this.buttonHorizontal.UseVisualStyleBackColor = true;
            this.buttonHorizontal.Click += new System.EventHandler(this.buttonHorizontal_Click);
            // 
            // buttonVertical
            // 
            this.buttonVertical.Location = new System.Drawing.Point(125, 128);
            this.buttonVertical.Name = "buttonVertical";
            this.buttonVertical.Size = new System.Drawing.Size(75, 23);
            this.buttonVertical.TabIndex = 0;
            this.buttonVertical.Text = "Vertical";
            this.buttonVertical.UseVisualStyleBackColor = true;
            this.buttonVertical.Click += new System.EventHandler(this.buttonVertical_Click);
            // 
            // timerHorizontal
            // 
            this.timerHorizontal.Interval = 10;
            this.timerHorizontal.Tick += new System.EventHandler(this.timerHorizontal_Tick);
            // 
            // timerVertical
            // 
            this.timerVertical.Interval = 10;
            this.timerVertical.Tick += new System.EventHandler(this.timerVertical_Tick);
            // 
            // buttonDiagonal
            // 
            this.buttonDiagonal.Location = new System.Drawing.Point(164, 157);
            this.buttonDiagonal.Name = "buttonDiagonal";
            this.buttonDiagonal.Size = new System.Drawing.Size(75, 23);
            this.buttonDiagonal.TabIndex = 0;
            this.buttonDiagonal.Text = "Diagonal";
            this.buttonDiagonal.UseVisualStyleBackColor = true;
            this.buttonDiagonal.Click += new System.EventHandler(this.buttonDiagonal_Click);
            // 
            // timerDiagonal
            // 
            this.timerDiagonal.Interval = 10;
            this.timerDiagonal.Tick += new System.EventHandler(this.timerDiagonal_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonDiagonal);
            this.Controls.Add(this.buttonVertical);
            this.Controls.Add(this.buttonHorizontal);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHorizontal;
        private System.Windows.Forms.Button buttonVertical;
        private System.Windows.Forms.Timer timerHorizontal;
        private System.Windows.Forms.Timer timerVertical;
        private System.Windows.Forms.Button buttonDiagonal;
        private System.Windows.Forms.Timer timerDiagonal;
    }
}

