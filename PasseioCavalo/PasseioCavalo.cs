using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasseioCavalo
{
    public class frmPasseioCavalo : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button b00;
        private System.Windows.Forms.Button btnSolucao;
        private System.Windows.Forms.TextBox txtResultado;

        private Button[,] BL;
        private int[] tabela;
        private Button selecionado;
        //private int lin;
        private Label label1;
        private TextBox txtLinhas;
        private Button btnGerar;
        //private int col;
        private int dimensao;

        private int valor;

        public frmPasseioCavalo()
        {
            InitializeComponent();
            selecionado = null;
            //lin = 8;
            dimensao = 8;
            tabela = new int[dimensao*dimensao];

            for (int i = 0; i < dimensao * dimensao; i++)
            {
                tabela[i] = i;
            }
            valor = 0;
            DrawPanel();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.b00 = new System.Windows.Forms.Button();
            this.btnSolucao = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLinhas = new System.Windows.Forms.TextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b00
            // 
            this.b00.BackColor = System.Drawing.Color.White;
            this.b00.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b00.Location = new System.Drawing.Point(10, 10);
            this.b00.Name = "b00";
            this.b00.Size = new System.Drawing.Size(56, 56);
            this.b00.TabIndex = 0;
            this.b00.Text = "X";
            this.b00.UseVisualStyleBackColor = false;
            this.b00.Visible = false;
            this.b00.Click += new System.EventHandler(this.b00_Click);
            // 
            // btnSolucao
            // 
            this.btnSolucao.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolucao.Location = new System.Drawing.Point(10, 480);
            this.btnSolucao.Name = "btnSolucao";
            this.btnSolucao.Size = new System.Drawing.Size(185, 57);
            this.btnSolucao.TabIndex = 1;
            this.btnSolucao.Text = "Solução";
            this.btnSolucao.UseVisualStyleBackColor = true;
            this.btnSolucao.Click += new System.EventHandler(this.btnSolucao_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(206, 480);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(251, 57);
            this.txtResultado.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 547);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dimensão (5,6,7 ou 8):";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtLinhas
            // 
            this.txtLinhas.Location = new System.Drawing.Point(270, 543);
            this.txtLinhas.Name = "txtLinhas";
            this.txtLinhas.Size = new System.Drawing.Size(122, 20);
            this.txtLinhas.TabIndex = 4;
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(398, 542);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(59, 23);
            this.btnGerar.TabIndex = 7;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // frmPasseioCavalo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 574);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtLinhas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnSolucao);
            this.Controls.Add(this.b00);
            this.Name = "frmPasseioCavalo";
            this.Text = "Passeio Cavalo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void DrawPanel()
        {
            BL = new Button[dimensao, dimensao];
            for (int i = 0; i < dimensao; i++)
            {
                for (int j = 0; j < dimensao; j++)
                {
                    Button B = new Button();
                    B.Parent = b00.Parent;
                    B.Font = b00.Font;
                    B.Size = b00.Size;
                    B.Left = j * 56 + 10;
                    B.Top = i * 56 + 10;
                    B.BackColor = b00.BackColor;
                    B.Tag = tabela[i * dimensao + j].ToString();
                    B.Visible = true;
                    B.Click += new System.EventHandler(this.b00_Click);
                    BL[i, j] = B;
                }
            }
        }

        private void b00_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            
            if (selecionado == null)
            {
                selecionado = b;
                selecionado.Text = (tabela[valor]+1).ToString();
                valor++;
            }

            if (!selecionado.Equals(b))
            {
                int val = Convert.ToInt32(selecionado.Tag.ToString());
                int[] result = posicoesValidas(val);
                int bvalor = Convert.ToInt32(b.Tag.ToString());

                bool valido = false;
                foreach (int v in result)
                {
                    int l = v / dimensao;
                    int c = v % dimensao;
                    if(bvalor == v && BL[l,c].Text == "")
                    {
                        valido = true;
                        break;
                    }

                }
                if (valido)
                {
                    selecionado = b;
                    selecionado.Text = (tabela[valor] + 1).ToString();
                    valor++;
                }
            }
        }

        private int[] posicoesValidas(int valor)
        {
            List<int> result = new List<int>();

            int l = valor / dimensao;
            int c = valor % dimensao;

            int lmax = dimensao - 1;
            int cmax = dimensao - 1;

            if ((l + 2) <= lmax && (c + 1) <= cmax)
            {
                int pos = ((l + 2) * dimensao) + (c + 1);
                result.Add(tabela[pos]);
            }
            if ((l + 2) <= lmax && (c - 1) >= 0)
            {
                int pos = ((l + 2) * dimensao) + (c - 1);
                result.Add(tabela[pos]);
            }
            if ((l - 2) >= 0 && (c + 1) <= cmax)
            {
                int pos = ((l - 2) * dimensao) + (c + 1);
                result.Add(tabela[pos]);
            }
            if ((l - 2) >= 0 && (c - 1) >= 0)
            {
                int pos = ((l - 2) * dimensao) + (c - 1);
                result.Add(tabela[pos]);
            }

            if ((c + 2) <= cmax && (l + 1) <= lmax)
            {
                int pos = ((l + 1) * dimensao) + (c + 2);
                result.Add(tabela[pos]);
            }
            if ((c + 2) <= cmax && (l - 1) >= 0)
            {
                int pos = ((l - 1) * dimensao) + (c + 2);
                result.Add(tabela[pos]);
            }

            if ((c - 2) >= 0 && (l + 1) <= lmax)
            {
                int pos = ((l + 1) * dimensao) + (c - 2);
                result.Add(tabela[pos]);
            }
            if ((c - 2) >= 0 && (l - 1) >= 0)
            {
                int pos = ((l - 1) * dimensao) + (c - 2);
                result.Add(tabela[pos]);
            }
            return result.ToArray();
        }

        private void btnSolucao_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "";

            int[,] estadoInicial = new int[dimensao, dimensao];
            int k = 0;
            for (int i = 0; i < dimensao; i++)
                for (int j = 0; j < dimensao; j++)
                    estadoInicial[i, j] = Int32.Parse(( (BL[i, j].Text == "") ? "0" : BL[i, j].Text));

            PCAgente agente = new PCAgente(estadoInicial, dimensao);

            DateTime ini = System.DateTime.Now;
            int[] solucao = agente.ObterSolucao();
            TimeSpan ts = System.DateTime.Now - ini;

            if (solucao == null)
            {
                txtResultado.Text += "Tamanho de busca alcançado!";
            }
            else
            {
                int p = 1;
                foreach (int v in solucao)
                {
                    int linha = v / dimensao;
                    int coluna = v % dimensao;

                    //txtResultado.Text += v.ToString() + ",";
                    txtResultado.Text += p + " {" + (linha + 1) + "," + (coluna + 1) + "}, ";
                    if(BL[linha,coluna] != selecionado)
                        BL[linha, coluna].Text = p.ToString();
                    p++;
                }

                txtResultado.Text += "#";
                txtResultado.Text = txtResultado.Text.Replace(", #", ".");
                txtResultado.Text += " Tempo: " + ts.TotalMilliseconds + "ms";
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            if (txtLinhas.Text != "")
            {
                if(Convert.ToInt32(txtLinhas.Text) >= 5 && Convert.ToInt32(txtLinhas.Text) <= 8)
                {
                    for (int i = 0; i < dimensao; i++)
                        for (int j = 0; j < dimensao; j++)
                        {
                            this.Controls.Remove(BL[i, j]);
                            
                        }
                    dimensao = Convert.ToInt32(txtLinhas.Text);
                    valor = 0;

                    tabela = new int[dimensao * dimensao];
                    for (int i = 0; i < dimensao * dimensao; i++)
                    {
                        tabela[i] = i;
                    }
                    selecionado = null;
                    DrawPanel();
                }
                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
