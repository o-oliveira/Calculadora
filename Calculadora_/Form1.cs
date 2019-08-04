using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Variável que verifica se algum botão de operação foi clicado, ex: +-*/
        bool verificar_operacao = false;
        bool mostrar_resultado = false;
        // Variável que verifica se o resultado foi ou não mostrado
        bool verifica_res = false;
        double num1 = 0;
        double calculo = 0;
        double n = 0;
        string operacao = string.Empty;
        int tamanho;

        public void MostrarNumeros(int nums)
        {
            if (verificar_operacao == false)
            {
                /* Gera a sequência de números enquanto nenhum botão de operação for clicado
                 * limita a entrada de dados na tela a 16 números contando a operação.
                */
                tamanho = txtTela.Text.Length;
                if (tamanho >= 16)
                    txtTela.Enabled = false;
                else
                    txtTela.Text += nums;
            }
            else
            {
                /* Se verifica_operacao == 'True', entenderá que foi clicado em um botão de 
                 * operacao, e guardará o número digitado na variável 'n'
                */
                txtTela.Text = nums.ToString();
                verificar_operacao = false;
            }
        }

        private void BtnNegativo_Click(object sender, EventArgs e)
        {
            NumNegativo(/*num1*/);
        }

        public void NumNegativo(/*double x*/)
        {
            txtTela.Text = (double.Parse(txtTela.Text) * -1).ToString();
            //x = double.Parse(txtTela.Text);
            //lblMensagem.Text = x.ToString();
        }

        public void Teste()
        {
            txtTela.Text = "";
        }

        public void Operacao(string op)
        {
            try
            {
                verificar_operacao = true;
                if (mostrar_resultado == true)
                {
                    CalcularMostrar();
                }
                operacao = op;
                /*Quando um botão de operação for clicado e a sequência parada,
                 * o número presente na tela será guardado na variável 'num1'
                */
                num1 = double.Parse(txtTela.Text);
                // Quando algum botão de operação for clicado, variável 'mostrar_resultado = True'
                mostrar_resultado = true;
                lblMensagem.Text = num1 + " " + operacao;
            }
            catch(Exception)
            {
            }
        }

        public void CalcularMostrar()
        {
            switch (operacao)
            {
                case "+":
                    calculo = num1 + n;
                    break;
                case "-":
                    calculo = num1 - n;
                    break;
                case "*":
                    calculo = num1 * n;
                    break;
                case "/":
                    calculo = num1 / n;
                    break;
            }
            // Se passar pelo 'switch', 'verificar_res = true' e

            //TESTAR
            verifica_res = true;
            //TESTAR

            lblMensagem.Text = "";
            //mostra o resultado na tela
            txtTela.Text = calculo.ToString();
        }

        public void TratarDivisao()
        {
            // Verifica se operação escolhida é divisão e se a divisão é por ZERO
            if (operacao == "/" & txtTela.Text == "0")
            {
                lblMensagem.Text = "Impossível dividir por zero";
                txtTela.Enabled = false;
            }
            else
                CalcularMostrar();
                // Após mostrar resultado, 'mostrar_resultado' retorna para 'false'
                mostrar_resultado = false;
        }

        private void BtnNumeros_Click(object sender, EventArgs e)
        {
            if (verifica_res == true)
            {
                txtTela.Text = string.Empty;
            }
            // Retorna "vericiar_res" para false
            verifica_res = false;
            //Instanciando os botões existentes nesta referência
            Button btn = (Button)sender;
            MostrarNumeros(int.Parse(btn.Text));
        }

        private void BtnOperacoes_Click(object sender, EventArgs e)
        {
            //Instanciando os botões existentes nesta referência
            Button button = (Button)sender;
            //Método 'Operacao', recebe como parâmetro o texto presente no botão que foi clicado
            Operacao(button.Text);

            switch (button.Text)
            {
                case "√":
                    lblMensagem.Text = "sqrt(" + num1 + ")";
                    txtTela.Text = Math.Sqrt(num1).ToString();
                    break;
                case "x²":
                    lblMensagem.Text = "sqr(" + num1 + ")";
                    txtTela.Text = Math.Pow(num1, 2).ToString();
                    break;
                case "x³":
                    lblMensagem.Text = "cube(" + num1 + ")";
                    txtTela.Text = Math.Pow(num1, 3).ToString();
                    break;
            }
            mostrar_resultado = false;
        }

        private void BtnRes_Click(object sender, EventArgs e)
        {
            try
            {
                //Variável para guardar o segundo número caso use um número negativo
                n = double.Parse(txtTela.Text);
                label1.Text = num1.ToString();
                label2.Text = n.ToString();
                TratarDivisao();
            }
            catch
            {
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtTela.Text = lblMensagem.Text = "";
            num1 = calculo = n = 0;
            verificar_operacao = false;
            txtTela.Enabled = true;
            operacao = string.Empty;

            label1.Text = label2.Text = label3.Text = string.Empty;
        }

        private void TxtTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Quando uma tecla for clicada, irá verificar se é caractere ou espaço em branco
            if ((char.IsLetter(e.KeyChar)) || (char.IsWhiteSpace(e.KeyChar)))
            {
                // impossibilitará de inserir
                e.Handled = true;
            }
        }

        private void BtnVirgula_Click(object sender, EventArgs e)
        {
            // Retorna a quantidade de caracteres contido na Tela para a variável 'tamanho'
            tamanho = txtTela.Text.Length;
            if (tamanho > 0)
                // Irá inserir a vírgula somente se já houver números digitados
                txtTela.Text += btnVirgula.Text;
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            //retorna a quantidade de caracteres contido na Tela para a variável 'tamanho'
            tamanho = txtTela.Text.Length;
            if(tamanho > 0)
                //Substring extrai caracteres desde indexInicio - Substrint(indexInicio, IndexFim)
                txtTela.Text = txtTela.Text.Substring(0, tamanho - 1);
        }

        /*private void CalcPadrao_Click(object sender, EventArgs e)
        {
            UserControls(this);
        }

        private void UserControls(Control control)
        {
            foreach(Control ctrl in control.Controls)
            {
                if(ctrl is UserControl)
                {
                    ((UserControl)(ctrl)).Visible = false;
                }
            }
        }*/
    }
}
