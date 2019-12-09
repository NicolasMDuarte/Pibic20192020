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

namespace apSerial
{
    public partial class frmSerial : Form
    {
        public frmSerial()
        {
            InitializeComponent();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            string inData = sp.ReadExisting();

            if(InvokeRequired)
            {
                Invoke
                    (
                        new MethodInvoker
                        (
                            delegate
                            {
                                txtReceive.AppendText(inData);
                            }
                        )
                    );
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                btnOpen.Text = "Open";
                panel1.BackColor = Color.Green;
                panel1.Enabled = false;
                cbPort.Enabled = true;
            }
            else
            {
                serialPort1.PortName = cbPort.Text;
                serialPort1.Open();
                btnOpen.Text = "Close";
                panel1.BackColor = Color.Red;
                cbPort.Enabled = false;
                panel1.Enabled = true;
            }
        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            btnOpen.Text = "Open";
            panel1.BackColor = Color.Green;
            cbPort.Enabled = true;
            panel1.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            String msgSend = txtSend.Text;

            if (msgSend != "")
            {
                serialPort1.Write(msgSend);
            }
        }
    }
}
