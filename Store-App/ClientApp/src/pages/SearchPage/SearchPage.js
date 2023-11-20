import { useEffect, useState } from "react"
import { useSearchParams } from "react-router-dom";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

export const SearchPage = () => {

    //Get the query from the url and create a products state
    let { searchParams, setSearchParams } = useSearchParams();
    const [products, setProducts] = useState(null);

    const productQuery = searchParams.get('product');
    //const categoryQuery = searchParams.get("product");

    //Get the products from the controller and set the products state
    useEffect(() => {
        fetch(`product/searchProducts/${productQuery}`)
            .then((response) => response.json())
            .then((json) => {
                setProducts(json);
            });
    }, [productQuery]);

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