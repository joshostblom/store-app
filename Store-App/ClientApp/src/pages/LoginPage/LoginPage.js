import { useState } from "react";
import { Row } from "react-bootstrap";
import { useNavigate } from 'react-router-dom'
import "./LoginPage.css";

export const LoginPage = ({ setLoggedIn }) => {

    //Create states for username, password, and error
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(false);

    //Create a nav state for navigating back to the previous page
    const navigate = useNavigate();

    function login(email, password) {

        //Create a loginrequest object
        let loginRequest = {
            Email: email,
            Password: password
        };

        //Send a login request to the controller
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
                    //If login failed, then set the error state to true
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
            {/*If there's an error, then display invalid credentials*/}
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
