using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public static class FileWriter
    {
        public static void WriteOrdersToFile(List<Order> orders, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    //Заголовок
                    writer.WriteLine("Номер заказа;Вес;Район;Время доставки");

                    //Запись заказов
                    foreach(Order order in orders)
                    {
                        writer.WriteLine($"{order.OrderId};{order.Weight};{order.District};{order.DeliveryTime}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка записи в файл: {ex.Message}");
            }
        }
    }
}
