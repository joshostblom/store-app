import React from 'react';
import { useState, useEffect } from "react";
import "./PaymentPage.css";
import { Row } from "react-bootstrap";
import { Col } from "react-bootstrap";
import { Link } from 'react-router-dom';
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

export const PaymentPage = ({ isLoggedIn, setLoggedIn }) => {
    const [street, setStreet] = useState('');
    const [city, setCity] = useState('');
    const [state, setState] = useState('');
    const [country, setCountry] = useState('');
    const [postalCode, setPostalCode] = useState('');
    const [firstNameTextBoxValue, setFirstNameTextBoxValue] = useState('');
    const [lastNameTextBoxValue, setLastNameTextBoxValue] = useState('');
    const [cardNumberTextBoxValue, setCardNumberTextBoxValue] = useState('');
    const [cardCvv, setCardCvv] = useState('');
    const [cardExpirationDate, setCardExpirationDate] = useState('');
    const [cartTotal, setCartTotal] = useState({});
    const [cartProducts, setCartProducts] = useState([]);

    useEffect(() => {
        const fetchAddressData = async () => {
            try {
                const response = await fetch('address/getaddressforcurrentuser');
                const json = await response.json();
                setStreet(json.street);
                setCity(json.city);
                setState(json.state);
                setCountry(json.country);
                setPostalCode(json.postalCode);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        const fetchPaymentData = async () => {
            try {
                const response = await fetch('payment/getpaymentforcurrentuser');
                const json = await response.json();
                setFirstNameTextBoxValue(json.cardFirstName);
                setLastNameTextBoxValue(json.cardLastName);
                setCardNumberTextBoxValue(json.cardNumber);
                setCardCvv(json.cvv);
                setCardExpirationDate(json.expirationDate);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        const fetchCartData = async () => {
            try {
                const response = await fetch('cart/getcartforcurrentuser');
                const json = await response.json();
                setCartTotal(json.total);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        const fetchProductsInCart = async () => {
            try {
                const response = await fetch('producttocart/getproductsincartforcurrentuser');
                const json = await response.json();
                setCartProducts(json)
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchAddressData();
        fetchPaymentData();
        fetchCartData();
        fetchProductsInCart();
    }, []);

    const totalPrice = "Total Price: $" + cartTotal;
    const expirationDate = cardExpirationDate.substring(0, 10)
    return (
        <div>
            <h1 className="text-center">Checkout</h1>
            <div className="outer-container" style={{ margin: '0 auto', maxWidth: '800px' }}>
                <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse', backgroundColor: '#f2f2f2' }}>
                    <tbody>
                        <Row>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="firstName" className="text-right">First Name:</label>
                                        <input className="text-field" id="firstName" type="text" value={firstNameTextBoxValue} onChange={(e) => { setFirstNameTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="street" className="text-right">Street:</label>
                                        <input className="text-field" id="street" type="text" value={street} onChange={(e) => { setStreet(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                        </Row>
                        <Row>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="lastName">Last Name:</label>
                                        <input className="text-field" id="lastName" type="text" value={lastNameTextBoxValue} onChange={(e) => { setLastNameTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="city">City:</label>
                                        <input className="text-field" id="city" type="text" value={city} onChange={(e) => { setCity(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                        </Row>
                        <Row>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="cardNumber">Card Number:</label>
                                        <input className="text-field" id="cardNumber" type="text" value={cardNumberTextBoxValue} onChange={(e) => { setCardNumberTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                            <Col className="center-column">
                                <form>  
                                    <div className="form-group">
                                        <label htmlFor="state">State:</label>
                                        <input className="text-field" id="state" type="text" value={state} onChange={(e) => { setState(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                        </Row>
                        <Row>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="cardCvv">CVV:</label>
                                        <input className="text-field" id="cardCvv" type="text" value={cardCvv} onChange={(e) => { setCardCvv(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="country">Country:</label>
                                        <input className="text-field" id="postalCode" type="text" value={country} onChange={(e) => { setCountry(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                        </Row>
                        <Row>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="cardExpirationDate">Expiration Date:</label>
                                        <input className="text-field" id="cardExpirationDate" type="text" value={expirationDate} onChange={(e) => { setCardExpirationDate(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                            <Col className="center-column">
                                <form>
                                    <div className="form-group">
                                        <label htmlFor="postalCode">Postal Code:</label>
                                        <input className="text-field" id="postalCode" type="text" value={postalCode} onChange={(e) => { setPostalCode(e.target.value) }} style={{ textAlign: 'center' }} />
                                    </div>
                                </form>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                {cartProducts.length > 0 ?
                                    <div style={{ display: 'flex', flexDirection: 'row' }}>

                                        {cartProducts?.map((product) => (
                                            <ProductBox product={product.product}></ProductBox>
                                        ))}
                                    </div>
                                    : <div><h2>Empty Cart</h2></div>}
                            </Col>
                            <Col>
                                <h6 className="d-flex justify-content-md-center">{totalPrice}</h6>
                                <div style={{ display: 'flex', justifyContent: 'center' }}>
                                    <Link to={`/order-confirmation`}>
                                        <button className="btn-primary">Place Order</button>
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