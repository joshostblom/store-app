import { useEffect, useState } from "react"
import { useSearchParams } from "react-router-dom";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";

export const SearchPage = () => {

    //Get the query from the url for product and category
    const [searchParams] = useSearchParams();
    const productQuery = searchParams.get('product');
    const categoryQuery = searchParams.get("category");

    //Set the state for the title and products
    const [searchTitle, setSearchTitle] = useState("");
    const [products, setProducts] = useState(null);
    const [noProducts, setNoProudcts] = useState(false);


    //Get the products from the controller and set the products state
    useEffect(() => {

        //If it was a category search
        if (categoryQuery !== "null" && categoryQuery != null) {

            fetch(`category/getCategory/${categoryQuery}`)
                .then((response) => response.json())
                .then((category) => {
                    setSearchTitle(category.name);
                }).catch((error) => {
                    setNoProudcts(true);
                });;

            fetch(`productToCategory/getProductsInCategory/${categoryQuery}`)
                .then((response) => response.json())
                .then((newProducts) => {
                    setProducts(newProducts);
                }).catch((error) => {
                    setNoProudcts(true);
                });
        }

        //If it was a product search
        if (productQuery !== "null" && productQuery != null) {

            setSearchTitle(`Search results for "${productQuery}"`)

            fetch(`product/searchProducts/${productQuery}`)
                .then((response) => response.json())
                .then((newProducts) => {
                    setProducts(newProducts);
                });
        }
    }, [productQuery, categoryQuery]);

    return (
        <div>
            <h2 classname="search-title">{searchTitle ?? "Search failed"}</h2>
            <div className="product-container">
                {products == null && !noProducts ? (
                    <div>Loading...</div>
                ) : (
                    noProducts ?
                        <div>No results found.</div> :
                        //Dispaly each product in a product box
                        products?.map((product) => (
                            <ProductBox product={product.product ?? product}></ProductBox>
                        ))
                )}
            </div>
        </div>
    );
}