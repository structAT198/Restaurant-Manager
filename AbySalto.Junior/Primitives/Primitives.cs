using AbySalto.Junior.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

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
    public class Ordered_Product_DB
    {
        [JsonIgnore]
        public long ID { get; set; }
        [JsonIgnore]
        public long order_ID { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Order_DB Order_DB { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
    }
    public class Ordered_Product
    {
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