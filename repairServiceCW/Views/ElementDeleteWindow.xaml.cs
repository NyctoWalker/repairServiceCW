using repairServiceCW.Models;
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
    /// Логика взаимодействия для ElementDeleteWindow.xaml
    /// </summary>
    public partial class ElementDeleteWindow : Window
    {
        public ElementDeleteWindow(OrderElementsVM vm, OrderElement oe)
        {
            InitializeComponent();
            RedactElementVM VM = new RedactElementVM(vm, oe);
            DataContext = VM;
            VM.RequestClose += () => this.Close();
        }
    }
}
