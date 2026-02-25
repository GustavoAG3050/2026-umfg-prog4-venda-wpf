using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using umfg.wpf.Abstracts;
using umfg.wpf.Interfaces;
using umfg.wpf.Model;

namespace umfg.wpf.ViewModel
{
    internal sealed class ListarProdutosViewModel : AbstractViewModel
    {

        private ProdutoModel _produtoSelecionado = new();

        private ObservableCollection<ProdutoModel> _produtos = [];

        public ProdutoModel ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetField(ref _produtoSelecionado, value);
        }

        public ObservableCollection<ProdutoModel> Produtos
        {
            get => _produtos;
            set => SetField(ref _produtos, value);
        }

        public ListarProdutosViewModel(IObserver observer, UserControls userControls) : base("Produtos")
        {

            UserControls = UserControls;
            MainWindow = observer;

            Add(observer);
            
            

            CarregarProdutos();

        }

        private void CarregarProdutos()
        {
            Produtos.Clear();



            Produtos.Add(new ProdutoModel
            {
                
                imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows\batata.png", UriKind.Relative)),

                   descricao = "Batata Frita 300g",
                   referencia = "BAT300",
                   valor = 15.00m

            });

            Produtos.Add(new ProdutoModel
            {

                imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows\combo.png", UriKind.Relative)),

                descricao = "Combo Big Mac + Batata 300g + Refil 500ml",
                referencia = "COM500",
                valor = 49.00m

            });

            Produtos.Add(new ProdutoModel
            {

                imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows\lanche.png", UriKind.Relative)),

                descricao = "Big Mac 300g",
                referencia = "BMC300",
                valor = 29.00m

            });

            Produtos.Add(new ProdutoModel
            {

                imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows\refrigerante.png", UriKind.Relative)),

                descricao = "Refrigerante Refill 500ml",
                referencia = "RFL500",
                valor = 10.00m

            });
        }
    }
}
