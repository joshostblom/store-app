import { Link } from 'react-router-dom';
import { Row } from "react-bootstrap";
import "./ProductBox.css";
import { useEffect, useState } from 'react';

export const ProductBox = ({ product }) => {

    //Create a state for sale
    const [sale, setSale] = useState({});

    //Set properties for sale info
    const onSale = sale !== null;
    const listPrice = "$" + product?.price?.toLocaleString(undefined, { maximumFractionDigits: 2, minimumFractionDigits: 2 });
    const salePrice = sale !== null ? "$" + (product?.price * (1 - sale.percentOff * 0.01)).toLocaleString(undefined, { maximumFractionDigits: 2, minimumFractionDigits: 2 }) : null;

    //Get the sale from the controller
    useEffect(() => {
        if (product?.saleId != null) {
            fetch(`sale/getSale/${product.saleId}`)
                .then((response) => response.json())
                .then((json) => {
                    setSale(json);
                });
        }
    }, [product]);

    return (
        <div className="product">
        {/*Put the entire box into a link to the detailed product page*/}
            <Link to={`/detailed-view/${product?.productId}`}>
                <div key={product?.productId} className="product">
                    <div className="product-image-container">
                        <img src={product?.imageUrl} alt={product?.productName} />
                        {onSale && (
                            <div className="product-percent-off">{ sale.percentOff }% OFF</div>
                        )}
                    </div>
                    <h5 className="product-title">{product?.productName}</h5>
                    <Row className="justify-content-md-center">
                        {onSale && (
                            <h4 className="product-price">{ salePrice }</h4>
                        )}
                        <h4 className={ onSale ? "product-price-old" : "product-price"}> {listPrice} </h4>
                    </Row>
                </div>
            </Link>
        </div>
    );
}