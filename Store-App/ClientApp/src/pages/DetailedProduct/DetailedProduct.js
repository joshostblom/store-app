import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import "./DetailedProduct.css";
import { ProductBox } from "../../components/Product/ProductBox/ProductBox.js";
import { SaleBanner } from "../../components/Sale/SaleBanner/SaleBanner.js";
import { Row } from "react-bootstrap";

export const DisplayDetailedProduct = () => {
    const { productId } = useParams();
    const [productById, setProductById] = useState({});

    //Create a state for sale
    const [sale, setSale] = useState({});

    useEffect(() => {
        fetch(`product/getProduct/${productId}`)
            .then((response) => response.json())
            .then((json) => {
                setProductById(json);
            });
    }, [productId]);

    //Get the sale from the controller
    useEffect(() => {
        if (productById?.saleId != null) {
            fetch(`sale/getSale/${productById.saleId}`)
                .then((response) => response.json())
                .then((json) => {
                    setSale(json);
                });
        }
    }, [productById]);

    const onSale = checkValidSale(sale);
    const name = "Name: " + productById?.productName;
    const description = "Description: " + productById?.descript;
    const rating = "Rating: " + productById?.rating + "/5 Stars";
    const manufacturer = "Manufacturer: " + productById?.manufacturerInformation;
    const sku = "SKU: " + productById?.sku;
    const dimensions = "Dimensions: \"" + productById?.prodHeight + "\" X \"" + productById?.prodWidth + "\" X \"" + productById?.prodLength + "\" X \"" + productById?.prodWeight + "\"";

    //Check if today's date is within the sale date
    function checkValidSale(sale) {
        if (sale != null) {
            const today = new Date();
            const startDate = new Date(sale.startDate);
            const endDate = new Date(sale.endDate);

            return startDate < today && today < endDate;
        } else {
            return false;
        }
    }

    return (
        <div className="outer-container" style={{ paddingTop: '200px' }}>
            <table style={{ width: '100%', marginTop: '10px', borderCollapse: 'collapse' }}>
                <tbody>
                    <Row className="justify-content-md-center">
                        <div style={{ display: 'flex', justifyContent: 'center' }}>
                            <ProductBox product={productById}></ProductBox>
                        </div>
                    </Row>
                    {onSale && (
                        <Row className="justify-content-md-center">
                            <SaleBanner sale={sale}></SaleBanner>
                        </Row>
                    )}
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{name}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{description}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{rating}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{manufacturer}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{sku}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <h6 className="product-text">{dimensions}</h6>
                    </Row>
                    <Row className="justify-content-md-center">
                        <div style={{ display: 'flex', justifyContent: 'right' }}>
                            <button className="btn-primary">Add to Cart</button>
                        </div>
                    </Row>
                </tbody>
            </table>
        </div>
    );
};
export default DisplayDetailedProduct;