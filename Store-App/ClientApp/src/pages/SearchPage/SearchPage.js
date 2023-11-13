import { useEffect, useState } from "react"
import { useParams } from "react-router-dom";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

export const SearchPage = () => {
    const { query } = useParams();
    const [products, setProducts] = useState(null);

    useEffect(() => {
        fetch(`product/searchProducts/${query}`)
            .then((response) => response.json())
            .then((json) => {
                setProducts(json);
            });
    }, [query]);

    return (
        <div className="product-container">
            {products == null ? (
                <div>Loading...</div>
            ) : (
                products.length > 0 ?
                    products?.map((product) => (
                        <ProductBox product={product}></ProductBox>
                    )) : <div>No results found.</div>
            )}
        </div>
    );
}