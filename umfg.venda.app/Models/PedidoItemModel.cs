using System;
using umfg.venda.app.Abstracts;

namespace umfg.venda.app.Models
{
    internal sealed class PedidoItemModel : AbstractModel
    {
        private ProdutoModel _produto;
        private int _quantidade = 1;

        public ProdutoModel Produto
        {
            get => _produto;
            set => SetField(ref _produto, value);
        }

        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (value < 1) value = 1;
                SetField(ref _quantidade, value);
                RaizePropertyChange(nameof(Subtotal));
            }
        }

        public decimal Subtotal => Produto is null ? 0m : Produto.Valor * Quantidade;
    }
}
