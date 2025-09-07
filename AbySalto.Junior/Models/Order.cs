using AbySalto.Junior.Primitives;
using System.Text.Json.Serialization;

namespace AbySalto.Junior.Models
{
    public class Order_DB
    {
        [JsonIgnore]
        public long ID { get; set; }
        public string customer_name { get; set; }
        public states state { get; set; }
        public TimeOnly time { get; set; }
        public payment_types payment_type { get; set; }
        public Address delivery_address { get; set; }
        public string contact_number { get; set; }
        public string note { get; set; }
        public List<Ordered_Product_DB> products { get; set; } = new List<Ordered_Product_DB>();
        public float price_total { get; set; }
        public string currency { get; set; }
    }
    public class Order
    {
        public string customer_name { get; set; }
        public states state { get; set; }
        public TimeOnly time { get; set; }
        public payment_types payment_type { get; set; }
        public Address delivery_address { get; set; }
        public string contact_number { get; set; }
        public string note { get; set; }
        public List<Ordered_Product> products { get; set; } = new List<Ordered_Product>();
        public float price_total { get; set; }
        public string currency { get; set; }
    }
}