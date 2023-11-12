import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { DisplayDetailedProduct } from "../DetailedProduct/DetailedProduct.js";
import "./ProductBox.css";

export const DisplayProductBoxes = () => {

    // Setting the default state to an empty list of products
    const [products, setProducts] = useState([]);
    const [productId, setProductId] = useState("");

    useEffect(() => {
        async function fetchData() {
            const response = await fetch('product/getProducts');
            const data = await response.json();
            setProducts(data);
        }
        fetchData();
    }, []);

    console.log("Products: ", products);

    return (
        <main>
            {products.length > 0 ? (
                products.map((product) => (
                    <div key={product.productId} className="product" onClick={() => setProductId(product.productId)}>
                      <Link to={`detailed-view/${product.productId}`}>
                        <div className="product-image">
                            <img src={product.imageUrl} alt={product.productName} />
                        </div>
                      </Link>
                    <h5 className="product-title">{product.productName}</h5>
                    </div>
                ))
            ) : (
                <div>Loading...</div>
            )}
            {productId && <DisplayDetailedProduct productId={productId} />}
        </main>
    );
}