import { Link } from 'react-router-dom';
import { Row } from "react-bootstrap";
import "./ProductBox.css";

export const ProductBox = ({ product }) => {

    const onSale = product.sale !== null;
    const listPrice = "$" + product.price.toLocaleString(undefined, { maximumFractionDigits: 2, minimumFractionDigits: 2 });
    const salePrice = product.sale !== null ? "$" + (product.price * (1 - product.sale.percentOff * 0.01)).toLocaleString(undefined, { maximumFractionDigits: 2, minimumFractionDigits: 2 }) : null;

    return (
        <div>
            <Link to={`/detailed-view/${product.productId}`}>
                <div key={product.productId} className="product">
                    <div className="product-image">
                        <img src={product.imageUrl} alt={product.productName} />
                        {onSale && (
                            <div className="product-percent-off">{ product.sale.percentOff }% OFF</div>
                        )}
                    </div>
                    <h5 className="product-title">{product.productName}</h5>
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