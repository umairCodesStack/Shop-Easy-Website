import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import ProductCard from "../components/products/ProductCard";
import "./Home.css";

const Home = () => {
  const [trendingProducts, setTrendingProducts] = useState([]);
  const [dealProducts, setDealProducts] = useState([]);
  const [topVendors, setTopVendors] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadHomeData();
  }, []);

  const loadHomeData = async () => {
    setLoading(true);

    // Simulate API call - Replace with your actual backend API
    setTimeout(() => {
      // Trending Products
      setTrendingProducts([
        {
          id: 1,
          name: "Wireless Noise Cancelling Headphones",
          price: 79.99,
          originalPrice: 129.99,
          discount: 38,
          rating: 4.5,
          reviews: 1234,
          storeName: "TechVault Store",
          storeId: 1,
          image:
            "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500&h=500&fit=crop",
          badge: "Trending",
        },
        {
          id: 2,
          name: "Smart Fitness Watch Pro",
          price: 199.99,
          originalPrice: 299.99,
          discount: 33,
          rating: 4.8,
          reviews: 892,
          storeName: "Gadget Hub",
          storeId: 2,
          image:
            "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500&h=500&fit=crop",
          badge: "Best Seller",
        },
        {
          id: 3,
          name: "Premium Running Shoes",
          price: 89.99,
          originalPrice: 149.99,
          discount: 40,
          rating: 4.6,
          reviews: 2341,
          storeName: "Sports Arena",
          storeId: 3,
          image:
            "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500&h=500&fit=crop",
          badge: "Hot",
        },
        {
          id: 4,
          name: "Professional Coffee Maker",
          price: 49.99,
          rating: 4.3,
          reviews: 567,
          storeName: "Home Essentials",
          storeId: 4,
          image:
            "https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=500&h=500&fit=crop",
        },
        {
          id: 5,
          name: "Laptop Backpack Water Resistant",
          price: 39.99,
          originalPrice: 59.99,
          discount: 33,
          rating: 4.4,
          reviews: 1789,
          storeName: "Travel Gear Co",
          storeId: 5,
          image:
            "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500&h=500&fit=crop",
        },
        {
          id: 6,
          name: "Portable Bluetooth Speaker",
          price: 59.99,
          originalPrice: 89.99,
          discount: 33,
          rating: 4.7,
          reviews: 2034,
          storeName: "Audio World",
          storeId: 6,
          image:
            "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500&h=500&fit=crop",
          badge: "Popular",
        },
        {
          id: 7,
          name: "Premium Yoga Mat & Bag Set",
          price: 29.99,
          rating: 4.5,
          reviews: 1456,
          storeName: "Fitness Plus",
          storeId: 7,
          image:
            "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=500&h=500&fit=crop",
        },
        {
          id: 8,
          name: "LED Desk Lamp with USB Port",
          price: 34.99,
          originalPrice: 49.99,
          discount: 30,
          rating: 4.2,
          reviews: 678,
          storeName: "Office Supplies Hub",
          storeId: 8,
          image:
            "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500&h=500&fit=crop",
        },
      ]);

      // Today's Deals
      setDealProducts([
        {
          id: 11,
          name: "Wireless Mouse",
          price: 19.99,
          originalPrice: 39.99,
          discount: 50,
          rating: 4.4,
          reviews: 456,
          storeName: "Tech Accessories",
          storeId: 11,
          image:
            "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500&h=500&fit=crop",
          badge: "50% OFF",
        },
        {
          id: 12,
          name: "Phone Stand",
          price: 14.99,
          originalPrice: 29.99,
          discount: 50,
          rating: 4.6,
          reviews: 789,
          storeName: "Mobile Shop",
          storeId: 12,
          image:
            "https://images.unsplash.com/photo-1556656793-08538906a9f8?w=500&h=500&fit=crop",
          badge: "Deal",
        },
        {
          id: 13,
          name: "Water Bottle",
          price: 12.99,
          originalPrice: 24.99,
          discount: 48,
          rating: 4.7,
          reviews: 1234,
          storeName: "Lifestyle Store",
          storeId: 13,
          image:
            "https://images.unsplash.com/photo-1602143407151-7111542de6e8?w=500&h=500&fit=crop",
          badge: "Hot Deal",
        },
        {
          id: 14,
          name: "Notebook Set",
          price: 9.99,
          originalPrice: 19.99,
          discount: 50,
          rating: 4.5,
          reviews: 567,
          storeName: "Stationery Plus",
          storeId: 14,
          image:
            "https://images.unsplash.com/photo-1531346878377-a5be20888e57?w=500&h=500&fit=crop",
          badge: "50% OFF",
        },
      ]);

      // Top Vendors
      setTopVendors([
        {
          id: 1,
          name: "TechVault Store",
          description: "Premium electronics and gadgets",
          logo: "💻",
          rating: 4.8,
          totalProducts: 234,
          totalSales: 5420,
          badge: "Top Rated",
          bgColor: "#e0e7ff",
        },
        {
          id: 2,
          name: "Fashion Gallery",
          description: "Trendy fashion for everyone",
          logo: "👗",
          rating: 4.7,
          totalProducts: 567,
          totalSales: 8920,
          badge: "Popular",
          bgColor: "#fce7f3",
        },
        {
          id: 3,
          name: "Sports Arena",
          description: "Quality sports equipment",
          logo: "⚽",
          rating: 4.9,
          totalProducts: 189,
          totalSales: 3210,
          badge: "Top Seller",
          bgColor: "#d1fae5",
        },
        {
          id: 4,
          name: "Home Essentials",
          description: "Everything for your home",
          logo: "🏠",
          rating: 4.6,
          totalProducts: 432,
          totalSales: 6780,
          badge: "Featured",
          bgColor: "#fef3c7",
        },
        {
          id: 5,
          name: "Book Haven",
          description: "Books for every reader",
          logo: "📚",
          rating: 4.9,
          totalProducts: 890,
          totalSales: 12340,
          badge: "Verified",
          bgColor: "#ddd6fe",
        },
        {
          id: 6,
          name: "Beauty Corner",
          description: "Premium beauty products",
          logo: "💄",
          rating: 4.7,
          totalProducts: 345,
          totalSales: 4560,
          badge: "Trusted",
          bgColor: "#fbcfe8",
        },
      ]);

      // Categories
      setCategories([
        {
          id: 1,
          name: "Electronics",
          icon: "💻",
          count: 2345,
          color: "#667eea",
        },
        { id: 2, name: "Fashion", icon: "👕", count: 4567, color: "#f093fb" },
        {
          id: 3,
          name: "Home & Garden",
          icon: "🏡",
          count: 1890,
          color: "#4facfe",
        },
        { id: 4, name: "Sports", icon: "⚽", count: 987, color: "#43e97b" },
        { id: 5, name: "Books", icon: "📚", count: 3456, color: "#fa709a" },
        {
          id: 6,
          name: "Toys & Kids",
          icon: "🧸",
          count: 876,
          color: "#30cfd0",
        },
        { id: 7, name: "Beauty", icon: "💄", count: 1234, color: "#a8edea" },
        { id: 8, name: "Automotive", icon: "🚗", count: 654, color: "#fed6e3" },
      ]);

      setLoading(false);
    }, 1000);
  };

  if (loading) {
    return (
      <div className="loading-screen">
        <div className="loader"></div>
        <p>Loading amazing products...</p>
      </div>
    );
  }

  return (
    <div className="home-page">
      {/* Hero Section */}
      <section className="hero-section">
        <div className="hero-overlay"></div>
        <div className="container hero-container">
          <div className="hero-content">
            <span className="hero-badge">🎉 Welcome to MultiVendor</span>
            <h1 className="hero-title">
              Shop From <span className="highlight">1000+ Vendors</span>
              <br />
              On One Platform
            </h1>
            <p className="hero-subtitle">
              Discover millions of products from trusted sellers worldwide.
              Great deals, fast shipping, and secure checkout.
            </p>
            <div className="hero-buttons">
              <Link to="/products" className="btn btn-primary btn-large">
                <span>🛍️</span> Start Shopping
              </Link>
              <Link to="/stores" className="btn btn-outline-white btn-large">
                <span>🏪</span> Browse Stores
              </Link>
            </div>
            <div className="hero-stats">
              <div className="stat-item">
                <span className="stat-number">1000+</span>
                <span className="stat-label">Vendors</span>
              </div>
              <div className="stat-item">
                <span className="stat-number">50K+</span>
                <span className="stat-label">Products</span>
              </div>
              <div className="stat-item">
                <span className="stat-number">100K+</span>
                <span className="stat-label">Happy Customers</span>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Categories Section */}
      <section className="categories-section">
        <div className="container">
          <div className="section-header-simple">
            <h2 className="section-title">Shop by Category</h2>
            <p className="section-subtitle">
              Browse through your favorite categories
            </p>
          </div>
          <div className="categories-scroll">
            <div className="categories-wrapper">
              {categories.map((category) => (
                <Link
                  key={category.id}
                  to={`/products?category=${category.name.toLowerCase()}`}
                  className="category-item"
                  style={{ "--category-color": category.color }}
                >
                  <div className="category-icon-box">
                    <span className="category-icon">{category.icon}</span>
                  </div>
                  <h3 className="category-name">{category.name}</h3>
                  <p className="category-count">
                    {category.count.toLocaleString()} items
                  </p>
                </Link>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* Trending Products Section */}
      <section className="products-section trending-section">
        <div className="container">
          <div className="section-header">
            <div className="section-header-left">
              <h2 className="section-title">
                <span className="title-icon">🔥</span> Trending Products
              </h2>
              <p className="section-subtitle">Most popular items this week</p>
            </div>
            <Link to="/products" className="view-all-btn">
              View All Products <span>→</span>
            </Link>
          </div>
          <div className="products-grid">
            {trendingProducts.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </div>
      </section>

      {/* Top Vendors Section */}
      <section className="vendors-section">
        <div className="container">
          <div className="section-header">
            <div className="section-header-left">
              <h2 className="section-title">
                <span className="title-icon">🏪</span> Featured Stores
              </h2>
              <p className="section-subtitle">Shop from top-rated vendors</p>
            </div>
            <Link to="/stores" className="view-all-btn">
              View All Stores <span>→</span>
            </Link>
          </div>
          <div className="vendors-grid">
            {topVendors.map((vendor) => (
              <Link
                key={vendor.id}
                to={`/stores/${vendor.id}`}
                className="vendor-card"
                style={{ "--vendor-bg": vendor.bgColor }}
              >
                <div className="vendor-badge">{vendor.badge}</div>
                <div className="vendor-logo">{vendor.logo}</div>
                <div className="vendor-info">
                  <h3 className="vendor-name">{vendor.name}</h3>
                  <p className="vendor-description">{vendor.description}</p>
                  <div className="vendor-stats">
                    <div className="vendor-stat">
                      <span className="stat-icon">⭐</span>
                      <span className="stat-value">{vendor.rating}</span>
                    </div>
                    <div className="vendor-stat">
                      <span className="stat-icon">📦</span>
                      <span className="stat-value">{vendor.totalProducts}</span>
                    </div>
                    <div className="vendor-stat">
                      <span className="stat-icon">🛒</span>
                      <span className="stat-value">
                        {vendor.totalSales.toLocaleString()}
                      </span>
                    </div>
                  </div>
                </div>
                <button className="vendor-visit-btn">Visit Store →</button>
              </Link>
            ))}
          </div>
        </div>
      </section>

      {/* Today's Deals Section */}
      <section className="products-section deals-section">
        <div className="container">
          <div className="section-header">
            <div className="section-header-left">
              <h2 className="section-title">
                <span className="title-icon">⚡</span> Today's Hot Deals
              </h2>
              <p className="section-subtitle">
                Limited time offers - Don't miss out!
              </p>
            </div>
            <div className="deals-timer">
              <span className="timer-label">Ends in:</span>
              <span className="timer-value">23:45:12</span>
            </div>
          </div>
          <div className="products-grid products-grid-4">
            {dealProducts.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </div>
      </section>

      {/* Why Choose Us Section */}
      <section className="features-section">
        <div className="container">
          <div className="section-header-simple">
            <h2 className="section-title">Why Shop With Us?</h2>
            <p className="section-subtitle">
              We provide the best shopping experience
            </p>
          </div>
          <div className="features-grid">
            <div className="feature-card">
              <div className="feature-icon">🚚</div>
              <h3 className="feature-title">Free Shipping</h3>
              <p className="feature-description">
                Free delivery on orders over $50. Fast and reliable shipping
                worldwide.
              </p>
            </div>
            <div className="feature-card">
              <div className="feature-icon">🔒</div>
              <h3 className="feature-title">Secure Payment</h3>
              <p className="feature-description">
                100% secure payment processing. Your data is protected with SSL
                encryption.
              </p>
            </div>
            <div className="feature-card">
              <div className="feature-icon">↩️</div>
              <h3 className="feature-title">Easy Returns</h3>
              <p className="feature-description">
                30-day hassle-free return policy. Not satisfied? Get your money
                back.
              </p>
            </div>
            <div className="feature-card">
              <div className="feature-icon">💬</div>
              <h3 className="feature-title">24/7 Support</h3>
              <p className="feature-description">
                Round-the-clock customer support. We're here to help anytime you
                need.
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* Newsletter Section */}
      <section className="newsletter-section">
        <div className="container">
          <div className="newsletter-content">
            <div className="newsletter-text">
              <h2 className="newsletter-title">
                Get Exclusive Deals in Your Inbox
              </h2>
              <p className="newsletter-subtitle">
                Subscribe to our newsletter and get 10% off your first order!
              </p>
            </div>
            <form className="newsletter-form">
              <input
                type="email"
                placeholder="Enter your email address"
                className="newsletter-input"
              />
              <button type="submit" className="btn btn-primary">
                Subscribe
              </button>
            </form>
          </div>
        </div>
      </section>
    </div>
  );
};

export default Home;
