import { useEffect, useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai'
import "./ProductBox.css"
import { DisplayDetailedProduct } from "../DetailedProduct/DetailedProduct.js";

export const DisplayProductBoxes = () => {

    // Setting the default state to an empty list of products
    const [products, setProducts] = useState([]);
    const [productById, setProductById] = useState("");

    useEffect(() => {
        async function fetchData() {
            const response = await fetch('product/getProducts');
            const data = await response.json();
            setProducts(data);
        }
        fetchData();
    }, []);

    console.log("Products: ", products);

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
                        <div className="product-image">
                            <img src={product.imageUrl} alt={product.productName} />
                        </div>
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