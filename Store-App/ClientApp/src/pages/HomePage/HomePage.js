import { useEffect, useState } from "react";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import "./HomePage.css"
import { Link } from 'react-router-dom';

export const HomePage = () => {

    //Create a state for the products
    const [products, setProducts] = useState([]);

    //Get the products from the controller and set state
    useEffect(() => {
        fetch('product/getProducts')
            .then((response) => response.json())
            .then((json) => {
                setProducts(json);
            });
    }, []);
    console.log(products);
    return (
        <div className="product-container">
            {products.length > 0 ?
                //Put each product into a product box
                products?.map((product) => (
                    <ProductBox product={product}></ProductBox>
                ))
                : <div>Loading...</div>}
            <div style={{ display: 'flex', justifyContent: 'right' }}>
                <Link to={`/payment`}>
                    <button className="btn-primary">Proceed to Checkout</button>
                </Link>
            </div>
        </div>
    );
}