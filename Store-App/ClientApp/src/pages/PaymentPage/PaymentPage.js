import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./PaymentPage.css";
import { Row } from "react-bootstrap";
import { Col } from "react-bootstrap";

export const PaymentPage = ({ isLoggedIn, setLoggedIn }) => {
    const [street, setStreet] = useState('');
    const [city, setCity] = useState('');
    const [country, setCountry] = useState('');
    const [postalCode, setPostalCode] = useState('');
    const [firstNameTextBoxValue, setFirstNameTextBoxValue] = useState('');
    const [lastNameTextBoxValue, setLastNameTextBoxValue] = useState('');
    const [cardNumberTextBoxValue, setCardNumberTextBoxValue] = useState('');
    const [cartTotal, setCartTotal] = useState({});

    useEffect(() => {
        // Use a function inside useEffect to handle asynchronous code
        const fetchAddressData = async () => {
            try {
                const response = await fetch('address/getaddressusingpersonid');
                const json = await response.json();
                setStreet(json.street);
                setCity(json.city);
                setCountry(json.country);
                setPostalCode(json.postalCode);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        const fetchPaymentData = async () => {
            try {
                const response = await fetch('payment/getpaymentusingpersonid');
                const json = await response.json();
                setFirstNameTextBoxValue(json.cardFirstName);
                setLastNameTextBoxValue(json.cardLastName);
                setCardNumberTextBoxValue(json.cardNumber);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        const fetchCartData = async () => {
            try {
                const response = await fetch('cart/getcartusingpersonid');
                const json = await response.json();
                setCartTotal(json.total);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchAddressData();
        fetchPaymentData();
        fetchCartData();
    }, []);

    const totalPrice = "Total Price: " + cartTotal;

    return (
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <Row className="justify-content-md-start">
                        {/*<input className="text-field" type="text" placeholder="First Name" onChange={(e) => { setFirstNameTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />*/}
                        <Col>
                            <label for="firstName">First Name:</label>
                            <input className="text-field" id="firstName" type="text" value={firstNameTextBoxValue} onChange={(e) => { setFirstNameTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>
                        <Col>
                            <label for="street">Street:</label>
                            <input className="text-field" id="street" type="text" value={street} onChange={(e) => { setStreet(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>
                    </Row>
                    <Row className="justify-content-md-start">
                        <Col>
                            <label for="lastName">Last Name:</label>
                            <input className="text-field" id="lastName" type="text" value={lastNameTextBoxValue} onChange={(e) => { setLastNameTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>
                        <Col>
                            <label for="city">City:</label>
                            <input className="text-field" id="city" type="text" value={city} onChange={(e) => { setCity(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>

                    </Row>
                    <Row className="justify-content-md-start">
                        <Col>
                            <label for="cardNumber">Card Number:</label>
                            <input className="text-field" id="cardNumber" type="text" value={cardNumberTextBoxValue} onChange={(e) => { setCardNumberTextBoxValue(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>
                        <Col>
                            <label for="country">Country:</label>
                            <input className="text-field" id="country" type="text" value={country} onChange={(e) => { setCountry(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>

                    </Row>
                    <Row className="justify-content-md-start">
                        <Col>
                            <label for="postalCode">Postal Code:</label>
                            <input className="text-field" id="postalCode" type="text" value={postalCode} onChange={(e) => { setPostalCode(e.target.value) }} style={{ textAlign: 'center' }} />
                        </Col>
                    </Row>
                    <h6 className="d-flex justify-content-md-end">{totalPrice}</h6>
                    <div style={{ display: 'flex', justifyContent: 'right' }}>
                        {/*<Link to={`/payment/${1}`}>*/}
                        <button className="btn-primary">Place Order</button>
                        {/*    </Link>*/}
                    </div>
                </tbody>
            </table>
        </div>
    );
};