using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using umfg.venda.app.Abstracts;
using umfg.venda.app.ViewModels;
using umfg.venda.app.Models;

namespace umfg.venda.app.Commands
{
    internal sealed class RemoverProdutoPedidoCommand : AbstractCommand
    {
        public override bool CanExecute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            //o método Any() indica que a lista possui ao menos um registro
            return vm is not null && vm.Pedido.Produtos.Any();
        }

        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            if (vm is null)
            {
                MessageBox.Show("Parâmetro obrigatório não informado! Verifique.");
                return;
            }

            if (vm.ProdutoSelecionado is null || Guid.Empty.Equals(vm.ProdutoSelecionado.Id))
            {
                MessageBox.Show("Nenhum produto selecionado para remover do carrinho!");
                return;
            }

            // find item in Pedido.Produtos by Produto.Id
            var item = vm.Pedido.Produtos.FirstOrDefault(x => x.Produto != null && x.Produto.Id == vm.ProdutoSelecionado.Id);
            if (item is null)
            {
                MessageBox.Show($"{vm.ProdutoSelecionado.Descricao} não foi encontrado no carrinho!");
                return;
            }

            var result = MessageBox
                .Show("Deseja realmente remover este item no carrinho?",
                            "Confirmar produto", MessageBoxButton.YesNo);

            if (!MessageBoxResult.Yes.Equals(result))
            {
                return;
            }

            vm.Pedido.Produtos.Remove(item);
            vm.Pedido.RecalcularTotal();
            vm.RaiseCanExecuteChanged();
        }
    }
}
