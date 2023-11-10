import React from 'react';
//import { commerce } from "../../lib/commerce";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";

const createMarkup = (text) => {
    return { __html: text };
}

//addProduct
export const DisplayDetailedProduct = () => { 
    const { productId } = useParams();
    console.log("Calling DisplayDetailedProduct....");
    const [product, setProduct] = useState({});
    console.log("after use state")
    
    useEffect(() => {
        async function fetchData() {
            const response = await fetch(`product/GetProduct/${productId}`);
            const data = await response.json();
            setProduct(data)
            console.log("In use effect")
        }
        fetchData();  // Fetch product data when the component mounts
    }, [productId]);

    console.log("DetailedProduct: ", product)

    /*
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
*/

    return (
        <main>
            {Object.keys(product).length > 0 ? (
                <div className="product">
                    <div className="product-image">
                        <img src={product.imageUrl} alt={product.productName} />
                    </div>
                    <h5 className="product-title">{product.productName}</h5>
                    <h6 className="price">{product.price}</h6>
                </div>
            ) : (
                <div>Loading...</div>
            )}
        </main>
    );
};

export default DisplayDetailedProduct;         