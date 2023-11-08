import { useState } from "react";
import { Row, Container } from "react-bootstrap";
import "./LoginPage.css"

export const LoginPage = () => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    return (
        <div className="container">
            <Container>
            <h1> Login </h1>
            <Row>
                <input className="text-field" type="text" placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
            </Row>
            <Row>
                <input className="text-field" type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
            </Row>
            <Row>
                <button className="btn-primary">Login</button>
            </Row>
            </Container>
        </div>
    );
}
