import React from "react";
import { useParams } from 'react-router-dom';
import "./Cart.css";
import { useState, useEffect } from "react";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import { Button, Col, Form, Image, ListGroup, Row } from "react-bootstrap";



export const CartPage = () => {
    const [products, setProducts] = useState([]);
    
    
    useEffect(() => {
        fetch('producttocart/GetProductsInCart')
            .then((response) => response.json())
            .then((json) => {
                setProducts(json);
            });
    }, []);

    //make differnt component for displaying items in cart
    return (
        <div className="product-container">
            {products.length > 0 ?
                //Put each product into a product box
                products?.map((product) => (
                    <ProductBox product={product}></ProductBox>
                ))
                : <div>Loading...</div>}
        </div>
    );
}
export default CartPage;