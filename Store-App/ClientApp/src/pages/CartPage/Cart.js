import React from "react";
import { useParams, useNavigate } from 'react-router-dom';
import "./Cart.css";
import { useState, useEffect } from "react";
//import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import { CartProduct } from "../../components/Product/CartProduct/CartProduct.js";
import { Link } from 'react-router-dom';
import { Button, Col, Form, Image, ListGroup, Row } from "react-bootstrap";



export const CartPage = ({ isLoggedIn }) => {
    const [cartTotal, setCartTotal] = useState({});
    const [cartProducts, setCartProducts] = useState([]);
    const navigate = useNavigate()
   
    const fetchProductsInCart = async () => {
        try {
            const response = await fetch('producttocart/getproductsincartforcurrentuser');
            const json = await response.json();
            setCartProducts(json);
        } catch (error) {
            console.error('Error fetching data:', error);
            setCartProducts([]);
        }
    };

    useEffect(() => {
        if (!isLoggedIn) {
            navigate("/login")
        } else {
            fetchProductsInCart();
        }
    }, [isLoggedIn]);

    const totalPrice = "Total Price: $" + cartTotal;
    return (
        <div>
            <h1 className="text-center">Shopping Cart ({cartProducts.length})</h1>
            <div className="outer-container" style={{ margin: '0 auto', maxWidth: '800px' }}>
                <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse', backgroundColor: '#f2f2f2' }}>
                    <tbody>
                        <Row>
                            <Col>
                                {cartProducts.length > 0 ?
                                    <div style={{ display: 'flex', flexDirection: 'row', flexWrap: 'wrap' }}>
                                        {cartProducts?.map((product) => (
                                            <CartProduct product={product.product} onRemove={ () => fetchProductsInCart() }></CartProduct>
                                        ))}
                                    </div>
                                    : <div><h2>Empty Cart</h2></div>}
                            </Col>
                            <Col>
                                <h6 className="d-flex justify-content-md-center">{totalPrice}</h6>
                                <div style={{ display: 'flex', justifyContent: 'center' }}>
                                    <Link to={`/order-confirmation`}>
                                        <button className="btn btn-primary">Place Order</button>
                                    </Link>
                                    <Link to={`/payment`}>
                                        <button className="btn btn-success">Proceed to Checkout</button>
                                    </Link>

                                </div>
                            </Col>
                        </Row>
                    </tbody>
                </table>
            </div>
        </div>
    );
};
export default CartPage;