using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TransportationOrders.Models.Enum;

namespace TransportationOrders.Models.Entities
{
    public class Order : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public OrderStatus Status { get; set; }

        public int? CourierId { get; set; }

        public string? CancelMessage { get; set; }

        
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
