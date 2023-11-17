import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

export const DisplayDetailedProduct = () => {
    const { productId } = useParams();
    const [productById, setProductById] = useState({});

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
                            <ProductBox product={productById}></ProductBox>
                            {/*<div style={{ display: 'flex', justifyContent: 'center' }}>
                                <img src={productById.imageUrl} alt={productById.productName} style={{ maxWidth: '20%' }}/>
                            </div>*/}
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
                            <h6 className="product-text" step=".01"><span>&#36;</span>{productById?.price}</h6>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;