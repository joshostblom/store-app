import { useState } from 'react';

export const APITest = () => {
    const [test1, setTest1] = useState("");
    const [test2, setTest2] = useState("");
    const [test3, setTest3] = useState([]);

    return (
        <div>
            <h1>TestController GetTest</h1>
            <button onClick={() => {
                console.log("Running TestController GetTest");
                fetch('test/getTest')
                    .then((response) => response.json())
                    .then((json) => {
                        console.log(json);
                        setTest1(json);
                    })
                    .catch((reason) => {
                        setTest1({productName: "Request failed! Press f12 to check error."});
                    });
            }}>Test TestController/GetTest</button>
            <div>{test1.productName}</div>

            <h1>ProductController GetTest</h1>
            <button onClick={() => {
                console.log("Running ProductController GetTest");
                fetch('product/getTest')
                    .then((response) => response.json())
                    .then((json) => {
                        console.log(json);
                        setTest2(json);
                    })
                    .catch((reason) => {
                        setTest2({ productName: "Request failed! Press f12 to check error." });
                    });
            }}>Test ProductController/GetTest</button>
            <div>{test2.productName}</div>

            <h1>ProductController GetProducts</h1>
            <button onClick={() => {
                console.log("Running ProductController GetProducts");
                fetch('product/getProducts')
                    .then((response) => response.json())
                    .then((json) => {
                        console.log(json);
                        setTest3(json);
                    })
                    .catch((reason) => {
                        setTest3([{key: 0, productName: "Request failed! Press f12 to check error." }]);
                    });
            }}>Test ProductController/GetProducts</button>
            <div>
                {test3?.map((value) => (
                    <div> {value.productName} </div>
                ))}
            </div>
        </div>
    );
}