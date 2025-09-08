import './App.css';
import Button from './Button';
import { Order, Change_Order_Status, Create_New_Order, Get_Orders, Get_Orders_Sorted, payment_types, order_states, Ordered_Product } from './API';
import { useState } from 'react';

const empty_product : Ordered_Product = 
{
    name: "",
    quantity: 1,
    price: 0
};

const empty_order : Order =
{
    id : 0,
    customer_name : "",
    state : 0,
    time: "string",
    payment_type : 0,
    delivery_address :
    {
        street : "",
        house_number : "string",
        postcode : 0,
        city : "string",
        country : "string",
    },
    contact_number : ",",
    note : "",
    products : [],
    price_total : 0,
    currency : "string"
};

function App()
{
    const [view, Set_View] = useState<number | null>(null);
    const [orders, Set_Orders] = useState<Order[] | null>(null);
    //const [new_products, Set_New_Products] = useState<Ordered_Product[]>([{...empty_product}])
    //const [new_order, Set_New_Order] = useState<Order>({...empty_order});
    const [changed_order_ID, Set_Changed_Order_ID] = useState<number>(0);
    const [changed_order_state, Set_Changed_Order_State] = useState<number>(0);
    async function Handle_Get_Orders_Button_Click()
    {
        Set_View(1);
        const all_orders : Order[] = await Get_Orders();
        Set_Orders(all_orders);
    }
    async function Handle_Get_Orders_Sorted_Button_Click(highest_price_first : boolean)
    {
        Set_View(highest_price_first === true ? 2 : 3);
        const all_orders_sorted : Order[] = await Get_Orders_Sorted(highest_price_first);
        Set_Orders(all_orders_sorted);
    }

    async function Handle_Change_Order_State_Button_Click()
    {
        const changed_order : Order = await Change_Order_Status(changed_order_ID, changed_order_state);
        Set_Orders(null);
        Set_Orders([changed_order]);
        Set_View(1);
    }
    let view_content;
    switch(view)
    {
        case 0:
        {
            view_content = 
            (
                <p className="IO_Container_String">Sorry not implemented yet. If you want to test sending data, use Change Order Status.</p>
            );
            break;
        }
        case 1:
        case 2:
        case 3:
        {
            if(orders!=null)
            console.log(orders[0]?.id);
            view_content = orders?.map((order, order_index) =>
            (
                <div key={order_index} className="IO_Container_Object">
                    <h3>Customer: {order.customer_name}</h3>
                    <p className="IO_Container_String"><strong>ID:</strong> {orders[order_index].id}</p>
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
            ));
            break;
        }
        case 4:
        {
            view_content = 
            (
                <div className="IO_Container_Change_Order_Status_Object">
                    <p className="IO_Container_Input_String">
                        <strong>Order ID:</strong>
                        <input type="number" min={0} className="IO_Input_Box" onFocus={e => e.currentTarget.addEventListener('wheel', (e: WheelEvent) => {e.preventDefault();}, { passive: false })} onChange={(event) => Set_Changed_Order_ID(Number(event.target.value))}></input>
                    </p>
                    <p className="IO_Container_Input_String">
                        <strong>New State:</strong>
                        <select className="IO_Selection_Box" onChange={(event) => Set_Changed_Order_State(Number(event.target.value))}>
                            {Object.entries(order_states).filter(([key, _]) => !isNaN(Number(key))).map(([key, label]) =>
                            (
                                <option key={key} value={key}>
                                    {label}
                                </option>
                            ))}
                        </select>
                    </p>
                    <div className="IO_Execute_Button" onClick={ Handle_Change_Order_State_Button_Click }>Execute</div>
                </div>
            );
            break;
        }
    }
    return(
        <div className="Content_Container">
            <div className="Header">
                <div className="Title">Restaurant Manager</div>
            </div>
            <div className="Center_Section">
                <div className="Control_Panel">
                    <div className="Control_Panel_Header">Control Panel</div>
                    <Button width="300px" height="38px" color="rgb(134, 187, 162)" Click_Handler={() => Set_View(0)}>Create Order</Button>
                    <Button width="300px" height="38px" color="rgb(135, 159, 186)" Click_Handler={Handle_Get_Orders_Button_Click}>Display Orders</Button>
                    <Button width="300px" height="38px" color="rgb(135, 159, 186)" Click_Handler={() => Handle_Get_Orders_Sorted_Button_Click(false)}>Display Orders Sorted Ascending</Button>
                    <Button width="300px" height="38px" color="rgb(135, 159, 186)" Click_Handler={() => Handle_Get_Orders_Sorted_Button_Click(true)}>Display Orders Sorted Descending</Button>
                    <Button width="300px" height="38px" color="rgb(222, 171, 95)" Click_Handler={() => Set_View(4)}>Change Order Status</Button>
                </div>
                <div className="IO_Container">
                    <div className="IO_Container_Header">Input/Output</div>
                    {view_content}
                </div>
            </div>
        </div>
    );
}

export default App;
