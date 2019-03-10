using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App_Consulta_CEP.Servico.Modelo;
using App_Consulta_CEP.Servico;

namespace App_Consulta_CEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Botao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = Cep.Text.Trim();
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        Resultado.Text = string.Format("Logradouro: {0}\n" +
                        "Complemento: {1}\n" +
                        "Bairro: {2}\n" +
                        "Localidade: {3}\n" +
                        "Uf: {4}\n" +
                        "Unidade: {5}\n" +
                        "IBGE: {6}\n" +
                        "GIA: {7}",
                        end.Logradouro,
                        end.Complemento,
                        end.Bairro,
                        end.Localidade,
                        end.Uf,
                        end.Unidade,
                        end.Ibge,
                        end.Gia);
                    }
                    else
                    {
                        DisplayAlert("Alerta",string.Format("Não foi encontrado endereço associado ao CEP {0}.", cep),"Ok");
                        Resultado.Text = "";
                        //Resultado.Text = string.Format("Não foi encontrado endereço associado ao CEP {0}.", cep);
                    }

                }
                catch(Exception e)
                {
                    DisplayAlert("Erro", "Verifique sua conexão com a internet.\n\n"+e.Message, "Ok");
                }
            }
//            else
//            {
//                Resultado.Text = "Digite um CEP válido.\n\n" +
//                    "O CEP deve conter somente números e 8 dígitos.";
//            }
        }

        private bool isValidCEP(string cep)
        {
            bool resultado = true;
            int NovoCEP = 0;

            if (!int.TryParse(cep, out NovoCEP))
            {
                resultado = false;
                DisplayAlert("CEP Inválido", "O CEP deve conter somente números.", "Ok");
                Resultado.Text = "";
            } else if (cep.Length != 8) {
                resultado = false;
                DisplayAlert("CEP Inválido", "O CEP deve conter 8 dígitos.", "Ok");
                Resultado.Text = "";
            }

            return resultado;
        }
    }
}
