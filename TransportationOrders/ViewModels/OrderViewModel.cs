using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TransportationOrders.Models.Entities;
using TransportationOrders.Models.Repository;
using TransportationOrders.ViewModels.Commands;

namespace TransportationOrders.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private readonly OrdersRepository ordersRepository;

        private ObservableCollection<Models.Entities.Order> _orders = new();
        public ObservableCollection<Models.Entities.Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private string _searchPattern;
        public string SearchPattern
        {
            get { return _searchPattern; }
            set { _searchPattern = value; OnPropertyChanged(); }
        }

        public ICommand AddOrderCommand { get; }
        
        public ICommand GetOrdersFromDB { get; }

        public ICommand DeleteOrderCommand { get; }

        public ICommand OrderEditCommand { get; }

        public ICommand SearchOrderCommand { get; }

        private readonly IServiceProvider serviceProvider;
        public OrderViewModel(OrdersRepository ordersRepository, IServiceProvider serviceProvider)
        {
            this.ordersRepository = ordersRepository;
            this.serviceProvider = serviceProvider;

            AddOrderCommand = new Command(AddOrder);
            GetOrdersFromDB = new Command(GetAllOrdersFromDB);
            DeleteOrderCommand = new Command<int>(DeleteOrder);
            OrderEditCommand = new Command<int>(OrderEdit);
            SearchOrderCommand = new Command<string>(SearchOrders);

            GetOrdersFromDB.Execute(null);

        }
        private async void SearchOrders(string pattern)
        {
            var searchedOrders = await ordersRepository.GetRangeByPattern(pattern, 100);

            Orders.Clear();
            foreach (var order in searchedOrders)
            {
                Orders.Add(order);
            }
        }
        private async void OrderEdit(int id)
        {
            var order = await ordersRepository.Get(id);
            
            if (order == null)
                throw new ArgumentException("there is no order with such id to edit");

            var modelView = serviceProvider.GetRequiredService<OrderEditViewModel>();
            modelView.AssignOrder(order);

            var view = serviceProvider.GetRequiredService<OrderEditWindow>();
            view.Show();
        }
        private async void DeleteOrder(int id)
        {
            if (await ordersRepository.Get(id) == null)
                throw new ArgumentException("there is no order with such id to delete");

            await ordersRepository.Delete(id);

            Orders.Remove(Orders.Where(o => o.Id == id).First());

        }

        private async void AddOrder()
        {
            var order = new Order() { Status = Models.Enum.OrderStatus.New};

            var orderDB = await ordersRepository.Add(order);

            Orders.Add(orderDB);
        }

        private async void GetAllOrdersFromDB()
        {
            var orders = await ordersRepository.GetRange(0, 100);

            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
