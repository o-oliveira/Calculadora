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

        bool verificar_operacao = false;
        bool mostrar_resultado = false;
        bool verifica_res = false;
        double num1 = 0;
        double calculo = 0;
        double n = 0;
        string operacao = string.Empty;
        int tamanho;

        public void MostrarNumeros(int nums)
        {
            // Enquanto verifica_operacao == 'False', vai formando a sequência de números
            if (verificar_operacao == false)
            {
                tamanho = txtTela.Text.Length;
                if (tamanho >= 16)
                    txtTela.Enabled = false;
                else
                    //gerar a sequência de números enquanto nenhum botão de operação for clicado
                    txtTela.Text += nums;
            }
            else
            {
                /* Se verifica_operacao == 'True', entenderá que foi clicado em um botão de 
                 * operacao, e guardará o número digitado na variável 'n'
                */
                txtTela.Text = nums.ToString();
                //verifica_operacao volta a ser 'False'
                verificar_operacao = false;
            }
        }

        public void Operacao(string op)
        {
            try
            {
                //Variável que irá verificar se algum botão de operação foi clicado
                verificar_operacao = true;
                /* Quando 'mostrar_resultado = True', invoca o método CalcularMostrar() para 
                 * mostrar o resultado
                 */
                if (mostrar_resultado == true)
                {
                    CalcularMostrar();
                }
                //variável 'operacao' recebe o conteúdo da variável 'op'
                operacao = op;
                /*Quando um botão de operação for clicado e a sequência parada,
                 * o número presente na tela será guardado na variável 'num1'
                */
                num1 = double.Parse(txtTela.Text);
                // Quando algum botão de operação for clicado, variável 'mostrar_resultado = True'
                mostrar_resultado = true;
                // Mostra na label o primeiro número + a operação invocada
                lblMensagem.Text = num1 + " " + operacao;
            }
            catch(Exception)
            {
            }
        }

        public string/*void*/ CalcularMostrar()
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
            verifica_res = true;
            lblMensagem.Text = "";
            //mostra o resultado na tela
            txtTela.Text = calculo.ToString();
            return operacao;
        }

        public void TratarDivisao()
        {
            // Verifica se operação escolhida é divisão, e se a divisão é por ZERO
            if (operacao == "/" & txtTela.Text == "0")
            {
                //impossibilita de dividir por ZERO e mostra uma mensagem de erro
                lblMensagem.Text = "Impossível dividir por zero";
                txtTela.Enabled = false;
            }
            else
                //senão, mostra o resultado
                CalcularMostrar();
                // Após mostrar resultado, 'mostrar_resultado' retorna para 'false'
                mostrar_resultado = false;
        }

        private void BtnNumeros_Click(object sender, EventArgs e)
        {
            // Variável que verifica se o resultado foi ou não mostrado
            if (verifica_res == true)
            {
                /* Se for 'true' quer dizer que foi mostrado, e ao clicar em um novo 
                 * número, limpará a tela e começara uma nova sequência de números
                 */
                txtTela.Text = string.Empty;
            }
            // Retorna "vericiar_res" para false
            verifica_res = false;
            //Instanciando os botões existentes nesta referência
            Button btn = (Button)sender;
            //Método 'MostrarNumeros' se verifica = 'False'
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
                case "±":
                    //num1 = double.Parse(txtTela.Text) * -1;
                    //txtTela.Text = num1.ToString();
                    NumNegativo(num1);

                    //label1.Text = num1.ToString();
                    //label2.Text = n.ToString();
                    break;
            }
            // Após mostrar o resultado das operações acima, 'mostrar_resultado' retorna ao estado false
            mostrar_resultado = false;
        }

        public double NumNegativo(double x/*, double y*/)
        {
            double res = 0;
            x = double.Parse(txtTela.Text) * -1;
            label1.Text = x.ToString();

            label2.Text = CalcularMostrar();

            return res;
        }

        private void BtnRes_Click(object sender, EventArgs e)
        {
            //Variável para guardar o segundo número caso use um número negativo
            n = double.Parse(txtTela.Text);
            label1.Text = num1.ToString();
            label2.Text = n.ToString();
            TratarDivisao();
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtTela.Text = lblMensagem.Text = "";
            num1 = calculo = n = 0;
            verificar_operacao = false;
            txtTela.Enabled = true;
        }

        private void TxtTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Quando uma tecla for clicada, irá verificar se é caractere ou espaço e
            if ((char.IsLetter(e.KeyChar)) || (char.IsWhiteSpace(e.KeyChar)))
            {
                //impossibilitará de inserir
                e.Handled = true;
            }
        }

        private void BtnVirgula_Click(object sender, EventArgs e)
        {
            //retorna a quantidade de caracteres contido na Tela para a variável 'tamanho'
            tamanho = txtTela.Text.Length;
            if (tamanho > 0)
                // Irá inserir a vírgula somente se já houver números digitados
                txtTela.Text += btnVirgula.Text;
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            //retorna a quantidade de caracteres contido na Tela para a variável 'tamanho'
            tamanho = txtTela.Text.Length;
            //se a quantidade for maior que ZERO, apaga o último caractere
            if(tamanho > 0)
                //Substring extrai caracteres desde indexInicio - Substrint(indexInicio, IndexFim)
                //apaga os últimos números trazido pelo tamanho
                txtTela.Text = txtTela.Text.Substring(0, tamanho - 1);
        }

        private void ToolStripFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
