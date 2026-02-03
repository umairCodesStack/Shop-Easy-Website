import React from "react";
import { Link } from "react-router-dom";
//import { useCart } from "../../context/CartContext";
import "./ProductCard.css";

const ProductCard = ({ product }) => {
  //const { addToCart } = useCart();

  const handleAddToCart = (e) => {
    e.preventDefault();
    //addToCart(product);
  };

  return (
    <Link to={`/products/${product.id}`} className="product-card card">
      <div className="product-image-container">
        <img
          src={product.image || "https://via.placeholder.com/300"}
          alt={product.name}
          className="product-image"
        />
        {product.discount && (
          <span className="product-badge">-{product.discount}%</span>
        )}
      </div>

      <div className="product-info">
        <div className="product-store">
          <span className="store-icon">🏪</span>
          <span className="store-name">{product.storeName || "Store"}</span>
        </div>

        <h3 className="product-name">{product.name}</h3>

        <div className="product-rating">
          <span className="rating-stars">⭐ {product.rating || 4.5}</span>
          <span className="rating-count">({product.reviews || 0})</span>
        </div>

        <div className="product-footer">
          <div className="product-price">
            {product.originalPrice && (
              <span className="price-original">${product.originalPrice}</span>
            )}
            <span className="price-current">${product.price}</span>
          </div>

          <button onClick={handleAddToCart} className="btn-add-cart">
            🛒 Add
          </button>
        </div>
      </div>
    </Link>
  );
};

export default ProductCard;
