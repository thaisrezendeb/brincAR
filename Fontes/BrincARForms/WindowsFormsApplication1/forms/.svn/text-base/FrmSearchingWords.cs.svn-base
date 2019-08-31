using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServicesBrincAR.services.obj;

namespace BrincAR.Forms
{
    public partial class FrmSearchingWords : FrmVisualBase
    {
        private Game currentGame;
        private int segundos;
        private int minutos;
        private DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);


        public FrmSearchingWords(Game game)
        {
            this.currentGame = game;
            InitializeComponent();
            this.segundos = 60;
            this.minutos = 1;
            //this.timeGame.Start();
            this.timeGame.Start();
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void pbContinue_Click(object sender, EventArgs e)
        {
            if (this.currentGame.NumberFoundObjects == 0)
            {
                this.pnError.Visible = true;
                this.pbBack.Enabled = false;
                this.pbBackScenario.Enabled = false;
                this.pbContinue.Enabled = false;
                this.pnFoundObjects.Enabled = false;
                this.pbBack.Enabled = false;
                this.pbContinue.Enabled = false;
                this.pnScenario.Enabled = false;
            }
            else
            {
                FrmSearchingWordsCount frmSearchingWordsCount = new FrmSearchingWordsCount(this.currentGame, this.pnFoundObjects);
                this.Visible = false;
                frmSearchingWordsCount.ShowDialog(this);
                this.Visible = true;
                //FrmSearchingWordsCount frmSearchingWordsCount = new FrmSearchingWordsCount();
                //frmSearchingWordsCount.Parent = this.Parent;
                //frmSearchingWordsCount.Show();
                //this.Dispose();
                //this.Close();
            }
        }

        private void setPositionObjectsToFind()
        {
            // 
            // pbImage1
            // 
            this.pbImage1.Image = global::BrincAR.Properties.Resources.bola1;
            this.pbImage1.Location = new System.Drawing.Point(521, 369);
            this.pbImage1.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage2
            // 
            this.pbImage2.Image = global::BrincAR.Properties.Resources.bolo;
            this.pbImage2.Location = new System.Drawing.Point(283, 272);
            this.pbImage2.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage3
            // 
            this.pbImage3.Image = global::BrincAR.Properties.Resources.boneca;
            this.pbImage3.Location = new System.Drawing.Point(3, 369);
            this.pbImage3.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage4
            // 
            this.pbImage4.Image = global::BrincAR.Properties.Resources.cachorro;
            this.pbImage4.Location = new System.Drawing.Point(413, 241);
            this.pbImage4.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage5
            // 
            this.pbImage5.Image = global::BrincAR.Properties.Resources.copo;
            this.pbImage5.Location = new System.Drawing.Point(356, 139);
            this.pbImage5.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage6
            // 
            this.pbImage6.Image = global::BrincAR.Properties.Resources.faca;
            this.pbImage6.Location = new System.Drawing.Point(193, 162);
            this.pbImage6.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage7
            // 
            this.pbImage7.Image = global::BrincAR.Properties.Resources.garfo;
            this.pbImage7.Location = new System.Drawing.Point(187, 260);
            this.pbImage7.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage8
            // 
            this.pbImage8.Image = global::BrincAR.Properties.Resources.maca;
            this.pbImage8.Location = new System.Drawing.Point(258, 356);
            this.pbImage8.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage9
            // 
            this.pbImage9.Image = global::BrincAR.Properties.Resources.panela;
            this.pbImage9.Location = new System.Drawing.Point(111, 225);
            this.pbImage9.Size = new System.Drawing.Size(76, 78);
            // 
            // pbImage10
            // 
            this.pbImage10.Image = global::BrincAR.Properties.Resources.peixe;
            this.pbImage10.Location = new System.Drawing.Point(19, 194);
            this.pbImage10.Size = new System.Drawing.Size(76, 78);
        }

        private void pbImage1_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage1.Image);
            this.pbFoundObj1.Image = this.pbImage1.Image;
            this.pbImage1.Visible = false;
        }

        private void pbImage2_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage2.Image);
            this.pbFoundObj2.Image = this.pbImage2.Image;
            this.pbImage2.Visible = false;
        }

        private void pbImage3_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage3.Image);
            this.pbFoundObj3.Image = this.pbImage3.Image;
            this.pbImage3.Visible = false;
        }

        private void pbImage4_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage4.Image);
            this.pbFoundObj4.Image = this.pbImage4.Image;
            this.pbImage4.Visible = false;
        }

        private void pbImage5_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage5.Image);
            this.pbFoundObj5.Image = this.pbImage5.Image;
            this.pbImage5.Visible = false;
        }

        private void pbImage6_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage6.Image);
            this.pbFoundObj6.Image = this.pbImage6.Image;
            this.pbImage6.Visible = false;
        }

        private void pbImage7_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage7.Image);
            this.pbFoundObj7.Image = this.pbImage7.Image;
            this.pbImage7.Visible = false;
        }

        private void pbImage8_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage8.Image);
            this.pbFoundObj8.Image = this.pbImage8.Image;
            this.pbImage8.Visible = false;
        }

        private void pbImage9_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage9.Image);
            this.pbFoundObj9.Image = this.pbImage9.Image;
            this.pbImage9.Visible = false;

        }

        private void pbImage10_Click(object sender, EventArgs e)
        {
            this.currentGame.NumberFoundObjects++;
            this.currentGame.foundObjects.Add(this.pbImage10.Image);
            this.pbFoundObj10.Image = this.pbImage10.Image;
            this.pbImage10.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pnError.Visible = false;
            this.pbBack.Enabled = true;
            this.pbBackScenario.Enabled = true;
            this.pbContinue.Enabled = true;
            this.pnFoundObjects.Enabled = true;
            this.pbBack.Enabled = true;
            this.pbContinue.Enabled = true;
            this.pnScenario.Enabled = true;
        }

        private void timeGame_Tick(object sender, EventArgs e)
        {
            if (this.segundos > 0)
            {
                this.segundos--;
                if(this.segundos > 9)
                    lbTime.Text = "0" + minutos.ToString() + ":" + segundos.ToString();
                else
                    lbTime.Text = "0" + minutos.ToString() + ":0" + segundos.ToString();
            }
            else
            {
                if (minutos > 0)
                {
                    this.minutos--;
                    this.segundos = 59;
                    if (this.segundos > 9)
                        lbTime.Text = "0" + minutos.ToString() + ":" + segundos.ToString();
                    else
                        lbTime.Text = "0" + minutos.ToString() + ":0" + segundos.ToString();
                }
                else
                {
                    this.timeGame.Stop();
                    this.pbContinue_Click(this, null);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.pnError.Visible = false;
            this.pbBack.Enabled = true;
            this.pbBackScenario.Enabled = true;
            this.pbContinue.Enabled = true;
            this.pnFoundObjects.Enabled = true;
            this.pbBack.Enabled = true;
            this.pbContinue.Enabled = true;
            this.pnScenario.Enabled = true;
        }
    }
}
