import { Link } from 'react-router-dom';
import { Row } from "react-bootstrap";
import { Rating } from "../Rating/Rating.js";
import "./CartProduct.css";
import { useEffect, useState } from 'react';

export const CartProduct = ({ product, onRemove }) => {

    //Create a state for sale
    const [sale, setSale] = useState({});

    //Set properties for sale info
    const onSale = checkValidSale(sale);
    const listPrice = "$" + product?.price?.toLocaleString(undefined, { maximumFractionDigits: 2, minimumFractionDigits: 2 });
    const salePrice = sale !== null ? "$" + (product?.price * (1 - sale.percentOff * 0.01)).toLocaleString(undefined, { maximumFractionDigits: 2, minimumFractionDigits: 2 }) : null;

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


    const handleRemoveFromCart = async () => {
        try {
            const responseCart = await fetch('/cart/GetCartForCurrentUser');

            if (!responseCart.ok) {
                console.error('Error getting cart for current user');
                return;
            }

            const cartData = await responseCart.json();
            const cartId = cartData.cartId;

            const responseRemoveFromCart = await fetch(`/producttocart/RemoveProductFromCart/${cartId}/products/${product?.productId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            
            if (responseRemoveFromCart.ok) {
                console.log('Product removed from cart');
                onRemove();
            } else {
                console.error('Failed to remove product from cart');
            }
        } catch (error) {
            console.error('Error removing product from cart:', error);
        }
    };


    return (
        <div className="product">
            {/*Put the entire box into a link to the detailed product page*/}
            <Link to={`/detailed-view/${product?.productId}`}>
                <div key={product?.productId} className="product">
                    <div className="product-image-container">
                        <img src={product?.imageUrl} alt={product?.productName} />
                        {onSale && (
                            <div className="product-percent-off">{sale.percentOff}% OFF</div>
                        )}
                    </div>
                    <h5 className="product-title">{product?.productName}</h5>
                    <Row className="justify-content-md-center">
                        <Rating rating={product?.rating} />
                    </Row>
                    <Row className="justify-content-md-center">
                        {onSale && (
                            <h4 className="product-price">{salePrice}</h4>
                        )}
                        <h4 className={onSale ? "product-price-old" : "product-price"}> {listPrice} </h4>
                    </Row>
                    
                </div>
            </Link>
            <Row className="justify-content-md-center">
                <Link to={`/cart`}>
                    <button className="btn btn-danger" onClick={handleRemoveFromCart}>Remove</button>
                </Link>
            </Row>
        </div>
    );
}