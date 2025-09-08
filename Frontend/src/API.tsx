export enum order_states
{
    "Unknown",
    "Waiting",
    "In Progress",
    "Completed"
}

export enum payment_types
{
    "Unknown",
    "Card",
    "Cash On Delivery",
    "Bank Transfer"
}

export interface Ordered_Product
{
    name: string;
    quantity : number;
    price : number;
}

export interface Order
{
    id : number;
    customer_name : string;
    state : number;
    time: string;
    payment_type : number;
    delivery_address :
    {
        street : string,
        house_number : string;
        postcode : number;
        city : string;
        country : string;
    };
    contact_number : string;
    note : string;
    products : Ordered_Product[];
    price_total : number;
    currency : string;
}

export async function Get_Orders()
{
    const response = await fetch("https://localhost:7056/restaurant_server/get_orders");
    if(response.ok)
    {
        const orders : Order[] = await response.json();
        return orders;
    }
    else
    {
        throw new Error("Failed to fetch orders.");
    }
}

export async function Create_New_Order(new_order : Order)
{
    const response = await fetch("https://localhost:7056/restaurant_server/create_new_order", { method : "POST", headers : { "Content-Type" : "application/json" }, body : JSON.stringify(new_order) });
    if(response.ok)
    {
        const order : Order = await response.json();
        return order;
    }
    else
    {
        throw new Error("Failed to create order.");
    }
}

export async function Get_Orders_Sorted(highest_price_first : boolean)
{
    const response = await fetch(`https://localhost:7056/restaurant_server/get_orders_sorted?highest_price_first=${highest_price_first}`);
    if(response.ok)
    {
        const orders : Order[] = await response.json();
        return orders;
    }
    else
    {
        throw new Error("Failed to fetch sorted orders.");
    }
}

export async function Change_Order_Status(order_ID : number, new_state : number)
{
    const response = await fetch(`https://localhost:7056/restaurant_server/change_order_status?order_ID=${order_ID}&new_state=${new_state}`, { method : "PUT" });
    if(response.ok)
    {
        const order : Order = await response.json();
        return order;
    }
    else
    {
        throw new Error("Failed to change order state.");
    }
}