import { useEffect, useState } from "react"
import { useParams } from "react-router-dom";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

export const SearchPage = () => {

    //Get the query from the url and create a products state
    const { query } = useParams();
    const [products, setProducts] = useState(null);

    //Get the products from the controller and set the products state
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
                    //Dispaly each product in a product box
                    products?.map((product) => (
                        <ProductBox product={product}></ProductBox>
                    )) : <div>No results found.</div>
            )}
        </div>
    );
}