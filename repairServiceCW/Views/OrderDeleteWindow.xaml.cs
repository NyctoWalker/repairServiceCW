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
    public partial class OrderDeleteWindow : Window
    {
        public OrderDeleteWindow(RepairServiceVM vm, Order order)
        {
            InitializeComponent();
            RedactOrderVM VM = new RedactOrderVM(vm, order);
            DataContext = VM;
            VM.RequestClose += () => this.Close();
        }
    }
}
