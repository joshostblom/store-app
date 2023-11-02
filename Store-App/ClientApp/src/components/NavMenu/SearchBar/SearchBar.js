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
        fetch("https://jsonplaceholder.typicode.com/users")
            .then((response) => response.json())
            .then((json) => {
                const results = json.filter((user) => {
                    return user && user.name && user.name.toLowerCase().includes(value.toLowerCase())
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
                        setQuery(value.name);
                        setSuggestions([]);
                    }
                    }> <div className="suggestion-text"> {value.name} </div> </div>
                ))}
            </div>
        </div>
    );
}