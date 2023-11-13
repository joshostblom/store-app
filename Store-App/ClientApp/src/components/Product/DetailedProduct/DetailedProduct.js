import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";

export const DisplayDetailedProduct = () => { 
    const { productId } = useParams();
    console.log("Calling DisplayDetailedProduct....");
    const [detailedProduct, setDetailedProduct] = useState({});
    const [productById, setProductById] = useState({});
    console.log("Product Id: ", productId)
    
    useEffect(() => {
        async function fetchDetailedProductData() {
            const response = await fetch(`detailedProduct/GetDetailedProduct/${productId}`);
            const data = await response.json();
            setDetailedProduct(data)
        }
        async function fetchProductData() {
            const response = await fetch(`product/getProduct/${productId}`);
            const data = await response.json();
            setProductById(data)
        }
        fetchDetailedProductData();
        fetchProductData();
    }, [productId]);

    return (
        <main>
            {Object.keys(detailedProduct).length > 0 ? (
                <div className="product">
                    <div className="product-image">
                        <img src={productById.imageUrl} alt={productById.productName} />
                    </div>
                    <h5 className="product-description">{detailedProduct.description}</h5>
                    <h6 className="product-manufacturer-information">{detailedProduct.manufacturerInformation}</h6>
                </div>
            ) : (
                <div>Loading...</div>
            )}
        </main>
    );
};

export default DisplayDetailedProduct;         