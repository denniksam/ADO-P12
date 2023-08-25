using ADO_P12.DAL.Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADO_P12.DAL.DAO
{
    internal class ProductGroupDao
    {
        private readonly SqlConnection _connection;

        public ProductGroupDao(SqlConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Одержання усіх невидалених товарних груп
        /// </summary>
        /// <returns>Групи у вигляді колекції</returns>
        public List<Entity.ProductGroup> GetAll()
        {
            using SqlCommand command = new();
            command.Connection = _connection;
            command.CommandText = "SELECT pg.* FROM ProductGroups pg WHERE pg.DeleteDt IS NULL";
            try
            {
                using SqlDataReader reader = command.ExecuteReader();
                var ProductGroups = new List<Entity.ProductGroup>();
                while (reader.Read())  // get result's one row
                {
                    ProductGroups.Add(new()
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Picture = reader.GetString(3),
                    });
                }
                return ProductGroups;
            }
            catch { throw; }
        }

        public void Add(Entity.ProductGroup productGroup)
        {
            using SqlCommand command = new();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO ProductGroups( Id, Name, Description, Picture ) VALUES (@id, @name, @description, @picture) ";
            // підготовка запиту -- утворення тимчасової збереженої процедури у СУБД
            command.Prepare();
            // задаємо типи та обмеження (за наявності) параметрів
            command.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50));
            command.Parameters.Add(new SqlParameter("@description", SqlDbType.NText));
            command.Parameters.Add(new SqlParameter("@picture", SqlDbType.NVarChar, 50));
            // задаємо значення параметрів
            command.Parameters[0].Value = productGroup.Id;
            command.Parameters[1].Value = productGroup.Name;
            command.Parameters[2].Value = productGroup.Description;
            command.Parameters[3].Value = productGroup.Picture;
            // виконуємо команду
            command.ExecuteNonQuery();
        }

        // зробити метод Delete, використати підготовлений запит

        /* Перенести метод Update до класу ProductGroupDao
         * Реалізувати шар DAO у власному проєкті
         * ** Додати методи
         * - GetDeleted() - List видалених груп
         * - Restore(entity) - відновлення видаленої групи
         * - У вікні додати кнопку/чекбокс "показувати видалені групи"
         */
    }
}
/* Підготовлені (параметризовані) запити
 * Технологія виконання запитів з відокремленними параметрами
 * Проблема - коли дані вставляються в код (запит) вони можуть 
 * спотворити цей запит, наприклад
 * $"INSERT INTO Users VALUES(N'{Name}' ...)
 * Name = "Лук'ян"
 *   INSERT INTO Users VALUES(N'Лук'ян' ...)  -- SQL Error
 */
