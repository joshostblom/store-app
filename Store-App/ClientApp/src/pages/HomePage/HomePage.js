import { useEffect, useState } from "react";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import "./HomePage.css"

export const HomePage = () => {

    const [products, setProducts] = useState([]);

    useEffect(() => {
        fetch('product/getProducts')
            .then((response) => response.json())
            .then((json) => {
                setProducts(json);
            });
    }, []);

    return (
        <div className="product-container">
            {products.length > 0 ?
                products?.map((product) => (
                    <ProductBox product={product}></ProductBox>
                ))
                : <div>Loading...</div>}
        </div>
    );
}