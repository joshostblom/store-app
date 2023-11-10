import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { AiOutlineSearch } from 'react-icons/ai'
import "./ProductBox.css"

export const DisplayProductBoxes = () => {

    // Setting the default state to an empty list of products
    const [products, setProducts] = useState([]);
    const dataType = 1;

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
                    <div key={product.productId} className="product">
                        <Link to={`/detailed-product/${product.productId}`}>
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
        </main>
    );
}