using Gestione_Magazzino.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Gestione_Magazzino.Core
{
    public static class DB
    {
        public static readonly string ConnetionString =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
            "\\magazzino.mdf; Integrated Security=True; MultipleActiveResultSets=True;";

        private static SqlConnection _sqlConnection;

        public static void InitializeConnection()
        {
            try
            {
                _sqlConnection = new SqlConnection(ConnetionString);
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB InitializeConnection(): " + ex.Message));
                throw;
            }

            Logger.Append(new Message(Message.EventTypes.Info, "DB Connection ready."));
        }

        public static void CloseConnection()
        {
            _sqlConnection?.Close();
            Logger.Append(new Message(Message.EventTypes.Info, "DB Connection closed."));
        }

        public static DataTable GetWarehouseContent()
        {
            try
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(
                    "SELECT * FROM Warehouse", _sqlConnection))
                {
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB GetWarehouseContent(): " + ex.Message));
                throw;
            }
        }

        //public static bool CheckWarehouseItemExists(string id)
        //{
        //    SqlCommand sqlCommand = new SqlCommand("SELECT count(*) FROM Warehouse WHERE Id = '" + id + "'",
        //        _sqlConnection);
        //    return (int) sqlCommand.ExecuteScalar() > 0;
        //}

        public static void AddItemToWarehouse(string id, string name, decimal price, int quantity)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Warehouse (Id, Name, Price, Quantity) " +
                                                       "VALUES ('" + id + "', '" + name + "', '" + price + "', '" +
                                                       quantity +
                                                       "')",
                    _sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB AddItemToWarehouse(): " + ex.Message));
                throw;
            }

            Logger.Append(new Message(Message.EventTypes.Info,
                "Item ID " + id + " (x" + quantity + ") added to Warehouse."));
        }

        public static void ModifyWarehouseItem(string id, string newName, decimal newPrice, int newQuantity)
        {
            try
            {
                SqlCommand sqlCommand =
                    new SqlCommand(
                        "UPDATE  Warehouse SET Name = '" + newName + "',Price = '" + newPrice + "',Quantity = '" +
                        newQuantity + "' WHERE Id = '" + id + "'", _sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB ModifyWarehouseItem(): " + ex.Message));
                throw;
            }
            Logger.Append(new Message(Message.EventTypes.Info, "Warehouse Item ID " + id + " modified."));
        }

        public static void DeleteWarehouseItemById(string id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM Warehouse WHERE Id = '" + id + "'",
                    _sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB DeleteWarehouseItemById(): " + ex.Message));
                throw;
            }

            Logger.Append(new Message(Message.EventTypes.Info, "Warehouse Item ID " + id + " deleted."));
        }

        public static void RegisterOrder(IEnumerable<WarehouseItem> items)
        {
            int orderId;
            try
            {
                SqlCommand sqlCommand2 =
                    new SqlCommand(
                        "INSERT INTO Orders (DateTime) VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                        "') SELECT SCOPE_IDENTITY()", _sqlConnection);

                orderId = Convert.ToInt32(sqlCommand2.ExecuteScalar());

                foreach (WarehouseItem item in items)
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(
                            "INSERT INTO OrderedItems(OrderId, ItemId, ItemName, Quantity, UnitPrice) VALUES ('" +
                            orderId +
                            "', '" + item.Id + "', '" + item.Name + "', '" + item.Quantity +
                            "', '" + item.Price + "')", _sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB RegisterOrder(): " + ex.Message));
                throw;
            }

            Logger.Append(new Message(Message.EventTypes.Info, "Order ID " + orderId + " registered."));
        }

        public static void SubtractItemsFromWarehouse(IEnumerable<WarehouseItem> wareHouseItems)
        {
            foreach (WarehouseItem warehouseItem in wareHouseItems)
            {
                SubtractItemFromWarehouse(warehouseItem);
            }
        }

        public static void SubtractItemFromWarehouse(WarehouseItem warehouseItem)
        {
            try
            {
                SqlCommand sqlCommand =
                    new SqlCommand(
                        "UPDATE Warehouse SET Quantity = Quantity - " + warehouseItem.Quantity + " WHERE Id = '" +
                        warehouseItem.Id + "'",
                        _sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "DB SubtractItemFromWarehouse(): " + ex.Message));
                throw;
            }
            Logger.Append(new Message(Message.EventTypes.Info,
                "Item ID " + warehouseItem.Id + " x" + warehouseItem.Quantity + " subtracted from Warehouse."));
        }

        /// <summary>
        ///     Get WarehouseItem object by Id
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Ritorna null se l'Item non è stato trovato nel DB</returns>
        public static WarehouseItem GetWarehouseItemById(string id)
        {
            try
            {
                SqlCommand sqlCommand =
                    new SqlCommand(
                        "SELECT Warehouse.Name, Warehouse.Price, Warehouse.Quantity FROM Warehouse WHERE Id = '" + id +
                        "'",
                        _sqlConnection);
                SqlDataReader ex = sqlCommand.ExecuteReader();
                ex.Read();
                return !ex.HasRows ? null : new WarehouseItem(id, ex.GetString(0), ex.GetDecimal(1), ex.GetInt32(2));
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore,
                    "DB GetWarehouseItemById(): " + ex.Message));
                throw;
            }
        }

        public static List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(
                    "SELECT * FROM Orders", _sqlConnection))
                {
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        object[] values = row.ItemArray;
                        int orderId = (int)values[0];

                        List<WarehouseItem> orderedItems = new List<WarehouseItem>();
                        using (SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(
                            "SELECT * FROM OrderedItems WHERE OrderId = '" + orderId + "'", _sqlConnection))
                        {
                            DataTable dataTable2 = new DataTable();
                            sqlDataAdapter2.Fill(dataTable2);
                            foreach (DataRow row2 in dataTable2.Rows)
                            {
                                object[] values2 = row2.ItemArray;
                                orderedItems.Add(new WarehouseItem((string)values2[1], (string)values2[2],
                                    (decimal)values2[4], (int)values2[3]));
                            }
                        }
                        orders.Add(new Order(orderId, DateTime.Parse(values[1].ToString()), orderedItems));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Append(new Message(Message.EventTypes.Errore, "Error DB GetOrders(): " + ex.Message));
            }

            return orders;
        }
    }
}