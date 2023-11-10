import { useState } from "react";
import { Row } from "react-bootstrap";
import "./LoginPage.css"

export const LoginPage = () => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [success, setSuccess] = useState(false);

    async function login(email, password) {

        let loginRequest = {
            Email: email,
            Password: password
        };

        const success = await fetch('person/login', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify(loginRequest)
        })
            .then((response) => response.json())
            .then((json) => {
                setSuccess(json);
                console.log(json);
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
            <Row className="justify-content-md-center">
                <button className="btn-primary" onClick={() => login(username, password)}>Login</button>
            </Row>
        </div>
    );
}
