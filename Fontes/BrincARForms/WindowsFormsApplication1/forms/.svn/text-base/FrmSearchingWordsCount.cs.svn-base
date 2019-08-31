using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServicesBrincAR.services.obj;
using System.Collections;

namespace BrincAR.Forms
{
    public partial class FrmSearchingWordsCount : FrmVisualBase
    {
        private Game currentGame;
        public FrmSearchingWordsCount(Game game, Panel pnFoundObjects)
        {
            this.currentGame = game;
            InitializeComponent();

            List<PictureBox> listFoundImages = new List<PictureBox>();
            listFoundImages.Add(this.pbFoundObj1);
            listFoundImages.Add(this.pbFoundObj2);
            listFoundImages.Add(this.pbFoundObj3);
            listFoundImages.Add(this.pbFoundObj4);
            listFoundImages.Add(this.pbFoundObj5);
            listFoundImages.Add(this.pbFoundObj6);
            listFoundImages.Add(this.pbFoundObj7);
            listFoundImages.Add(this.pbFoundObj8);
            listFoundImages.Add(this.pbFoundObj9);
            listFoundImages.Add(this.pbFoundObj10);

            for (int i = 0; i < this.currentGame.foundObjects.Count; i++)
            {
                listFoundImages[i].Image = this.currentGame.foundObjects[i];
            }
        }

        private void pbContinue_Click(object sender, EventArgs e)
        {
            FrmSearchingWordsChoice frmSearchingWordsChoice = new FrmSearchingWordsChoice(currentGame, this.pnFoundObjects);
            this.Visible = false;
            frmSearchingWordsChoice.ShowDialog(this);
            this.Visible = true;
            //FrmMountingWords frmMountingWords = new FrmMountingWords();
            //frmMountingWords.Parent = this.Parent;
            //frmMountingWords.Show();
            //this.Dispose();
            //this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
