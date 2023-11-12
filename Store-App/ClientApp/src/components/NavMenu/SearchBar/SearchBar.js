import { useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai'
import "./SearchBar.css"

export const SearchBar = () => {

    const [query, setQuery] = useState("");
    const [suggestions, setSuggestions] = useState([]);

    const fetchData = (value) => {
        if (value === "") {
            setSuggestions([]);
            return;
        }
        fetch('product/getProducts')
            .then((response) => response.json())
            .then((json) => {
                const results = json.filter((product) => {
                    return product && product.productName && product.productName.toLowerCase().includes(value.toLowerCase())
                });
                setSuggestions(results);
            });
    }

    const handleChange = (value) => {
        setQuery(value)
        fetchData(value)
    }

    return (
        <div className="search">
            <div className="search-bar-container">
                <AiOutlineSearch className="search-icon"/>
                <input type="text" value={query} placeholder="Search for categories or items" onChange={(e) => handleChange(e.target.value)} />
            </div>
            <div className="suggestions-container">
                { suggestions?.map((value) => (
                    <div className="suggestion-item" onClick={() => {
                        setQuery(value.productName);
                        setSuggestions([]);
                    }
                    }> <div className="suggestion-text"> {value.productName} </div> </div>
                ))}
            </div>
        </div>
    );
}