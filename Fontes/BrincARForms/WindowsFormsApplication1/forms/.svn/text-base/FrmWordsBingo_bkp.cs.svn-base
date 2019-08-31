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
    public partial class FrmWordsBingo_bkp : FrmBaseGame
    {
        private Game currentGame;
        private String alphabet;
        public FrmWordsBingo_bkp(Game game)
        {
            this.alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            this.currentGame = game;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void pbRaffle_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int max = alphabet.Length;
            bool contains = true;
            char raffledChar = alphabet[rand.Next(0, max)];
            while (contains)
            {
                if (this.currentGame.RaffleLetters.Contains(raffledChar))
                {
                    raffledChar = alphabet[rand.Next(0, max)];
                }
                else
                    contains = false;
            }
            
            this.lbRaffledLetters.Text += " - " + raffledChar;
            this.currentGame.RaffleLetters.Add(raffledChar);
        }
    }
}
