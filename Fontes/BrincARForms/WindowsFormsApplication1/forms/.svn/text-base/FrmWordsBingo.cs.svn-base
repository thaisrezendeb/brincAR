using brincAR.brincARCore;
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
    public partial class FrmWordsBingo : FrmVisualBase
    {
        private Game currentGame;
        private String alphabet;
        private int raffledLettersCont;
        private NyARWordsBingoCore core;

        public FrmWordsBingo(Game game)
        {
            this.alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            this.currentGame = game;
            InitializeComponent();

            if (core == null)
            {
                pbCamera.BringToFront();
                pbCamera.Visible = true;
                core = new NyARWordsBingoCore(label12, currentGame, pbCamImage);
                CheckForIllegalCrossThreadCalls = false;
            }
        }

        public Image raffledLetterImage(char letter)
        {
            Image img = BrincAR.Properties.Resources.A;
            switch (letter)
            { 
                case 'A':
                    img = BrincAR.Properties.Resources.A;
                    break;

                case 'B':
                    img = BrincAR.Properties.Resources.B;
                    break;

                case 'C':
                    img = BrincAR.Properties.Resources.C;
                    break;

                case 'D':
                    img = BrincAR.Properties.Resources.D;
                    break;

                case 'E':
                    img = BrincAR.Properties.Resources.E;
                    break;

                case 'F':
                    img = BrincAR.Properties.Resources.F;
                    break;

                case 'G':
                    img = BrincAR.Properties.Resources.G;
                    break;


                case 'H':
                    img = BrincAR.Properties.Resources.H;
                    break;

                case 'I':
                    img = BrincAR.Properties.Resources.I;
                    break;

                case 'J':
                    img = BrincAR.Properties.Resources.J;
                    break;

                case 'K':
                    img = BrincAR.Properties.Resources.K;
                    break;

                case 'L':
                    img = BrincAR.Properties.Resources.L;
                    break;

                case 'M':
                    img = BrincAR.Properties.Resources.M;
                    break;

                case 'N':
                    img = BrincAR.Properties.Resources.N;
                    break;

                case 'O':
                    img = BrincAR.Properties.Resources.O;
                    break;

                case 'P':
                    img = BrincAR.Properties.Resources.P;
                    break;

                case 'Q':
                    img = BrincAR.Properties.Resources.Q;
                    break;

                case 'R':
                    img = BrincAR.Properties.Resources.R;
                    break;

                case 'S':
                    img = BrincAR.Properties.Resources.S;
                    break;

                case 'T':
                    img = BrincAR.Properties.Resources.T;
                    break;

                case 'U':
                    img = BrincAR.Properties.Resources.U;
                    break;

                case 'V':
                    img = BrincAR.Properties.Resources.V;
                    break;

                case 'W':
                    img = BrincAR.Properties.Resources.W;
                    break;

                case 'X':
                    img = BrincAR.Properties.Resources.X;
                    break;

                case 'Y':
                    img = BrincAR.Properties.Resources.Y;
                    break;

                case 'Z':
                    img = BrincAR.Properties.Resources.Z;
                    break;
            }

            return img;
        }

        private void pbRaffle_Click(object sender, EventArgs e)
        {
            if (raffledLettersCont < 26)
            {
                Random rand = new Random();
                int max = alphabet.Length;
                bool contains = true;
                char raffledChar = alphabet[rand.Next(0, max)];

                //verifica se já foram sorteadas letras
                //caso sim insere o separador " - "
                //senao, limpa o label
                if (this.currentGame.RaffleLetters.Count > 0)
                {
                    if (this.currentGame.RaffleLetters.Count == 13)
                        this.lbRaffledLetters.Text += "\n";
                    else
                        this.lbRaffledLetters.Text += " - ";
                }
                else
                    this.lbRaffledLetters.Text = "";

                while (contains)
                {
                    if (this.currentGame.RaffleLetters.Contains(raffledChar))
                    {
                        raffledChar = alphabet[rand.Next(0, max)];
                    }
                    else
                        contains = false;
                }
                //seta a imagem
                this.pbLastRaffledLetter.Image = this.raffledLetterImage(raffledChar);
                this.lbRaffledLetters.Text += raffledChar;
                this.raffledLettersCont++;
                this.currentGame.RaffleLetters.Add(raffledChar);

                //Atualiza o nyARCore com a letra sorteada.
                core.updateGame(this.currentGame);
            }
            else
                MessageBox.Show("Opa! Todas as letras já foram sorteadas!", "Opa!");
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();

            if (core != null)
                core.stopCapture();
        }

        private void pbTreasure_Click(object sender, EventArgs e)
        {
            if (this.pnWordsTreasure.Visible)
                this.pnWordsTreasure.Visible = false;
            else
            {
                this.pnWordsTreasure.Location = new System.Drawing.Point(370, 61);
                this.pnWordsTreasure.Visible = true;
                this.pnWordsTreasure.BringToFront();
            }
        }
    }
}
