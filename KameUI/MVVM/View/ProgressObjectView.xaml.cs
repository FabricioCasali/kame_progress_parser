using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KameUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProgressObjectView.xaml
    /// </summary>
    public partial class ProgressObjectView : UserControl
    {
        public ProgressObjectView(KameProgressParser.ProgressInstruction instrucntion)
        {
            InitializeComponent();
        }
    }
}
