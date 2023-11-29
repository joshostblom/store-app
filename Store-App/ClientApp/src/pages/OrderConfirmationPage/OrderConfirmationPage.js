import React from 'react';
import { Link } from 'react-router-dom';
import { Row } from "react-bootstrap";
import { useState, useEffect } from "react";

export const OrderConfirmationPage = ({ isLoggedIn, setLoggedIn }) => {
    useEffect(() => {
        const emptyCart = async () => {
            try {
                const response = await fetch('producttocart/removeallproductsfromcartforcurrentuser', { method: 'DELETE' });

                if (!response.ok) {
                    // Handle error response
                    console.error(`Error: ${response.status} - ${response.statusText}`);
                } else {
                    // Handle success response
                    console.log('Products successfully removed from the cart');
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        const setCartTotalToZero = async () => {
            try {
                const response = await fetch('cart/setcurrentcarttotaltozero', { method: 'PUT' });

                if (!response.ok) {
                    // Handle error response
                    console.error(`Error: ${response.status} - ${response.statusText}`);
                } else {
                    // Handle success response
                    console.log('Cart Total Successfully set to Zero.');
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        emptyCart();
        setCartTotalToZero();
    }, []);
    return (
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <Row className="text-center justify-content-center align-items-center">
                        <h2>Order has been placed!</h2>
                    </Row>
                    <Row className="text-center justify-content-center align-items-center">
                        <Link to={`/`}>
                            <button className="btn btn-primary">Continue Shopping</button>
                        </Link>
                    </Row>
                </tbody>
            </table>
        </div>
    );
};