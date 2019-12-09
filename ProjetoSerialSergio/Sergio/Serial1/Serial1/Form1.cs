using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serial1
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnLigar_Click(object sender, EventArgs e)
        {
            try
            {
                // ligar porta serial
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = cbxPorta.Text;
                    serialPort1.Open();
                    panel1.BackColor = Color.LawnGreen;
                    panel1.Enabled = true;
                    btnLigar.Text = "Close";
                    cbxPorta.Enabled = false;
                }
                else
                {
                    serialPort1.Close();
                    panel1.BackColor = Color.MediumVioletRed;
                    panel1.Enabled = false;
                    btnLigar.Text = "Open";
                    cbxPorta.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir a porta serial");
            }


        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();

            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate {
                    txtIn.AppendText(inData);
                }));
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtOut.Text != "")
                serialPort1.Write(txtOut.Text);
            txtOut.Text = "";
        }
    }
}
