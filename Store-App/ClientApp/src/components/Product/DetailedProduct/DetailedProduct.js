import React from 'react';
//import { commerce } from "../../lib/commerce";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";

const createMarkup = (text) => {
    return { __html: text };
}

//addProduct
export const DisplayDetailedProduct = (id) => {
    console.log("Calling DisplayDetailedProduct....");
    const [product, setProduct] = useState({});
    
    useEffect(() => {
        async function fetchData() {
            const response = await fetch(`detailedproduct/getDetailedProduct/${id}`);
            const data = await response.json();
            setProduct(data)

        }
        fetchData();  // Fetch product data when the component mounts
    }, [id]);
   
    return (
        <main>
            {product.length > 0 ? (
                product.map((product) => (
                    <div key={product.detailedProductId} className="product">
                            <div className="product-image">
                    </div>
                        <h5 className="product-title">{product.manufacturerInformation}</h5>
                    </div>
                ))
            ) : (
                <div>Loading...</div>
            )}
        </main>
    );
};

export default DisplayDetailedProduct;