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
        double num1 = 0;
        double calculo = 0;
        string operacao = string.Empty;
        int tamanho;

        public void MostrarNumeros(int nums)
        {
            /*Se verifica for igual a 'False', cada número que digitar irá 
            * aparecer na tela junto do último número clicado
            */
            if (verificar_operacao == false)
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
                verificar_operacao = false;
            }
        }

        public void Operacao(string op)
        {
            try
            {
                //Método que irá verificar se algum botão de operação foi clicado
                verificar_operacao = true;
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
            catch(Exception)
            {

            }
        }

        public void CalcularMostrar()
        {
            try
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
            catch
            {

            }
        }

        public void TratarDivisao()
        {
            //Verifica se operação escolhida é divisão e se a divisão é por ZERO
            if (operacao == "/" & txtTela.Text == "0")
            {
                //impossibilita de dividir por ZERO e mostra uma mensagem
                lblMensagem.Text = "Impossível dividir por zero";
                txtTela.Enabled = false;

            }
            else
                //senão, mostra o resultado
                CalcularMostrar();
                mostrar_resultado = false;
        }

        private void BtnNumeros_Click(object sender, EventArgs e)
        {
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
        }

        private void BtnRes_Click(object sender, EventArgs e)
        {
            TratarDivisao();
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtTela.Text = lblMensagem.Text = string.Empty;
            verificar_operacao = false;
            num1 = calculo = 0;
            txtTela.Enabled = true;
        }

        private void TxtTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Quando uma tecla for clicada, irá verificar se é caractere ou espaço
            if ((char.IsLetter(e.KeyChar)) || (char.IsWhiteSpace(e.KeyChar)))
            {
                //impossibilitará de inserir caracteres e espaços
                e.Handled = true;
            }
        }

        private void BtnVirgula_Click(object sender, EventArgs e)
        {
            //retorna a quantidade de caracteres contido na Tela para a variável 'tamanho'
            tamanho = txtTela.Text.Length;
            //se a quantidade for maior que ZERO, apaga o último caractere
            if (tamanho > 0)
                //insere a vírgula somente se já houver números digitados
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
    }
}
