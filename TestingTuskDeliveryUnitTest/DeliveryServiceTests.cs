using DeliveryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingTuskDeliveryUnitTest
{
    [TestClass]
    public class DeliveryServiceTests
    {
        [TestMethod]
        public void FilterOrders_ShouldReturnCorrectOrders()
        {
            //Тестовые данные
            DeliveryService.DeliveryService deliveryService = new DeliveryService.DeliveryService("Центральный", new DateTime(2024, 10, 22, 10, 00, 00), "deliveryLog.txt", "deliveryOrder.txt");

            List<Order> orders = new List<Order>()
            {
                new Order(1, 10, "Центральный", new DateTime(2024, 10, 22, 10, 00, 00)),
                new Order(2, 5, "Западный", new DateTime(2024, 10, 22, 10, 15, 00)),
                new Order(3, 8, "Центральный", new DateTime(2024, 10, 22, 10, 20, 00)),
                new Order(4, 12, "Восточный", new DateTime(2024, 10, 22, 10, 35, 00)),
            };

            //Вызов фильтра
            List<Order> filtredOrders = deliveryService.FilterOrders(orders);

            //Тесты
            Assert.AreEqual(2, filtredOrders.Count);
            Assert.IsTrue(filtredOrders.Any(o => o.OrderId == 1));
            Assert.IsTrue(filtredOrders.Any(o => o.OrderId == 3));
        }
    }
}
