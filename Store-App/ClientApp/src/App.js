import { Route, Routes } from 'react-router-dom';
import './custom.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from 'react';
import { LoginPage } from './pages/LoginPage/LoginPage';
import { APITest } from './components/APITest/APITest';
import { DisplayDetailedProduct } from "./pages/DetailedProduct/DetailedProduct.js";
import { SearchPage } from './pages/SearchPage/SearchPage';
import { NavMenu } from './components/NavMenu/NavMenu';
import { Container } from 'reactstrap';
import { HomePage } from './pages/HomePage/HomePage';
import { PaymentPage } from './pages/PaymentPage/PaymentPage'
import { OrderConfirmationPage } from './pages/OrderConfirmationPage/OrderConfirmationPage'

export default function App() {

    const [isLoggedIn, setLoggedIn] = useState(false);

    return (
        <div>
            <NavMenu isLoggedIn={isLoggedIn} setLoggedIn={setLoggedIn} />
            <Container>
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/detailed-view/:productId" element={<DisplayDetailedProduct />} />
                    <Route path="/search" element={<SearchPage />} />
                    <Route path="/apitest" element={<APITest />} />
                    <Route path="/login" element={<LoginPage setLoggedIn={setLoggedIn} />} />
                    <Route path="/payment" element={<PaymentPage setLoggedIn={setLoggedIn} />} />
                    <Route path="/order-confirmation" element={<OrderConfirmationPage setLoggedIn={setLoggedIn} />} />
                </Routes>
            </Container>
        </div>
    );
}
