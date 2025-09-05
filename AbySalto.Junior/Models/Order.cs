using AbySalto.Junior.Primitives;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Junior.Models
{
    public class Order
    {
        [Key]
        public long key { get; set; }
        public string customer_name { get; set; }
        [NotMapped]
        public states state { get; set; }
        [NotMapped]
        public TimeOnly time { get; set; }
        [NotMapped]
        public payment_types payment_type { get; set; }
        [NotMapped]
        public address delivery_address { get; set; }
        [NotMapped]
        public string contact_number { get; set; }
        [NotMapped]
        public string note { get; set; }
        [NotMapped]
        public List<product> products { get; set; } = new List<product>();
        [NotMapped]
        public float price_total { get; set; }
        [NotMapped]
        public string currency { get; set; }
    }
}