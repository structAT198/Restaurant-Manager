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

        [HttpPost]
        [Route("restaurant_server/create_new_order")]
        public async Task<IActionResult> Create_New_Order(Order new_order)
        {
            new_order.price_total = 0.0f;
            foreach(Ordered_Product product in new_order.products)
            {
                new_order.price_total += (product.price * product.quantity);
            }
            database_context.orders.Add(new_order);
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

        [HttpGet]
        [Route("restaurant_server/get_orders")]
        public IActionResult Get_Orders()
        {
            var orders = database_context.orders.ToList();
            return Ok(orders);
        }

        [HttpGet]
        [Route("restaurant_server/get_orders_sorted")]
        public IActionResult Get_Orders_Sorted(bool highest_price_first)
        {
            if(highest_price_first == true)
            {
                var sorted_orders = database_context.orders.OrderByDescending(order => order.price_total).ToList();
                return Ok(sorted_orders);
            }
            else
            {
                var sorted_orders = database_context.orders.OrderBy(order => order.price_total).ToList();
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