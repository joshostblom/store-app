import { useState } from "react";
import { Row } from "react-bootstrap";
import { useNavigate } from 'react-router-dom'
import "./LoginPage.css";

export const LoginPage = ({ setLoggedIn }) => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(false);
    const navigate = useNavigate();

    async function login(email, password) {

        let loginRequest = {
            Email: email,
            Password: password
        };

        fetch('person/login', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify(loginRequest)
        })
            .then((response) => response.json())
            .then((loggedIn) => {
                if (loggedIn === true) {
                    setLoggedIn(true);
                    navigate(-1);
                } else {
                    setError(true);
                }
            });
    }

    return (
        <div>
            <Row className="justify-content-md-center">
                <h1 className="login-header"> Login </h1>
            </Row>
            <Row className="justify-content-md-center">
                <input className="text-field" type="text" placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
            </Row>
            <Row className="justify-content-md-center">
                <input className="text-field" type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
            </Row>
            {error &&
                <Row className="justify-content-md-center">
                    <div className="error-text">Invalid credentials.</div>
                </Row>
            }
            <Row className="justify-content-md-center">
                <button className="btn-primary" onClick={() => login(username, password)}>Login</button>
            </Row>
        </div>
    );
}
