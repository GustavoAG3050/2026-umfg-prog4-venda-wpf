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

        public MainWindowViewModel() : base("Sistema de Pedidos")
        {
        }

        public void Update(ISubject subject)
        {
            // When a child view model notifies, update the displayed UserControl and the footer title
            if (subject is ListarProdutosViewModel lp)
            {
                UserControl = lp.UserControl;
                Titulo = lp.Titulo;
            }

            if (subject is ReceberPedidoViewModel rp)
            {
                UserControl = rp.UserControl;
                Titulo = rp.Titulo;
            }
        }
    }
}
