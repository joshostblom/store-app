import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import { SaleBanner } from "../../components/Sale/SaleBanner/SaleBanner.js";
import { Row } from "react-bootstrap";

export const DisplayDetailedProduct = () => {
    const { productId } = useParams();
    const [productById, setProductById] = useState({});
    const [quantity, setQuantity] = useState(1);

    const handleDecrementQuantity = () => {
        if (quantity > 1) {
            setQuantity((prevQuantity) => prevQuantity - 1);
        }
    };

    const handleIncrementQuantity = () => {
        if (quantity < 10) {
            setQuantity((prevQuantity) => prevQuantity + 1);
        }
    };

    useEffect(() => {
        fetch(`product/getProduct/${productId}`)
            .then((response) => response.json())
            .then((json) => {
                setProductById(json);
            });
    }, [productId]);

    const handleAddToCart = async () => {
        try {
            const responseCart = await fetch('/cart/GetCartForCurrentUser');

            if (!responseCart.ok) {
                    console.error('Error getting cart for current user');
                return;
            }

            const cartData = await responseCart.json();
            const cartId = cartData.cartId;

            const responseAddToCart = await fetch(`/producttocart/AddProductToCart/${cartId}/products/${productId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (responseAddToCart.ok) {
                console.log('Product added to cart');
            } else {
                console.error('Failed to add product to cart');
            }
        } catch (error) {
            console.error('Error adding product to cart:', error);
        }
    };



    //Check if today's date is within the sale date
    function checkValidSale(sale) {
        if (sale != null) {
            const today = new Date();
            const startDate = new Date(sale.startDate);
            const endDate = new Date(sale.endDate);

            return startDate < today && today < endDate;
        } else {
            return false;
        }
    }

    return (
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <Row className="justify-content-md-center">
                        <div style={{ display: 'flex', justifyContent: 'center' }}>
                            <ProductBox product={productById}></ProductBox>
                        </div>
                    </Row>
                    {onSale && (
                        <Row className="justify-content-md-center">
                            <SaleBanner sale={sale}></SaleBanner>
                        </Row>
                    )}
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{description}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{rating}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{manufacturer}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{sku}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{dimensions}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="decrease" onClick={handleDecrementQuantity}>{dimensions}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="increase" onClick={handleIncrementQuantity}>{dimensions}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="QTY: " >{quantity}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <div style={{ display: 'flex', justifyContent: 'right' }}>
                            <button className="btn-primary" onClick={handleAddToCart}>Add to Cart</button>
                        </div>
                    </Row>
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;