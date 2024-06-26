using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransportationOrders.Models.Entities;
using TransportationOrders.ViewModels;

namespace TransportationOrders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly OrderViewModel orderViewModel;
        public MainWindow(OrderViewModel orderViewModel)
        {
            InitializeComponent();
            this.orderViewModel = orderViewModel;
            this.DataContext = orderViewModel;            
        }
    }
}