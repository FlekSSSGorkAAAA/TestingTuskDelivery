using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class DeliveryService
    {
        private string _cityDistrict;
        private DateTime _firstDeliveryDateTime;
        private string _deliveryLogFilePath;
        private string _deliveryOrderFilePath;
        public DeliveryService(string cityDistrict, DateTime firstDeliveryDateTime, string deliveryLogFilePath, string deliveryOrderFilePath)
        {
            _cityDistrict = cityDistrict;
            _firstDeliveryDateTime = firstDeliveryDateTime;
            _deliveryLogFilePath = deliveryLogFilePath;
            _deliveryOrderFilePath = deliveryOrderFilePath;
        }

        public void Run()
        {
            try
            {
                //Чтение заказов из файла
                List<Order> orders = FileReader.ReadOrdersFromFile("orders.txt");

                //Фильтрация заказов
                List<Order> filteredOrders = FilterOrders(orders);

                //Запись результатов в файл
                FileWriter.WriteOrdersToFile(filteredOrders, _deliveryOrderFilePath);

                //Логирование
                Log("Фильтрация заказов выполнена успешно.");
            }
            catch (Exception ex)
            {
                Log($"Произошла ошибка: {ex.Message}");
            }
        }

        public List<Order> FilterOrders(List<Order> orders)
        {
            //Фильтрация по району
            List<Order> filteredOrders = orders.Where(o => o.District == _cityDistrict).ToList();

            //Фильтрация по времени
            DateTime endTime = _firstDeliveryDateTime.AddMinutes(30);
            filteredOrders = filteredOrders.Where(o => o.DeliveryTime >= _firstDeliveryDateTime && o.DeliveryTime <= endTime).ToList();

            return filteredOrders;
        }

        private void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_deliveryLogFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }

        public static void Main(string[] args)
        {
            // 1. Проверка корректности входных данных 
            if (args.Length != 4)
            {
                Console.WriteLine("Неправильное количество аргументов командной строки.");
                return;
            }

            string cityDistrict = args[0];
            DateTime firstDeliveryDateTime;
            if (!DateTime.TryParse(args[1], out firstDeliveryDateTime))
            {
                Console.WriteLine("Неверный формат времени первой доставки.");
                return;
            }
            string deliveryLogFilePath = args[2];
            string deliveryOrderFilePath = args[3];

            // 2. Создание экземпляра DeliveryService
            DeliveryService deliveryService = new DeliveryService(cityDistrict, firstDeliveryDateTime, deliveryLogFilePath, deliveryOrderFilePath);

            // 3. Запуск обработки заказов 
            deliveryService.Run();
        }
    }
}
