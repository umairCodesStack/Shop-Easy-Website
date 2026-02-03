import React, { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import ProductList from "../components/products/ProductList";
import ProductFilter from "../components/products/ProductFilter";
import "./Products.css";

const Products = () => {
  const [searchParams] = useSearchParams();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [filters, setFilters] = useState({});

  useEffect(() => {
    loadProducts();
  }, [searchParams, filters]);

  const loadProducts = async () => {
    setLoading(true);

    // Simulate API call - replace with actual API
    setTimeout(() => {
      const mockProducts = [
        {
          id: 1,
          name: "Wireless Headphones",
          price: 79.99,
          originalPrice: 99.99,
          discount: 20,
          rating: 4.5,
          reviews: 120,
          storeName: "Tech Store",
          image:
            "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500",
        },
        {
          id: 2,
          name: "Smart Watch",
          price: 199.99,
          rating: 4.8,
          reviews: 89,
          storeName: "Gadget Hub",
          image:
            "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500",
        },
        {
          id: 3,
          name: "Running Shoes",
          price: 89.99,
          originalPrice: 120.0,
          discount: 25,
          rating: 4.6,
          reviews: 234,
          storeName: "Sports World",
          image:
            "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500",
        },
        {
          id: 4,
          name: "Coffee Maker",
          price: 49.99,
          rating: 4.3,
          reviews: 56,
          storeName: "Home Essentials",
          image:
            "https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=500",
        },
        {
          id: 5,
          name: "Laptop Backpack",
          price: 39.99,
          rating: 4.4,
          reviews: 178,
          storeName: "Travel Gear",
          image:
            "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500",
        },
        {
          id: 6,
          name: "Bluetooth Speaker",
          price: 59.99,
          originalPrice: 79.99,
          discount: 25,
          rating: 4.7,
          reviews: 203,
          storeName: "Audio Pro",
          image:
            "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500",
        },
        {
          id: 7,
          name: "Yoga Mat",
          price: 29.99,
          rating: 4.5,
          reviews: 145,
          storeName: "Fitness Plus",
          image:
            "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=500",
        },
        {
          id: 8,
          name: "Desk Lamp",
          price: 34.99,
          rating: 4.2,
          reviews: 67,
          storeName: "Home Decor",
          image:
            "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500",
        },
      ];

      setProducts(mockProducts);
      setLoading(false);
    }, 1000);
  };

  const handleFilterChange = (newFilters) => {
    setFilters(newFilters);
  };

  const searchQuery = searchParams.get("search");

  return (
    <div className="products-page">
      <div className="container">
        <div className="products-header">
          <h1 className="page-title">
            {searchQuery
              ? `Search results for "${searchQuery}"`
              : "All Products"}
          </h1>
          <p className="products-count">Showing {products.length} products</p>
        </div>

        <div className="products-layout">
          <aside className="products-sidebar">
            <ProductFilter onFilterChange={handleFilterChange} />
          </aside>

          <main className="products-main">
            <ProductList products={products} loading={loading} />
          </main>
        </div>
      </div>
    </div>
  );
};

export default Products;
