using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TransportationOrders.Models.Entities;
using TransportationOrders.Models.Enum;
using TransportationOrders.Models.Repository;
using TransportationOrders.ViewModels.Commands;

namespace TransportationOrders.ViewModels
{
    public class OrderEditViewModel : INotifyPropertyChanged
    {
        public Action CloseRequested;

        private readonly OrdersRepository ordersRepository;
        private readonly CouriersRepository couriersRepository;

        private Order _currentOrder;
        public Order CurrentOrder
        {
            get { return _currentOrder; }

            set
            {
                _currentOrder = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get {
                return _currentOrder.Description ?? "";
            }
            set
            {
                _currentOrder.Description = value;
                OnPropertyChanged();
            }

        }

        public Array Statuses
        {
            get 
            { 
                if(_currentOrder.Status == OrderStatus.New )
                    return Enum.GetValues(typeof(OrderStatus));
                else
                {
                    var a = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>();
                    return a.Except(new List<OrderStatus>(){ OrderStatus.New}).ToArray();
                }
                    
            }
            set 
            {
                Statuses = value;
                OnPropertyChanged();
            }
        }

        public OrderStatus Status
        {
            get { return _currentOrder.Status; }
            set { _currentOrder.Status = value; OnPropertyChanged(); IsCancelMessageVisible = _currentOrder.Status == OrderStatus.Canceled;}
        }

        private ObservableCollection<int> _courierIds = new();
        public ObservableCollection<int> CourierIds
        {
            get { return _courierIds; }
            set { _courierIds = value; OnPropertyChanged(); }
        }
        public int CourierId
        {
            get { return _currentOrder.CourierId ?? -1; }
            set { _currentOrder.CourierId = value; OnPropertyChanged(); }
        }


        private bool _canEditOrderData;
        
        public bool CanEditOrderData
        {
            get { return _canEditOrderData; }
            set { _canEditOrderData = value; OnPropertyChanged(); }
        }

        private bool _isCancelMessageVisible;
       
        public bool IsCancelMessageVisible { get { return _isCancelMessageVisible; } set {  _isCancelMessageVisible = value; OnPropertyChanged(); } }

        public string CancelMessage { get { return _currentOrder.CancelMessage ?? ""; } set { _currentOrder.CancelMessage = value; OnPropertyChanged(); } }
       
        public ICommand SaveOrderCommand { get; }
        
        public OrderEditViewModel(OrdersRepository ordersRepository, CouriersRepository couriersRepository)
        {
            this.ordersRepository = ordersRepository;
            this.couriersRepository = couriersRepository;

            GetCourierIds();

            SaveOrderCommand = new Command(SaveOrder);
        }
        
        public async Task GetCourierIds()
        {
            var couriers = await couriersRepository.GetRange(0, 100);
            foreach (var courier in couriers)
            {
                CourierIds.Add(courier.Id);
            }
            
        }

        public void AssignOrder(Order o)
        {
            _currentOrder = o;

            switch (_currentOrder.Status)
            {
                case OrderStatus.New:
                    CanEditOrderData = true;
                    break;
                case OrderStatus.Canceled:
                    IsCancelMessageVisible = true;
                    break;
                default:
                    CanEditOrderData = false;
                    break;
            }
        }

        
        private void SaveOrder()
        {
            ordersRepository?.Update(_currentOrder);

            MessageBox.Show("Изменения успешно сохранены");

            _currentOrder.OnPropertyChanged(nameof(Description));
            _currentOrder.OnPropertyChanged(nameof(Status));
            _currentOrder.OnPropertyChanged(nameof(CourierId));
            _currentOrder.OnPropertyChanged(nameof(CancelMessage));
            _currentOrder.OnPropertyChanged(nameof(IsCancelMessageVisible));
            CloseRequested?.Invoke();
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
