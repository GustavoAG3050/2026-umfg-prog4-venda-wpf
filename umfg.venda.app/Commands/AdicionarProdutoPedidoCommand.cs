using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic;
using umfg.venda.app.Abstracts;
using umfg.venda.app.ViewModels;
using umfg.venda.app.Models;

namespace umfg.venda.app.Commands
{
    internal sealed class AdicionarProdutoPedidoCommand : AbstractCommand
    {
        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            if (vm is null)
            {
                MessageBox.Show("Parâmetro obrigatório não foi informado");
                return;
            }

            if (vm.Pedido is null || Guid.Empty.Equals(vm.Pedido.Id))
            {
                MessageBox.Show("Pedido não foi criado corretamente! Verifique.");
                return;
            }

            if (vm.ProdutoSelecionado is null || Guid.Empty.Equals(vm.ProdutoSelecionado.Id))
            {
                MessageBox.Show("Nenhum produto selecionado! Verifique.");
                return;
            }

            var result = MessageBox.Show("Adicionar esse produto no carrinho?", "Confirmar produto", MessageBoxButton.YesNo);

            if (!MessageBoxResult.Yes.Equals(result))
                return;

            try
            {
                string input = Interaction.InputBox("Informe a quantidade:", "Quantidade", "1");
                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (!int.TryParse(input, out var quantidade) || quantidade <= 0)
                {
                    MessageBox.Show("Quantidade inválida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Use PedidoItemModel: increment quantidade if produto already in cart
                var existing = vm.Pedido.Produtos.FirstOrDefault(p => p.Produto != null && p.Produto.Id == vm.ProdutoSelecionado.Id);
                if (existing is not null)
                {
                    existing.Quantidade += quantidade;
                }
                else
                {
                    var item = new PedidoItemModel
                    {
                        Produto = vm.ProdutoSelecionado,
                        Quantidade = quantidade
                    };
                    vm.Pedido.Produtos.Add(item);
                }

                vm.Pedido.RecalcularTotal();
                vm.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
