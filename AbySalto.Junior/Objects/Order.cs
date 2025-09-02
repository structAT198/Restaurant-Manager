using Microsoft.Identity.Client;

namespace AbySalto.Junior.Objects
{
    public class Order
    {
        public enum states
        {
            undetermined,
            waiting,
            in_progress,
            completed
        }
        public struct address
        {
            public string street { get; set; }
            public string house_number { get; set; }
            public int postcode { get; set; }
            public string city { get; set; }
            public string country { get; set; }
        }
        public struct product
        {
            public string name { get; set; }
            public int quantity { get; set; }
            public float price {  get; set; }
        }
        public enum payment_types
        {
            undetermined,
            card,
            cash_on_delivery,
            bank_transfer
        }
        public string customer_name { get; set; }
        public states state { get; set; }
        public TimeOnly time {  get; set; }
        public payment_types payment_type { get; set; }
        public address delivery_address { get; set; }
        public string contact_number { get; set; }
        public string note { get; set; }
        public List<product> products { get; set; } = new List<product>();
        public float price_total { get; set; }
        public string currency { get; set; }
    }
}