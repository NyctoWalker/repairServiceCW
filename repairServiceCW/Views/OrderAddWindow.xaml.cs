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
    public partial class OrderAddWindow : Window
    {
        public OrderAddWindow(RepairServiceVM vm)
        {
            InitializeComponent();
            AddOrderVM VM = new AddOrderVM(vm);
            DataContext = VM;
            VM.RequestClose += () => this.Close();
        }
    }
}
