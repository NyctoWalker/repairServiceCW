using repairServiceCW.ViewModels;
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
using System.Windows.Shapes;

namespace repairServiceCW.Views
{
    /// <summary>
    /// Логика взаимодействия для ElementAddWindow.xaml
    /// </summary>
    public partial class ElementAddWindow : Window
    {
        public ElementAddWindow(OrderElementsVM vm)
        {
            InitializeComponent();
            AddElementVM VM = new AddElementVM(vm);
            DataContext = VM;
            VM.RequestClose += () => this.Close();
        }
    }
}
