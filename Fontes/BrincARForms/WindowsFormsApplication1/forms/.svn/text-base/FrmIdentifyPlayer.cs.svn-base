using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace BrincAR.Forms
{
    public partial class FrmIdentifyPlayer : Form
    {
        public FrmIdentifyPlayer()
        {
            InitializeComponent();
        }

        public void StartOSK()
        {
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            string osk = null;

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "sysnative"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "system32"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
            {
                osk = "osk.exe";
            }

            Process.Start(osk);
        }

        private void pbOk_Click(object sender, EventArgs e)
        {
            String userName = this.tbName.Text;
            FrmSelectGame frmIdentify = new FrmSelectGame(userName);
            this.Visible = false;
            frmIdentify.ShowDialog(this);
            this.Visible = true;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tbName_ChangeUICues(object sender, UICuesEventArgs e)
        {
            //String winDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //System.Diagnostics.Process p = System.Diagnostics.Process.Start(winDir + "\\osk.exe");
            this.StartOSK();
        }

        private void lbBrincAR_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog(this);
        }
    }
}
