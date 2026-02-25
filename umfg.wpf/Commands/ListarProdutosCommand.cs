using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfg.wpf.Abstracts;
using umfg.wpf.UserControls;
using umfg.wpf.ViewModel;

namespace umfg.wpf.Commands
{
    internal sealed class ListarProdutosCommand : AbstractCommand


        public override void Execute
    { 
        ucListarProdutos.Show(parameter as MainWindowViewModel);
    }
}
