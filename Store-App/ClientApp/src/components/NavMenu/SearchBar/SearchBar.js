import { useState } from 'react';
import "./SearchBar.css"

export const SearchBar = () => {

    const [query, setQuery] = useState("");

    return (
        <div className="search-bar-container">
            <input type="text" placeholder="Search for categories or items" onChange={e => setQuery(e.target.value)} />
        </div>
    );
}