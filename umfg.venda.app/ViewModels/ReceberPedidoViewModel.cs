using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using umfg.venda.app.Abstracts;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;

namespace umfg.venda.app.ViewModels
{
    internal sealed class ReceberPedidoViewModel : AbstractViewModel
    {
        private PedidoModel _pedido = new();
        private string _numeroCartao = string.Empty;
        private string _cvv = string.Empty;
        private DateTime? _dataValidade = null;
        private string _nome = string.Empty;

        public string NumeroCartao
        {
            get => _numeroCartao;
            set => SetField(ref _numeroCartao, value);
        }

        public string CVV
        {
            get => _cvv;
            set => SetField(ref _cvv, value);
        }

        public DateTime? DataValidade
        {
            get => _dataValidade;
            set => SetField(ref _dataValidade, value);
        }

        public string Nome
        {
            get => _nome;
            set => SetField(ref _nome, value);
        }

        public PedidoModel Pedido
        {
            get => _pedido;
            set => SetField(ref _pedido, value);
        }

        public Abstracts.AbstractCommand Receber { get; private set; }
        public Abstracts.AbstractCommand Voltar { get; private set; }
        public Abstracts.AbstractCommand RemoverItem { get; private set; }

        public ReceberPedidoViewModel(UserControl userControl, IObserver observer, PedidoModel pedido)
            : base("Faça seu pagamento")
        {
            UserControl = userControl ?? throw new ArgumentNullException(nameof(userControl));
            MainWindow = observer ?? throw new ArgumentNullException(nameof(observer));

            
            if (observer is umfg.venda.app.ViewModels.MainWindowViewModel mainVm)
            {
                if (mainVm.Pedido is not null && mainVm.Pedido.Produtos is not null && mainVm.Pedido.Produtos.Count > 0)
                {
                    Pedido = mainVm.Pedido;
                }
                else
                {
                    Pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));
                    mainVm.Pedido = Pedido;
                }
            }
            else
            {
                Pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));
            }

            Add(observer);

            Receber = new ReceberCommand(this);
            Voltar = new VoltarCommand(this);
            RemoverItem = new RemoverItemCommand(this);
        }

        private sealed class ReceberCommand : Abstracts.AbstractCommand
        {
            private readonly ReceberPedidoViewModel _vm;

            public ReceberCommand(ReceberPedidoViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                var errors = new List<string>();

                if (string.IsNullOrWhiteSpace(_vm.Nome))
                    errors.Add("Nome no cartão é obrigatório.");

                if (string.IsNullOrWhiteSpace(_vm.NumeroCartao))
                    errors.Add("Número do cartão é obrigatório.");
                else if (!IsValidCardNumber(_vm.NumeroCartao))
                    errors.Add("Número do cartão inválido.");

                if (string.IsNullOrWhiteSpace(_vm.CVV))
                    errors.Add("CVV é obrigatório.");
                else if (!System.Text.RegularExpressions.Regex.IsMatch(_vm.CVV, "^\\d{3}$"))
                    errors.Add("CVV deve conter exatamente 3 dígitos.");

                if (!_vm.DataValidade.HasValue)
                    errors.Add("Data de validade é obrigatória.");
                else
                {
                    var selected = _vm.DataValidade.Value;
                    
                    var lastDay = new DateTime(selected.Year, selected.Month, DateTime.DaysInMonth(selected.Year, selected.Month));
                    if (lastDay < DateTime.Today)
                        errors.Add("Data de validade do cartão deve ser superior à data atual.");
                }

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Erro ao processar pagamento", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                MessageBox.Show("Pagamento efetuado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                
                if (_vm.MainWindow is umfg.venda.app.ViewModels.MainWindowViewModel mainVm)
                {
                    mainVm.UserControl = null;
                }
            }

            private static bool IsValidCardNumber(string number)
            {
                
                var digits = System.Text.RegularExpressions.Regex.Replace(number, "[^0-9]", string.Empty);
                if (digits.Length < 12 || digits.Length > 19) return false;

                
                int sum = 0;
                bool alternate = false;
                for (int i = digits.Length - 1; i >= 0; i--)
                {
                    int n = int.Parse(digits[i].ToString());
                    if (alternate)
                    {
                        n *= 2;
                        if (n > 9) n -= 9;
                    }
                    sum += n;
                    alternate = !alternate;
                }
                return (sum % 10) == 0;
            }
        }

        private sealed class VoltarCommand : Abstracts.AbstractCommand
        {
            private readonly ReceberPedidoViewModel _vm;

            public VoltarCommand(ReceberPedidoViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object? parameter)
            {
                
                if (_vm.MainWindow is umfg.venda.app.ViewModels.MainWindowViewModel mainVm)
                {
                    umfg.venda.app.UserControls.ucListarProdutos.Show(mainVm);
                }
            }
        }

        private sealed class RemoverItemCommand : Abstracts.AbstractCommand
        {
            private readonly ReceberPedidoViewModel _vm;

            public RemoverItemCommand(ReceberPedidoViewModel vm)
            {
                _vm = vm;
            }

            public override bool CanExecute(object? parameter)
            {
                
                return (_vm?.Pedido?.Produtos?.Any() == true) && (parameter is PedidoItemModel || parameter is umfg.venda.app.Models.ProdutoModel);
            }

            public override void Execute(object? parameter)
            {
                if (_vm?.Pedido?.Produtos is null)
                    return;

                PedidoItemModel itemToRemove = null;

                if (parameter is PedidoItemModel pItem)
                {
                    itemToRemove = _vm.Pedido.Produtos.FirstOrDefault(x => x == pItem);
                }
                else if (parameter is umfg.venda.app.Models.ProdutoModel prod)
                {
                    itemToRemove = _vm.Pedido.Produtos.FirstOrDefault(x => x.Produto != null && x.Produto.Id == prod.Id);
                }

                if (itemToRemove is null)
                    return;

                _vm.Pedido.Produtos.Remove(itemToRemove);
                _vm.Pedido.RecalcularTotal();
            }
        }
    }
}
