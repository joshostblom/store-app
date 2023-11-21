import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import { Row } from "react-bootstrap";

export const DisplayDetailedProduct = () => {
    const { productId } = useParams();
    const [productById, setProductById] = useState({});
    const [quantity, setQuantity] = useState(1);



    
    useEffect(() => {
        fetch(`product/getProduct/${productId}`)
            .then((response) => response.json())
            .then((json) => {
                setProductById(json);
            });
    }, [productId]);

    const description = "Description: " + productById?.descript;
    const manufacturer = "Manufacturer: " + productById?.manufacturerInformation;
    const dimensions = "Dimensions: \"" + productById?.prodHeight + "\" X \"" + productById?.prodWidth + "\" X \"" + productById?.prodLength + "\" X \"" + productById?.prodWeight + "\"";


    //IMPLIMENT CARTID
    
    const handleAddToCart = () => {
        /*
        if (cartId) {
            fetch(`/ProductToCart/AddProductToCart/${cartId}/products/${productId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            })
                .then((response) => {
                    if (response.ok) {
                        console.log('Product added to cart');
                    } else {
                        console.error('Failed to add product to cart');
                    }
                })
                .catch((error) => {
                    console.error('Error adding product to cart:', error);
                });
        } else {
            console.error('Cart ID is not available');
        }
        */
    };
    
   
    const handleIncrementQuantity = () => {
        setQuantity((prevQuantity) => prevQuantity + 1);
    };

    const handleDecrementQuantity = () => {
        if (quantity > 1) {
            setQuantity((prevQuantity) => prevQuantity - 1);
        }
    };


    return (
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <Row className="justify-content-md-center">
                        <div style={{ display: 'flex', justifyContent: 'center' }}>
                            <ProductBox product={productById}></ProductBox>
                        </div>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{description}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{manufacturer}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{dimensions}</h6>
                    </Row>
                    




                    <Row className="justify-content-md-center">



                        <div style={{ display: 'flex', justifyContent: 'right' }}>
                            <button className="decrease" onClick={handleDecrementQuantity}>-</button>
                            <span>Qty:{quantity}</span>
                            <button classNmae="increase" onClick={handleIncrementQuantity}>+</button>
                            <button className="btn-primary" onClick={handleAddToCart}>Add to Cart</button>
                        </div>


                    </Row>
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;