import React from 'react';
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "./DetailedProduct.css";
import { FaCartPlus, FaCheck } from "react-icons/fa";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import { SaleBanner } from "../../components/Sale/SaleBanner/SaleBanner.js";
import { Row } from "react-bootstrap";
import { Link } from 'react-router-dom';

export const DisplayDetailedProduct = ({ isLoggedIn }) => {
    const { productId } = useParams();
    const [productById, setProductById] = useState({});
    const [addedToCart, setAddedToCart] = useState(false);
    const navigate = useNavigate()

    //Create a state for sale
    const [sale, setSale] = useState({});

    useEffect(() => {
        fetch(`product/getProduct/${productId}`)
            .then((response) => response.json())
            .then((json) => {
                setProductById(json);
            });
    }, [productId]);

    const handleAddToCart = async () => {

        if (!isLoggedIn) {
            navigate("/login");
        } else {
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
                    setAddedToCart(true);
                } else {
                    console.error('Failed to add product to cart');
                }
            } catch (error) {
                console.error('Error adding product to cart:', error);
            }
        }
    };

    //Get the sale from the controller
    useEffect(() => {
        if (productById?.saleId != null) {
            fetch(`sale/getSale/${productById.saleId}`)
                .then((response) => response.json())
                .then((json) => {
                    setSale(json);
                });
        }
    }, [productById]);

    const onSale = checkValidSale(sale);
    const description = "Description: " + productById?.descript;
    const rating = "Rating: " + productById?.rating + "/5 Stars";
    const manufacturer = "Manufacturer: " + productById?.manufacturerInformation;
    const sku = "SKU: " + productById?.sku;
    const dimensions = "Dimensions: \"" + productById?.prodHeight + "\" X \"" + productById?.prodWidth + "\" X \"" + productById?.prodLength + "\" X \"" + productById?.prodWeight + "\"";

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
                        <div style={{ display: 'flex', justifyContent: 'right' }}>
                            {addedToCart ? (
                                <button className="btn-primary btn-added-to-cart" onClick={() => navigate("/cart")}>Added to Cart! <FaCheck /></button>
                            ) : (
                                <button className="btn-primary" onClick={handleAddToCart}>Add to Cart <FaCartPlus /> </button>
                            )}
                        </div>
                    </Row>
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;