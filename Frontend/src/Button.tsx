import './Button.css';

interface Button_Properties
{
    width: string;
    height: string;
    color: string;
    Click_Handler: (...args : any[]) => void;
    children : React.ReactNode;
}

function Button(properties : Button_Properties)
{
    return(
        <div className="Button_Base" style={{width : properties.width, height : properties.height, backgroundColor : properties.color}} onClick={properties.Click_Handler}>
            {properties.children}
        </div>
    );
}

export default Button;