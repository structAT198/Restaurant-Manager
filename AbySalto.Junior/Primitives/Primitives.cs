using AbySalto.Junior.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Junior.Primitives
{
    public enum states
    {
        undetermined,
        waiting,
        in_progress,
        completed
    }
    public class Address
    {
        public string street { get; set; }
        public string house_number { get; set; }
        public int postcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
    public class Ordered_Product
    {
        public long ID { get; set; }
        public long order_ID { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
    }
    public enum payment_types
    {
        undetermined,
        card,
        cash_on_delivery,
        bank_transfer
    }
}