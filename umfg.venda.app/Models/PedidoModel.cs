using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfg.venda.app.Abstracts;

namespace umfg.venda.app.Models
{
    internal sealed class PedidoModel : AbstractModel
    {
        private Guid _id = Guid.NewGuid();
        private decimal _total = 0.0m;
        private ObservableCollection<PedidoItemModel> _produtos = new System.Collections.ObjectModel.ObservableCollection<PedidoItemModel>();

        public Guid Id 
        { 
            get => _id; 
            set => SetField(ref _id, value);
        }

        public decimal Total 
        { 
            get => _total;
            set => SetField(ref _total, value);
        }

        public ObservableCollection<PedidoItemModel> Produtos
        { 
            get => _produtos; 
            set => SetField(ref _produtos, value);
        }

        public void RecalcularTotal()
        {
            Total = Produtos.Sum(x => x.Subtotal);
        }
    }
}
