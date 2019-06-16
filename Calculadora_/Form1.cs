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

        bool verifica = false;
        bool mostrar_resultado = false;
        double num1 = 0;
        double calculo = 0;
        string operacao = string.Empty;

        public void MostrarNumeros(int nums)
        {
            /*Se verifica for igual a 'False', cada número que digitar irá 
            * aparecer na tela junto do último número clicado
            */
            if (verifica == false)
            {
                //gerar a sequência de números enquanto nenhum botão de operação for clicado
                txtTela.Text += nums;
            }
            else
            {
                /*verifica = 'True' terminará a sequência de números digitada
                * e começa novamente, guardando a última sequência em uma variável
                */
                txtTela.Text = nums.ToString();
                //verifica volta a ser 'False'
                verifica = false;
            }
        }

        public void Operacao(string op)
        {
            //Método que irá verificar se algum botão de operação foi clicado
            verifica = true;
            //
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
            mostrar_resultado = true;
        }

        public void CalcularMostrar()
        {
            switch (operacao)
            {
                case "+":
                    calculo = num1 + double.Parse(txtTela.Text);
                    break;
                case "-":
                    calculo = num1 - double.Parse(txtTela.Text);
                    break;
                case "*":
                    calculo = num1 * double.Parse(txtTela.Text);
                    break;
                case "/":
                    calculo = num1 / double.Parse(txtTela.Text);
                    break;
            }

            txtTela.Text = calculo.ToString();
        }

        private void BtnNumeros_Click(object sender, EventArgs e)
        {
            //Instanciando os botões existentes nesta referências
            Button btn = (Button)sender;
            //Método 'MostrarNumeros' se verifica = 'False'
            MostrarNumeros(int.Parse(btn.Text));
        }

        private void BtnOperacoes_Click(object sender, EventArgs e)
        {
            //Instanciando os botões existentes nesta referências
            Button button = (Button)sender;
            //Método 'Operacao', recebe como parâmetro o texto presente no botão que foi clicado
            Operacao(button.Text);
        }

        private void BtnRes_Click(object sender, EventArgs e)
        {
            CalcularMostrar();
            mostrar_resultado = false;
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtTela.Text = string.Empty;
            verifica = false;
            num1 = calculo = 0;
        }

        private void TxtTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsLetter(e.KeyChar)) || (char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
    }
}
