using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace apSerial
{
    public partial class frmSerial : Form
    {
        public frmSerial()
        {
            InitializeComponent();
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

            try
            {
                // CRIA O ARQUIVO
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // COLOCA AS COISA NO EXCEL
                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                if (dgv.Rows.Count > 0)
                {
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
                }

                // E SALVA
                // colocar o path inteiro + txtArquivoExcel.Text;
                String nomeArq = "C:\\Users\\giova\\Desktop\\" + txtArquivoExcel.Text;
                xlWorkBook.SaveAs(nomeArq, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
 Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                liberarObjetos(xlWorkSheet);
                liberarObjetos(xlWorkBook);
                liberarObjetos(xlApp);

                MessageBox.Show("O arquivo Excel foi criado com sucesso. Você pode encontrá-lo na sua Área de Trabalho, com o nome: " + txtArquivoExcel.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
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
            arq.Close();
        }
        //a   b   c
        //d   e  f
        private void frmSerial_Load(object sender, EventArgs e)
        {

        }
    }
}
