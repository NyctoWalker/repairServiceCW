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
    /// Логика взаимодействия для ElementsWindow.xaml
    /// </summary>
    public partial class ElementsWindow : Window
    {
        public ElementsWindow(RepairServiceVM vm, Order order)
        {
            InitializeComponent();
            OrderElementsVM VM = new OrderElementsVM(vm, order);
            DataContext = VM;
            VM.RequestClose += () => this.Close();
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
