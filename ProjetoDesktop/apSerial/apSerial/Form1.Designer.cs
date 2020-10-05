namespace apSerial
{
    partial class frmSerial
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlDados = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtArquivoExcel = new System.Windows.Forms.TextBox();
            this.btnGerarExcel = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Horário = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Altitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Umidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Temperatura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pressão = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.co = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nh3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.cbxPortas = new System.Windows.Forms.ComboBox();
            this.spPorta = new System.IO.Ports.SerialPort(this.components);
            this.dlgAbrir = new System.Windows.Forms.OpenFileDialog();
            this.btnImportar = new System.Windows.Forms.Button();
            this.lbArqImportado = new System.Windows.Forms.Label();
            this.txtReceber = new System.Windows.Forms.TextBox();
            this.tmrTempo = new System.Windows.Forms.Timer(this.components);
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtEnviar = new System.Windows.Forms.TextBox();
            this.pnlDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDados
            // 
            this.pnlDados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlDados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDados.Controls.Add(this.label1);
            this.pnlDados.Controls.Add(this.txtArquivoExcel);
            this.pnlDados.Controls.Add(this.btnGerarExcel);
            this.pnlDados.Controls.Add(this.dgv);
            this.pnlDados.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDados.Location = new System.Drawing.Point(0, 146);
            this.pnlDados.Name = "pnlDados";
            this.pnlDados.Size = new System.Drawing.Size(1104, 444);
            this.pnlDados.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nome do Arquivo:";
            // 
            // txtArquivoExcel
            // 
            this.txtArquivoExcel.Location = new System.Drawing.Point(13, 42);
            this.txtArquivoExcel.MaxLength = 100;
            this.txtArquivoExcel.Multiline = true;
            this.txtArquivoExcel.Name = "txtArquivoExcel";
            this.txtArquivoExcel.Size = new System.Drawing.Size(298, 28);
            this.txtArquivoExcel.TabIndex = 6;
            // 
            // btnGerarExcel
            // 
            this.btnGerarExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnGerarExcel.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarExcel.Location = new System.Drawing.Point(423, 393);
            this.btnGerarExcel.Name = "btnGerarExcel";
            this.btnGerarExcel.Size = new System.Drawing.Size(259, 56);
            this.btnGerarExcel.TabIndex = 5;
            this.btnGerarExcel.Text = "Gerar Excel";
            this.btnGerarExcel.UseVisualStyleBackColor = false;
            this.btnGerarExcel.Click += new System.EventHandler(this.btnGerarExcel_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.Horário,
            this.Altitude,
            this.Latitude,
            this.Longitude,
            this.Umidade,
            this.Temperatura,
            this.Pressão,
            this.co,
            this.no2,
            this.nh3});
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(13, 76);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(1079, 311);
            this.dgv.TabIndex = 4;
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            // 
            // Horário
            // 
            this.Horário.HeaderText = "Horário";
            this.Horário.Name = "Horário";
            // 
            // Altitude
            // 
            this.Altitude.HeaderText = "Altitude";
            this.Altitude.Name = "Altitude";
            // 
            // Latitude
            // 
            this.Latitude.HeaderText = "Latitude";
            this.Latitude.Name = "Latitude";
            // 
            // Longitude
            // 
            this.Longitude.HeaderText = "Longitude";
            this.Longitude.Name = "Longitude";
            // 
            // Umidade
            // 
            this.Umidade.HeaderText = "Umidade";
            this.Umidade.Name = "Umidade";
            // 
            // Temperatura
            // 
            this.Temperatura.HeaderText = "Temperatura";
            this.Temperatura.Name = "Temperatura";
            // 
            // Pressão
            // 
            this.Pressão.HeaderText = "Pressão";
            this.Pressão.Name = "Pressão";
            // 
            // co
            // 
            this.co.HeaderText = "CO";
            this.co.Name = "co";
            // 
            // no2
            // 
            this.no2.HeaderText = "NO2";
            this.no2.Name = "no2";
            // 
            // nh3
            // 
            this.nh3.HeaderText = "NH3";
            this.nh3.Name = "nh3";
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnviar.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Location = new System.Drawing.Point(290, 38);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 34);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // cbxPortas
            // 
            this.cbxPortas.FormattingEnabled = true;
            this.cbxPortas.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cbxPortas.Location = new System.Drawing.Point(12, 7);
            this.cbxPortas.Name = "cbxPortas";
            this.cbxPortas.Size = new System.Drawing.Size(266, 23);
            this.cbxPortas.TabIndex = 1;
            // 
            // spPorta
            // 
            this.spPorta.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.spPorta_DataReceived);
            // 
            // dlgAbrir
            // 
            this.dlgAbrir.DefaultExt = "*.txt";
            this.dlgAbrir.FileName = "openFileDialog1";
            // 
            // btnImportar
            // 
            this.btnImportar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Location = new System.Drawing.Point(78, 82);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(233, 49);
            this.btnImportar.TabIndex = 4;
            this.btnImportar.Text = "Importar Dados";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // lbArqImportado
            // 
            this.lbArqImportado.AutoSize = true;
            this.lbArqImportado.BackColor = System.Drawing.Color.Transparent;
            this.lbArqImportado.ForeColor = System.Drawing.Color.Silver;
            this.lbArqImportado.Location = new System.Drawing.Point(19, 101);
            this.lbArqImportado.Name = "lbArqImportado";
            this.lbArqImportado.Size = new System.Drawing.Size(0, 15);
            this.lbArqImportado.TabIndex = 5;
            // 
            // txtReceber
            // 
            this.txtReceber.Location = new System.Drawing.Point(423, 7);
            this.txtReceber.Multiline = true;
            this.txtReceber.Name = "txtReceber";
            this.txtReceber.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReceber.Size = new System.Drawing.Size(464, 130);
            this.txtReceber.TabIndex = 2;
            // 
            // tmrTempo
            // 
            this.tmrTempo.Tick += new System.EventHandler(this.tmrTempo_Tick);
            // 
            // btnConectar
            // 
            this.btnConectar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConectar.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.Location = new System.Drawing.Point(290, 2);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(105, 34);
            this.btnConectar.TabIndex = 6;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConectar.UseVisualStyleBackColor = true;
            // 
            // txtEnviar
            // 
            this.txtEnviar.Location = new System.Drawing.Point(12, 38);
            this.txtEnviar.Multiline = true;
            this.txtEnviar.Name = "txtEnviar";
            this.txtEnviar.Size = new System.Drawing.Size(266, 30);
            this.txtEnviar.TabIndex = 7;
            // 
            // frmSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1104, 590);
            this.Controls.Add(this.txtEnviar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.lbArqImportado);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.cbxPortas);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.pnlDados);
            this.Controls.Add(this.txtReceber);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSerial";
            this.Text = "PIBIC 19-20";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSerial_FormClosed);
            this.Load += new System.EventHandler(this.frmSerial_Load);
            this.pnlDados.ResumeLayout(false);
            this.pnlDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDados;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.ComboBox cbxPortas;
        private System.IO.Ports.SerialPort spPorta;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnGerarExcel;
        private System.Windows.Forms.TextBox txtArquivoExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Horário;
        private System.Windows.Forms.DataGridViewTextBoxColumn Altitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Umidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Temperatura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pressão;
        private System.Windows.Forms.DataGridViewTextBoxColumn co;
        private System.Windows.Forms.DataGridViewTextBoxColumn no2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nh3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog dlgAbrir;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label lbArqImportado;
        private System.Windows.Forms.TextBox txtReceber;
        private System.Windows.Forms.Timer tmrTempo;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtEnviar;
    }
}

