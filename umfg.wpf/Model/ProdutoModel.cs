using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using umfg.wpf.Abstracts;

namespace umfg.wpf.Model
{
    internal sealed class ProdutoModel : AbstractModel 
    {
        private Guid _id = Guid.NewGuid();
        private ImageSource _imagem;
        private string _referencia;
        private string _descricao;
        private decimal _valor;



        public Guid id { get => _id; set => SetField(ref _id, value);}

        public ImageSource imagem { get => _imagem; set => SetField(ref _imagem, value); }

        public string referencia { get => _referencia; set => SetField(ref _referencia, value); }

        public string descricao { get => _descricao; set => SetField(ref _descricao, value); }

        public decimal valor { get => _valor; set => SetField(ref _valor, value); }

        

    }
}

