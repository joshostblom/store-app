import { Link } from 'react-router-dom';
import "./ProductBox.css";

export const ProductBox = ({ product }) => {

    return (
        <div>
            <Link to={`detailed-view/${product.productId}`}>
                <div key={product.productId} className="product">
                    <div className="product-image">
                        <img src={product.imageUrl} alt={product.productName} />
                    </div>
                    <h5 className="product-title">{product.productName}</h5>
                </div>
            </Link>
        </div>
    );
}