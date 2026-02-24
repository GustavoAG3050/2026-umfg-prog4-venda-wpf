using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfg.wpf.Abstracts;

namespace umfg.wpf.Model
{
    internal sealed class PedidoModel : AbstractModel
    {

        private Guid _id = Guid.NewGuid();

        private decimal _total = 0.0m;

        private ObservableCollection<ProdutoModel> _produtos = [];

        public Guid id { get => _id; set => SetField(ref _id, value); }

        public decimal total { get => _total; set => SetField(ref _total, value); }

        public ObservableCollection<ProdutoModel> Produtos
        { get => _produtos; set => SetField(ref _produtos, value); }

        

    }
}
