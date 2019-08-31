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
    public partial class FrmMountingWords : FrmBaseGame
    {
        private Game currentGame;
        public FrmMountingWords(string playerName, Game game)
        {
            currentGame = game;
            InitializeComponent();
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btTeste_Click(object sender, EventArgs e)
        {
            PictureBox pbImageTeste = new PictureBox();
            pbImageTeste.Parent = pbBlackBoard;
            pbImageTeste.Location = new System.Drawing.Point(135, 70);
            pbImageTeste.Width = 320;
            pbImageTeste.Height = 240;
            pbImageTeste.BackColor = System.Drawing.Color.Gray;
            pbImageTeste.Visible = true;
        }
    }
}
