import './App.css';
import Button from './Button';
import { Order, Change_Order_Status, Create_New_Order, Get_Orders, Get_Orders_Sorted, payment_types, order_states } from './API';
import { useState } from 'react';

function App()
{
    const [orders, Set_Orders] = useState<Order[] | null>(null);
    async function Handle_Get_Orders_Button_Click()
    {
        const all_orders : Order[] = await Get_Orders();
        Set_Orders(all_orders);
    }
    async function Handle_Get_Orders_Sorted_Button_Click(highest_price_first : boolean)
    {
        const all_orders_sorted : Order[] = await Get_Orders_Sorted(highest_price_first);
        Set_Orders(all_orders_sorted);
    }

    async function Handle_Change_Order_State_Button_Click(order_ID : number, new_state : number)
    {
        const changed_order : Order = await Change_Order_Status(order_ID, new_state);
        Set_Orders(null);
        Set_Orders([changed_order]);
    }
    
    return(
        <div className="Content_Container">
            <div className="Header">
                <div className="Title">Restaurant Manager</div>
            </div>
            <div className="Center_Section">
                <div className="Control_Panel">
                    <div className="Control_Panel_Header">Control Panel</div>
                    <Button width="300px" height="38px" color="rgb(134, 187, 162)" Click_Handler={Create_New_Order}>Create Order</Button>
                    <Button width="300px" height="38px" color="rgb(135, 159, 186)" Click_Handler={Handle_Get_Orders_Button_Click}>Display Orders</Button>
                    <Button width="300px" height="38px" color="rgb(135, 159, 186)" Click_Handler={() => Handle_Get_Orders_Sorted_Button_Click(false)}>Display Orders Sorted Ascending</Button>
                    <Button width="300px" height="38px" color="rgb(135, 159, 186)" Click_Handler={() => Handle_Get_Orders_Sorted_Button_Click(true)}>Display Orders Sorted Descending</Button>
                    <Button width="300px" height="38px" color="rgb(222, 171, 95)" Click_Handler={() => Handle_Change_Order_State_Button_Click(10, 1)}>Change Order Status</Button>
                </div>
                <div className="IO_Container">
                    <div className="IO_Container_Header">Input/Output</div>
                        {
                            orders?.map((order, order_index) =>
                            (
                                <div key={order_index} className="IO_Container_Object">
                                    <h3>Customer: {order.customer_name}</h3>
                                    <p className="IO_Container_String"><strong>State:</strong> {order_states[order.state]}</p>
                                    <p className="IO_Container_String"><strong>Time:</strong> {order.time}</p>
                                    <p className="IO_Container_String"><strong>Payment Type:</strong> {payment_types[order.payment_type]}</p>
                                    <p className="IO_Container_String"><strong>Contact:</strong> {order.contact_number}</p>
                                    <p className="IO_Container_String"><strong>Address:</strong> {order.delivery_address.street} {order.delivery_address.house_number}, {order.delivery_address.postcode}, {order.delivery_address.city}, {order.delivery_address.country}</p>
                                    <p className="IO_Container_String"><strong>Note:</strong> {order.note}</p>
                                    <p className="IO_Container_String"><strong>Products:</strong></p>
                                    <ul className="IO_Container_List">
                                        {order.products.map((product, product_index) =>
                                        (
                                            <li key={product_index} className="IO_Container_List">
                                                {product.quantity}x {product.name}, {product.price} {order.currency}
                                            </li>
                                        ))}
                                    </ul>
                                    <p className="IO_Container_String_Spaced"><strong>Total Price:</strong> {order.price_total} {order.currency}</p>
                                </div>
                            ))
                        }
                </div>
            </div>
        </div>
    );
}

export default App;
