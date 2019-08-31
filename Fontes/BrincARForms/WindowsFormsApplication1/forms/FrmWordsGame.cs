using brincAR.brincARCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServicesBrincAR;
using ServicesBrincAR.services.obj;

namespace BrincAR.Forms
{
    public partial class FrmWordsGame : FrmBaseGame
    {
        private Game currentGame;
        NyARWordsGameCore core;
        public FrmWordsGame(Game game)
        {
            this.currentGame = game;
            InitializeComponent();

            this.lbHitNumber.Text = "Que figura é esta?";
            //this.currentGame.SelectedObject = "faca";

            switch (currentGame.SelectedObject)
            {
                case "bola":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.bola11;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "gato":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.Gato1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "espada":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.espada1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "tesoura":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.tesoura1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "caneta":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.caneta1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "lapis":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.lapis1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "mesa":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.mesa1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "faca":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.faca1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "garfo":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.garfo1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "panela":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.panela1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "prato":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.prato1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "radio":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.radio1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "sapato":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.sapato1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "leao":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.leao1;
                    this.pbRaffleImage.Visible = true;                   
                    break;

                case "peixe":
                    this.pbRaffleImage.Image = BrincAR.Properties.Resources.peixe1;
                    this.pbRaffleImage.Visible = true;                   
                    break;
            }

        }


        public void createPbCamImage()
        {
            PictureBox pbCamImage = new PictureBox();
            pbCamImage.Parent = this;
            pbCamImage.Location = new System.Drawing.Point(360, 90);
            pbCamImage.Size = new Size(320, 240);
            pbCamImage.BackColor = System.Drawing.Color.Gray;
            pbCamImage.Visible = true;
            pbCamImage.BringToFront();
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();

            if (core != null)
                core.stopCapture();
        }

        private void pbContinue_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.pnHitNumber.Visible = true;
            //this.pbRaffleImage.Visible = false;
            //this.createPbCamImage();

            if (core == null)
            {
                core = new NyARWordsGameCore(lbHitNumber, currentGame, pbRaffleImage);
                CheckForIllegalCrossThreadCalls = false;
                this.lbHitNumber.Text = "";
                this.Cursor = Cursors.Default;
            }
            this.pbContinue.Visible = false;
        }
    }
}
