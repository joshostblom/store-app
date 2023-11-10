import React from 'react';
import { Grid, Button, Container, Typography } from "@material-ui/core";
import { ShoppingCart } from "@material-ui/icons";
//import { commerce } from "../../lib/commerce";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";

const createMarkup = (text) => {
    return { __html: text };
}

//addProduct
export const DisplayDetailedProduct = (id) => {
    const [product, setProduct] = useState({});
    //const [quantity, setQuantity] = useState(1);
    //const { id } = useParams();

   // const fetchProduct = async (id) => {
        //try {
            // Replace this URL with your actual endpoint for fetching product details
            //const response = await fetch(`product/getProduct/${id}`);
            //const data = await response.json();

            // Update the state with the fetched product data
            //setProduct(data)

       // } catch (error) {
           // console.error("Error fetching product:", error);
        //}
    //};


    
    useEffect(() => {
        async function fetchData() {
            const response = await fetch(`detailedproduct/getDetailedProduct/${id}`);
            const data = await response.json();
            setProduct(data)

        }
        fetchData();  // Fetch product data when the component mounts
    }, [id]);
    

    /*
    const handleQuantity = (param) => {
        if (param === "decrease" && quantity > 1) {
            setQuantity(quantity - 1);
        }
        if (param === "increase" && quantity < 10) {
            setQuantity(quantity + 1);
        }
    };
    */
   /*
    return (
        <Container className="detailed-product">
            <Grid container spacing={4}>
                <Grid item xs={12} md={8} className="image-wrapper">
                    <img
                        src={product.imageUrl}
                        alt={product.productName}
                    />
                </Grid>
                <Grid item xs={12} md={4} className="text">
                    <Typography variant="h2">{product.productName}</Typography>
                    <Typography
                        variant="p"
                        //IDK IF NEEDED, how to get description
                        dangerouslySetInnerHTML={createMarkup(product.description)}
                    />
                    <Typography variant="h3">Price: {product.price}</Typography>
                    <Grid container spacing={4}>
                        <Button
                            size="small"
                            variant="contained"
                            className="increase-product-quantitiy"
                            onClick={() => {
                                //MIGHT NEED TO CHANGE LOOK AT CLASS
                                handleQuantity("increase");
                            }}
                        >
                            +
                        </Button>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography className="quantity" variant="h3">
                            Quantity: {quantity}
                        </Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Button
                            size="small"
                            color="secondary"
                            variant="contained"
                            onClick={() => {
                                handleQuantity("decrease");
                            }}
                        >
                            -
                        </Button>
                    </Grid>
                    <Grid item xs={12}>
                        <Button
                            size="large"
                            className="custom-button"
                            // onClick={() => {
                                // addProduct(product.id, quantity);
                            // }}
                        >
                            <ShoppingCart /> add to basket
                        </Button>
                    </Grid>
                </Grid>
            </Grid>
        </Container>
    );
    */
    return (
        <main>
            {product.length > 0 ? (
                product.map((product) => (
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
};

export default DisplayDetailedProduct;