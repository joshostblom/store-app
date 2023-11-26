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
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;