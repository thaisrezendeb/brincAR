﻿namespace BrincAR.Forms
{
    partial class FrmVisualBaseMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisualBaseMenu));
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.pbBlackboard = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlackboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbClose.Image = global::BrincAR.Properties.Resources.sair_90_90;
            this.pbClose.Location = new System.Drawing.Point(906, 572);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(90, 90);
            this.pbClose.TabIndex = 5;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // pbBlackboard
            // 
            this.pbBlackboard.BackgroundImage = global::BrincAR.Properties.Resources.gameMenuBackground;
            this.pbBlackboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbBlackboard.Location = new System.Drawing.Point(91, 3);
            this.pbBlackboard.Name = "pbBlackboard";
            this.pbBlackboard.Size = new System.Drawing.Size(821, 561);
            this.pbBlackboard.TabIndex = 8;
            this.pbBlackboard.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BrincAR.Properties.Resources.logomini;
            this.pictureBox1.Location = new System.Drawing.Point(12, 570);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(217, 80);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // FrmVisualBaseMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BrincAR.Properties.Resources.formBackground;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbBlackboard);
            this.Controls.Add(this.pbClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "FrmVisualBaseMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "brincAR";
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlackboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.PictureBox pbBlackboard;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}