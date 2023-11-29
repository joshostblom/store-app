import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

//TODO: NEED CART ID

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

            const responseAddToCart = await fetch(`/productToCart/AddProductToCart/${cartId}/products/${productId}`, {
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



    return (
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <tr>
                        <td>
                            <div style={{ display: 'flex', justifyContent: 'center' }}>
                                <ProductBox product={productById}></ProductBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6 className="product-text">{productById?.descript}</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6 className="product-text">{productById?.manufacturerInformation}</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div className="quantity-container">
                                <button className="decrease" onClick={handleDecrementQuantity}>-</button>
                                <h6 className="quantity">QTY: {quantity}</h6>
                                <button className="decreaseButton" onClick={handleIncrementQuantity}>+</button>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div className="addCart-container">
                                <button className="addtoCart" onClick={handleAddToCart}>Add to Cart</button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;