using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestione_Magazzino.Core
{
    public class Order
    {
        public Order(int id, DateTime dateTime, List<WarehouseItem> items)
        {
            Id = id;
            DateTime = dateTime;
            Items = items;
            TotalPrice = CalculateTotalPrice(Items);
        }

        public int Id { get; }

        public DateTime DateTime { get; }
        public List<WarehouseItem> Items { get; }
        public decimal TotalPrice { get; }

        private static decimal CalculateTotalPrice(IEnumerable<WarehouseItem> items)
        {
            return items.Sum(item => item.Price * item.Quantity);
        }
    }
}