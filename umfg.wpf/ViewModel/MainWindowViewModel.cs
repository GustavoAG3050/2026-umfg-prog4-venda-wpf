using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using umfg.wpf.Abstracts;
using umfg.wpf.Interfaces;

namespace umfg.wpf.ViewModel
{
    internal class MainWindowViewModel : AbstractViewModel, IObserver
    {
        private UserControl _userControl;


            public UserControl UserControl
        {

            get => _userControl;

            set => SetField(ref _userControl, value);

        }


        public MainWindowViewModel ListarProdutos { get; private set; } = new();


        public MainWindowViewModel() : base("Main Screen")
        {
        }

        public void Update(ISubject subject)
        {
            throw new NotImplementedException();


        }

        public void Update(ISubject subject)
        {

            if (subject is ListarProdutosViewModel)
                UserControl = (subject as ListarProdutosViewModel).UserControls;

        }

    }
}
