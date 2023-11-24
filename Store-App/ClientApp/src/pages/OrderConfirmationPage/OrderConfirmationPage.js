import React from 'react';
import { Link } from 'react-router-dom';
import { Row } from "react-bootstrap";

export const OrderConfirmationPage = ({ isLoggedIn, setLoggedIn }) => {
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