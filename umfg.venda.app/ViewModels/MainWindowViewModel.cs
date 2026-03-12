using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using umfg.venda.app.Abstracts;
using umfg.venda.app.Commands;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;

namespace umfg.venda.app.ViewModels
{
    internal sealed class MainWindowViewModel : AbstractViewModel, IObserver
    {
        private UserControl _userControl;
        private PedidoModel _pedido = new PedidoModel();

        public UserControl UserControl
        {
            get => _userControl;
            set => SetField(ref _userControl, value);
        }

        public PedidoModel Pedido
        {
            get => _pedido;
            set => SetField(ref _pedido, value);
        }

        public ListarProdutosCommand ListarProdutos { get; private set; } = new();

        public MainWindowViewModel() : base("UMFG - Tela Pricipal")
        {
        }

        public void Update(ISubject subject)
        {
            if (subject is ListarProdutosViewModel)
                UserControl = (subject as ListarProdutosViewModel).UserControl;

            if (subject is ReceberPedidoViewModel)
                UserControl = (subject as ReceberPedidoViewModel).UserControl;
        }
    }
}
