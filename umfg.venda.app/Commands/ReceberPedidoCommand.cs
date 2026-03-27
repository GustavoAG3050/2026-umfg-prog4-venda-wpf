using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using umfg.venda.app.Abstracts;
using umfg.venda.app.UserControls;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.Commands
{
    internal sealed class ReceberPedidoCommand : AbstractCommand
    {
        
        public override bool CanExecute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            if (vm is null)
                return false;

            return vm.Pedido is not null && vm.Pedido.Produtos is not null && vm.Pedido.Produtos.Count > 0;
        }

        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            

            if (vm is null)
            {
                MessageBox.Show("Parâmetro obrigatório não informado! Verifique.");
                return;
            }

            ucReceberPedido.Exibir(vm.MainWindow, vm.Pedido);
        }
    }
}
