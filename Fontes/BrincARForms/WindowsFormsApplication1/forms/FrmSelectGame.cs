using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServicesBrincAR.services.obj;
using ServicesBrincAR;

namespace BrincAR.Forms
{
    public partial class FrmSelectGame : FrmVisualBaseMenu
    {
        private String playerName;
        public FrmSelectGame(String userName)
        {
            this.playerName = userName;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void lbMondando_Click(object sender, EventArgs e)
        {
            Game currentGame = Services.startGame(playerName, "mountingWords");

            FrmMountingWords frmMountigWords = new FrmMountingWords(playerName, currentGame);
            this.Visible = false;
            frmMountigWords.ShowDialog(this);
            this.Visible = true;
        }

        private void lbBingo_Click(object sender, EventArgs e)
        {
            Game currentGame = Services.startGame(playerName, "wordsBingo");

            this.Cursor = Cursors.WaitCursor;
            FrmWordsBingo frmWordsBingo = new FrmWordsBingo(currentGame);
            this.Visible = false;
            frmWordsBingo.ShowDialog(this);
            this.Cursor = Cursors.Default;
            this.Visible = true;
        }

        private void lbGincana_Click(object sender, EventArgs e)
        {
            Game currentGame = Services.startGame(playerName, "wordsGame");

            FrmWordsGame frmWordsGame = new FrmWordsGame(currentGame);
            this.Visible = false;
            frmWordsGame.ShowDialog(this);
            this.Visible = true;
        }

        private void lbCacando_Click(object sender, EventArgs e)
        {
            Game currentGame = Services.startGame(playerName, "searchingWords");
            FrmSearchingWords frmSearchingWords = new FrmSearchingWords(currentGame);
            this.Visible = false;
            frmSearchingWords.ShowDialog(this);
            this.Visible = true;
        }
    }
}
