using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrincAR.Forms
{
    public partial class FrmBaseGame : Form
    {
        public FrmBaseGame()
        {
            InitializeComponent();
        }

        private void pbTreasure_Click(object sender, EventArgs e)
        {
            if (this.pnWordsTreasure.Visible)
                this.pnWordsTreasure.Visible = false;
            else
            {
                this.pnWordsTreasure.Visible = true;
                this.pnWordsTreasure.BringToFront();
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void lbBrincar_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog(this);
        }

        private void FrmBaseGame_Load(object sender, EventArgs e)
        {

        }
    }
}
