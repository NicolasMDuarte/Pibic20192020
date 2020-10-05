using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Threading.Tasks;

namespace apSerial
{
    public partial class frmSerial : Form
    {
        private string RxString; // declarada como atributo da classe

        public frmSerial()
        {
            InitializeComponent();
            tmrTempo.Enabled = true;
        }

        private void tmrTempo_Tick(object sender, EventArgs e)
        {
           // atualizaListaCOMs();
        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            //alo
        }

        private void atualizaListaCOMs()
        {
            int i = 0;
            bool quantDiferente; //flag para sinalizar se
                                 //a quantidade de portas mudou
            quantDiferente = false;
            //se a quantidade de portas mudou
            if (cbxPortas.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (!cbxPortas.Items[i++].Equals(s))
                    {
                        quantDiferente = true;
                        break; // escapa do foreach
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }
            //Se não foi detectado diferença
            if (!quantDiferente)
            {
                return; //retorna
            }
            //limpa comboBox
            cbxPortas.Items.Clear();
            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                cbxPortas.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            cbxPortas.SelectedIndex = 0;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (!spPorta.IsOpen) // Porta fechada
            {
                try
                {
                    spPorta.PortName = cbxPortas.Items[cbxPortas.SelectedIndex].ToString();
                    spPorta.Open();
                }
                catch
                {
                    return;
                }
                if (spPorta.IsOpen) // abriu!!
                {
                    btnConectar.Text = "Desconectar";
                    cbxPortas.Enabled = false;
                   // pnConectar.BackColor = Color.LightGreen;
                    //AtivarBotoes();
                    //BotoesOriginais();
                    //lbMensagem.Text = "Mensagem: Conectado - Clique em um botão para selecionar o efeito";
                }
            }
            else
            {
                try
                {
                    //lbMensagem.Text = "Mensagem: Desconectado";
                    spPorta.Close();
                    cbxPortas.Enabled = true;
                    btnConectar.Text = "Conectar";
                   // DesativarBotoes();
                }
                catch
                {
                    return;
                }
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (spPorta.IsOpen) //porta está aberta
                spPorta.Write(txtEnviar.Text); //envia o texto digitado no textbox 
        }

        private void escreveDadoRecebido(object sender, EventArgs e)
        {
            txtReceber.AppendText(RxString);
        }

        private void spSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //le o dado disponível na serial
            RxString = spPorta.ReadExisting();
            //chama outra thread para escrever o dado no text box
            this.Invoke(new EventHandler(escreveDadoRecebido));
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            dlgAbrir.Title = "Selecione o arquivo com os dados coletados do drone";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                lbArqImportado.Text = "Arquivo selecionado: " + dlgAbrir.FileName + ".txt";
                LerDados(dlgAbrir.FileName);
            }
        }

        private void LerDados(string nomeArq)
        {
            var arq = new StreamReader(nomeArq, System.Text.Encoding.UTF7);
            int linha = 0;
            while (!arq.EndOfStream)
            {
                string linhaLida = arq.ReadLine();
                var dados = linhaLida.Split('\t');
                dgv.Rows.Add(dados);
                //for(int i=0; i<dados.Length; i++)
                //{
                //    dgv[linha,i].Value = dados[i];
                //}
                linha++;
            }
            //dgv.Rows.RemoveAt(linha);
            //a   b   c
            //d   e  f
            arq.Close();
        }

        private void btnGerarExcel_Click(object sender, EventArgs e)
        {
            if (txtArquivoExcel.Text == null)
            {
                MessageBox.Show("Insira o nome que deseja para o arquivo Excel!", "Preencha os campos", MessageBoxButtons.OK);
                return;
            }
            if (dgv[0, 0].Value == null)
            {
                MessageBox.Show("Nenhum dado foi importado ainda.\n Importe via Serial ou via Arquivo Texto.", "Importe os dados", MessageBoxButtons.OK);
                return;
            }
            if (dgv.Rows.Count <= 0)
            {
                MessageBox.Show("Nenhum dado foi importado ainda.\n Importe via Serial ou via Arquivo Texto.", "Importe os dados", MessageBoxButtons.OK);
                return;
            }

            try
            {
                // CRIA O ARQUIVO
                SaveFileDialog salvar = new SaveFileDialog(); // novo SaveFileDialog

                Excel.Application App;
                Excel.Workbook WorkBook;
                Excel.Worksheet WorkSheet;
                object misValue = System.Reflection.Missing.Value;

                App = new Excel.Application();
                WorkBook = App.Workbooks.Add(misValue);

                WorkSheet = (Excel.Worksheet)WorkBook.Worksheets.get_Item(1);

                // COLOCA AS COISA NO EXCEL
                for (int i = 0; i < dgv.Columns.Count - 1; i++)
                {
                    WorkSheet.Cells[0, i] = dgv.Columns[i].HeaderText;
                    Console.Write(WorkSheet.Cells[i + 1, 0].Value);
                }

                for (int i = 0; i <= dgv.RowCount - 1; i++)
                {
                    for (int j = 0; j <= dgv.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dgv[j, i];
                        WorkSheet.Cells[i + 1, j + 1] = cell.Value;
                    }
                }

                //try
                //{
                //    XcelApp.Application.Workbooks.Add(Type.Missing);
                //    for (int i = 1; i < dgv.Columns.Count + 1; i++)
                //    {
                //        XcelApp.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                //    }
                //    for (int i = 0; i < dgv.Rows.Count - 1; i++)
                //    {
                //        for (int j = 0; j < dgv.Columns.Count; j++)
                //        {
                //            XcelApp.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                //        }
                //    }
                //    XcelApp.Columns.AutoFit();
                //    XcelApp.Visible = true;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Erro : " + ex.Message);
                //    XcelApp.Quit();
                //}

                // E SALVA
                salvar.Title = "Exportar para Excel";
                salvar.Filter = "Arquivo do Excel *.xls | *.xls";
                salvar.ShowDialog(); // mostra

                WorkBook.SaveAs(salvar.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                WorkBook.Close(true, misValue, misValue);
                App.Quit(); // encerra o excel

                MessageBox.Show("Exportado com sucesso!");

                liberarObjetos(WorkSheet);
                liberarObjetos(WorkBook);
                liberarObjetos(App);

                //MessageBox.Show("O arquivo Excel foi criado com sucesso. Você pode encontrá-lo na sua Área de Trabalho, com o nome: " + txtArquivoExcel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            // cria o nosso arquivo vazio e cria a pasta3 com os dados
            // o excel ta indo pra ca C:\Users\giova\Documents
            // tentar trocar o design da tabela
        }

        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Ocorreu um erro durante a liberação do objeto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void spPorta_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = spPorta.ReadExisting();
            //chama outra thread para escrever o dado no text box
            if (RxString == "terminou\r\n")
            {
                this.Invoke(new EventHandler(Terminou));
            }
        }
        
        public void Terminou(object sender, EventArgs e)
        {
            //BotoesOriginais();
            //AtivarBotoes();
            //lbMensagem.Text = "Mensagem: Disponível - Clique em um botão para selecionar o efeito";
        }

        private void frmSerial_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (spPorta.IsOpen) // se porta aberta
                spPorta.Close(); //fecha a port
        }
    }
}
/*
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

private void btn5_Click(object sender, EventArgs e)
{
    codigo = 'e';
    if (spPorta.IsOpen) //porta está aberta
    {
        pb5.Visible = true;
        // THISSSSSSSSS 
        spPorta.WriteLine(codigo.ToString());//mandar pro arduino qual botao foi acionado
        DesativarBotoes();
        lbMensagem.Text = "Mensagem: Executando";
    }
}




// not working
 // COLOCA AS COISA NO EXCEL
Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                
try
{
    XcelApp.Application.Workbooks.Add(Type.Missing);
    for (int i = 1; i < dgv.Columns.Count + 1; i++)
    {
        XcelApp.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
    }
    for (int i = 0; i < dgv.Rows.Count - 1; i++)
    {
        for (int j = 0; j < dgv.Columns.Count; j++)
        {
            XcelApp.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
        }
    }
    XcelApp.Columns.AutoFit();
    XcelApp.Visible = true;
}
catch (Exception ex)
{
    MessageBox.Show("Erro : " + ex.Message);
    XcelApp.Quit();
}

////////////////////////////////////////////////////////////

// colocar o path inteiro + txtArquivoExcel.Text;
//String nomeArq = "C:\\Users\\giova\\Desktop\\" + txtArquivoExcel.Text;
//WorkBook.SaveAs(nomeArq, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
//    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
//WorkBook.Close(true, misValue, misValue);
//App.Quit();

}*/
