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
            const response = await fetch(`detailedproduct/GetDetailedProduct/${productId}`);
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
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <tr>
                        <td>
                            <div style={{ display: 'flex', justifyContent: 'center' }}>
                                <img src={productById.imageUrl} alt={productById.productName} style={{ maxWidth: '20%' }}/>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6 className="product-text">{detailedProduct.description}</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6 className="product-text">{detailedProduct.manufacturerInformation}</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6 className="product-text" step=".01"><span>&#36;</span>{productById.price}</h6>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
};

export default DisplayDetailedProduct;         