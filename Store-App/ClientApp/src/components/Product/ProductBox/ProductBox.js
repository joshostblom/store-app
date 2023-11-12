import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { DisplayDetailedProduct } from "../DetailedProduct/DetailedProduct.js";
import "./ProductBox.css";

export const DisplayProductBoxes = () => {

    // Setting the default state to an empty list of products
    const [products, setProducts] = useState([]);

    useEffect(() => {
        async function fetchData() {
            const response = await fetch('product/getProducts');
            const data = await response.json();
            setProducts(data);
        }
        fetchData();
    }, []);

    //console.log("Products: ", products);

    const handleProductClick = async (productId) => {
        console.log("Product ID: ", productId);
        fetch(`product/getProduct/${productId}`)
            .then((response) => response.json())
            .then((json) => {
                console.log("Test: ", json);
                setProductById(json);
                // call function to display detailed product box
                DisplayDetailedProduct(productId);
            })
            .catch((reason) => {
                setProductById({ productName: "Request failed! Press f12 to check error." });
            });
    }

    return (
        <main>
            {products.length > 0 ? (
                products.map((product) => (
                    <div key={product.productId} className="product" onClick={() => handleProductClick(product.productId)}>
                      <Link to={`detailed-view/${product.productId}`}>
                        <div className="product-image">
                            <img src={product.imageUrl} alt={product.productName} />
                        </div>
                      </Link>
                    <h5 className="product-title">{product.productName}</h5>
                        {/* <div>{productById.productName}</div> */}
                    </div>
                ))
            ) : (
                <div>Loading...</div>
            )}
        </main>
    );
}