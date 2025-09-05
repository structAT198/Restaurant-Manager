using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using AbySalto.Junior.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext database_context;

        public RestaurantController(ApplicationDbContext database_context)
        {
            this.database_context = database_context;
        }

        [HttpGet]
        [Route("api/create_test_order")]
        public IActionResult Create_Sample_Order()
        {
            Order test_order = new Order();
            test_order.customer_name = "Marko Markic";
            test_order.state = states.in_progress;
            test_order.time = TimeOnly.FromDateTime(DateTime.Now);
            test_order.payment_type = payment_types.cash_on_delivery;
            address delivery_address = new address();
            delivery_address.street = "Ulica Bukava";
            delivery_address.house_number = "22b";
            delivery_address.postcode = 10203;
            delivery_address.city = "Zagreb";
            delivery_address.country = "Croatia";
            test_order.delivery_address = delivery_address;
            test_order.contact_number = "+385910000000";
            test_order.note = "Drugi kat.";
            product product_1 = new product();
            product_1.name = "Sok";
            product_1.quantity = 1;
            product_1.price = 1.39f;
            product product_2 = new product();
            product_2.name = "Bonboni";
            product_2.quantity = 2;
            product_2.price = 2.19f;
            test_order.products.Add(product_1);
            test_order.products.Add(product_2);
            test_order.price_total = 0.0f;
            foreach(product current_product in test_order.products)
            {
                test_order.price_total += current_product.price;
            }
            test_order.currency = "EUR";
            return Ok(test_order);
        }

        [HttpGet]
        [Route("api/dbtest")]
        public async Task<ActionResult<Order>> Get_From_DB()
        {
            var orders = await database_context.orders.ToListAsync();
            return Ok(orders);
        }
    }
}