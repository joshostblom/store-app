import React, { useState } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { GiShoppingCart } from "react-icons/gi";
import './NavMenu.css';
import { SearchBar } from './SearchBar/SearchBar.js'

export const NavMenu = ({ isLoggedIn, setLoggedIn }) => {

    //Set a collapsed state
    const [collapsed, setCollapsed] = useState(false);

    //Call to the controller to logout
    function logout() {
        fetch('person/logout')
            .then((response) => response.json())
            .then((loggedOut) => {
                if (loggedOut) {
                    setLoggedIn(false);
                }
            });
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                <NavbarBrand tag={Link} to="/">The Online Store</NavbarBrand>
                <NavbarToggler onClick={() => setCollapsed(!collapsed)} className="mr-2" />
                <SearchBar></SearchBar>
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-dark navitem" to="/">Home</NavLink>
                        </NavItem>
                        <NavItem>
                            {isLoggedIn ? (
                                <NavLink tag={Link} className="text-dark navitem" to="/login" onClick={() => logout()}>Logout</NavLink>
                            ) : (
                                    <NavLink tag={Link} className="text-dark navitem" to="/login">Login</NavLink>
                            )}
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark navitem cart-icon" to="/cart"><GiShoppingCart/></NavLink>
                        </NavItem>
                    </ul>
                </Collapse>
            </Navbar>
        </header>
    );
}
