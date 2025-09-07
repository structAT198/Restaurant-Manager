using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using AbySalto.Junior.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Controllers
{
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext database_context;

        public RestaurantController(ApplicationDbContext database_context)
        {
            this.database_context = database_context;
        }

        [HttpPost]
        [Route("restaurant_server/create_new_order")]
        public async Task<IActionResult> Create_New_Order([FromBody] Order new_order)
        {
            if(new_order != null)
            {
                Order_DB new_order_DB = new Order_DB();
                new_order_DB.customer_name = new_order.customer_name;
                new_order_DB.state = new_order.state;
                new_order_DB.time = new_order.time;
                new_order_DB.payment_type = new_order.payment_type;
                new_order_DB.delivery_address = new_order.delivery_address;
                new_order_DB.contact_number = new_order.contact_number;
                new_order_DB.note = new_order.note;
                foreach(Ordered_Product product in new_order.products)
                {
                    Ordered_Product_DB current_ordered_product_DB = new Ordered_Product_DB();
                    current_ordered_product_DB.Order_DB = new_order_DB;
                    current_ordered_product_DB.name = product.name;
                    current_ordered_product_DB.quantity = product.quantity;
                    current_ordered_product_DB.price = product.price;
                    new_order_DB.products.Add(current_ordered_product_DB);
                }
                new_order_DB.price_total = new_order.price_total;
                new_order_DB.price_total = 0.0f;
                new_order_DB.currency = new_order.currency;
                foreach(Ordered_Product_DB product in new_order_DB.products)
                {
                    new_order_DB.price_total += (product.price * product.quantity);
                }
                database_context.orders.Add(new_order_DB);
                try
                {
                    await database_context.SaveChangesAsync();
                    return Ok(new_order);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return StatusCode(500, "Database error.");
                }
            }
            else
            {
                return StatusCode(400, "Input data invalid.");
            }
        }

        [HttpGet]
        [Route("restaurant_server/get_orders")]
        public IActionResult Get_Orders()
        {
            var orders = database_context.orders.Include(order => order.products).ToList();
            return Ok(orders);
        }

        [HttpGet]
        [Route("restaurant_server/get_orders_sorted")]
        public IActionResult Get_Orders_Sorted(bool highest_price_first)
        {
            if(highest_price_first == true)
            {
                var sorted_orders = database_context.orders.OrderByDescending(order => order.price_total).Include(order => order.products).ToList();
                return Ok(sorted_orders);
            }
            else
            {
                var sorted_orders = database_context.orders.OrderBy(order => order.price_total).Include(order => order.products).ToList();
                return Ok(sorted_orders);
            }
        }

        [HttpPut]
        [Route("restaurant_server/change_order_status")]
        public async Task<IActionResult> Change_Order_State(long order_ID, states new_state)
        {
            var order = database_context.orders.Find(order_ID);
            order.state = new_state;
            try
            {
                await database_context.SaveChangesAsync();
                return Ok(order);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(500, "Database error.");
            }
            
        }
    }
}