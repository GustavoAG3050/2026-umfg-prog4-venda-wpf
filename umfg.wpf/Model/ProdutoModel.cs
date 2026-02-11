using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace umfg.wpf.Model
{
    public class ProdutoModel
    {
        private Guid _id = Guid.NewGuid();
        private ImageSource _imagem;
        private string _referencia;
        private string _descricao;
        private decimal _valor;



        public Guid id { get => _id; set => _id = value; }

        public ImageSource imagem { get => _imagem; set => _imagem = value; }

        public string referencia { get => _referencia; set => _referencia = value; }

        public string descricao { get => _descricao; set => _descricao = value; }

        public decimal valor { get => _valor; set => _valor = value; }

        

    }
}
