namespace BrincAR.Forms
{
    partial class FrmWordsBingo_bkp
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbRaffledLetters = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbRaffle = new System.Windows.Forms.PictureBox();
            this.pbLastRaffledLetter = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlackBoard)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRaffle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLastRaffledLetter)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbRaffledLetters);
            this.panel1.Location = new System.Drawing.Point(77, 457);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 100);
            this.panel1.TabIndex = 9;
            // 
            // lbRaffledLetters
            // 
            this.lbRaffledLetters.AutoSize = true;
            this.lbRaffledLetters.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRaffledLetters.Location = new System.Drawing.Point(6, 33);
            this.lbRaffledLetters.Name = "lbRaffledLetters";
            this.lbRaffledLetters.Size = new System.Drawing.Size(113, 34);
            this.lbRaffledLetters.TabIndex = 0;
            this.lbRaffledLetters.Text = "label12";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::BrincAR.Properties.Resources.voltar;
            this.pictureBox1.Location = new System.Drawing.Point(797, 575);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 90);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pbRaffle
            // 
            this.pbRaffle.BackColor = System.Drawing.Color.Coral;
            this.pbRaffle.Location = new System.Drawing.Point(668, 575);
            this.pbRaffle.Name = "pbRaffle";
            this.pbRaffle.Size = new System.Drawing.Size(90, 90);
            this.pbRaffle.TabIndex = 11;
            this.pbRaffle.TabStop = false;
            this.pbRaffle.Click += new System.EventHandler(this.pbRaffle_Click);
            // 
            // pbLastRaffledLetter
            // 
            this.pbLastRaffledLetter.Location = new System.Drawing.Point(5, 147);
            this.pbLastRaffledLetter.Name = "pbLastRaffledLetter";
            this.pbLastRaffledLetter.Size = new System.Drawing.Size(200, 200);
            this.pbLastRaffledLetter.TabIndex = 12;
            this.pbLastRaffledLetter.TabStop = false;
            // 
            // FrmWordsBingo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 672);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbRaffle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbLastRaffledLetter);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmWordsBingo";
            this.Text = "FrmWordsBingo";
            this.Controls.SetChildIndex(this.pbLastRaffledLetter, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.pbRaffle, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.pbBlackBoard, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbBlackBoard)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRaffle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLastRaffledLetter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbRaffledLetters;
        private System.Windows.Forms.PictureBox pbRaffle;
        private System.Windows.Forms.PictureBox pbLastRaffledLetter;
    }
}