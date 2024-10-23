using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public static class FileReader
    {
        public static List<Order> ReadOrdersFromFile(string filePath)
        {
            List<Order> orders = new List<Order>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');

                        //Валидация данных
                        if(parts.Length != 4)
                        {
                            throw new ArgumentException("Некорректный формат входных данных.");
                        }

                        //Парсинг данных
                        int orderId = int.Parse(parts[0]);
                        double weight = double.Parse(parts[1]);
                        string district = parts[2];
                        DateTime deliveryTime = DateTime.Parse(parts[3]);

                        //Создание объекта Order
                        Order order = new Order(orderId, weight, district, deliveryTime);

                        //Добавление заказа в список
                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка чтения файла: {ex.Message}");
            }

            return orders;
        }
    }
}
