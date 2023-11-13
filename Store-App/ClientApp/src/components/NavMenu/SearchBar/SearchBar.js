import { useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai'
import { MdClear } from 'react-icons/md'
import { useNavigate } from 'react-router-dom'
import "./SearchBar.css"

export const SearchBar = () => {

    const [query, setQuery] = useState("");
    const [suggestions, setSuggestions] = useState([]);
    const navigate = useNavigate();

    const fetchData = (value) => {
        if (value === "") {
            setSuggestions([]);
        } else {
            fetch(`product/searchProducts/${value}`)
                .then((response) => response.json())
                .then((json) => {
                    setSuggestions(json);
                });
        }
    }

    const handleChange = (value) => {
        setQuery(value)
        fetchData(value)
    }

    const search = (input) => {
        setSuggestions([]);
        navigate(`search/${input}`)
    }

    return (
        <div className="search">
            <div className="search-bar-container">
                <AiOutlineSearch className="search-icon" onClick={() => search(query) } />
                <input type="text"
                    value={query}
                    placeholder="Search for categories or items"
                    onKeyDown={(key) => { if (key.key === 'Enter') { search(query); } }}
                    onChange={(e) => handleChange(e.target.value)} />
                <MdClear className="clear-icon" onClick={() => { setQuery(""); setSuggestions([]); } } />
            </div>
            <div className="suggestions-container">
                {suggestions?.map((value) => (
                    <div className="suggestion-item" onClick={() => {
                        setQuery(value.productName);
                        search(value.productName);
                    }
                    }> <div className="suggestion-text"> {value.productName} </div> </div>
                ))}
            </div>
        </div>
    );
}