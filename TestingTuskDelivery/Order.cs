using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Weight { get; set; }
        public string District { get; set; }
        public DateTime DeliveryTime { get; set; }

        public Order(int orderId, double weight, string district, DateTime deliveryTime)
        {
            OrderId = orderId;
            Weight = weight;
            District = district;
            DeliveryTime = deliveryTime;
        }
    }
}
