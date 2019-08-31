﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrincAR.Forms
{
    public partial class FrmVisualBaseMenu : Form
    {
        public FrmVisualBaseMenu()
        {
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void lbBrincAR_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog(this);
        }
    }
}
