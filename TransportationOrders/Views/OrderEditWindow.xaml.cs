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
using TransportationOrders.Models.Entities;
using TransportationOrders.ViewModels;

namespace TransportationOrders
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class OrderEditWindow : Window
    {
        public OrderEditWindow(OrderEditViewModel orderEditViewModel)
        {
            InitializeComponent();
            DataContext = orderEditViewModel;
            orderEditViewModel.CloseRequested += Close;
        }

        
    }
}
