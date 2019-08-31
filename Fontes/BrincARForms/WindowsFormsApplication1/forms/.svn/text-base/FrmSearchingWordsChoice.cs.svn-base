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
    public partial class FrmSearchingWordsChoice : FrmVisualBase
    {
        private Game currentGame;
        private Panel pnFoundObjectsView;
        public FrmSearchingWordsChoice(Game game, Panel pnFoundObjects)
        {
            this.currentGame = game;
            this.pnFoundObjectsView = pnFoundObjects;
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

        public void verifyFoundObjects(int numFoundObjects)
        {
            if (this.currentGame.NumberFoundObjects == numFoundObjects)
            {
                //para sortear as palavras e nao ficar repetindo sempre a ultima;
                String playerName = this.currentGame.PlayerName;
                String gameName = this.currentGame.GameName;
                int numberFoundObjects = this.currentGame.NumberFoundObjects;
                this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);
                this.currentGame.NumberFoundObjects = numberFoundObjects;
                FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
                this.Visible = false;
                frmWordsGame.ShowDialog(this);
                this.Visible = true;
            }
        }

        private void pbEnvelope1_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(1);

            //if (this.currentGame.NumberFoundObjects == 1)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(9);
            //if (this.currentGame.NumberFoundObjects == 9)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope2_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(2);
            //if (this.currentGame.NumberFoundObjects == 2)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope3_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(3);
            //if (this.currentGame.NumberFoundObjects == 3)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope4_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(4);
            //if (this.currentGame.NumberFoundObjects == 4)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope5_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(5);
            //if (this.currentGame.NumberFoundObjects == 5)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope6_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(6);
            //if (this.currentGame.NumberFoundObjects == 6)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope7_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(7);
            //if (this.currentGame.NumberFoundObjects == 7)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope8_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(8);
            //if (this.currentGame.NumberFoundObjects == 8)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbEnvelope10_Click(object sender, EventArgs e)
        {
            this.verifyFoundObjects(10);
            //if (this.currentGame.NumberFoundObjects == 10)
            //{
            //    //para sortear as palavras e nao ficar repetindo sempre a ultima;
            //    String playerName = this.currentGame.PlayerName;
            //    String gameName = this.currentGame.GameName;
            //    this.currentGame = ServicesBrincAR.Services.startGame(playerName, gameName);

            //    FrmWordsGame frmWordsGame = new FrmWordsGame(this.currentGame);
            //    this.Visible = false;
            //    frmWordsGame.ShowDialog(this);
            //    this.Visible = true;
            //}
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
