import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { ProductBox } from './ProductBox/Product.js'

class Product extends Component {
    render() {
        return (
            <header>
                <ProductBox></ProductBox>
            </header>
        );
    }
}

export default Product