using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOrders.Models.Entities
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            if(obj == this) return true;

            if(obj.GetType() != this.GetType()) return false;

            return ((Courier)obj).Id == Id && ((Courier)obj).Name == Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
