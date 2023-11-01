import { useState } from 'react';

const SearchBar = () => {

    const [query, setQuery] = useState("");

    return (
        <div>
            <input type="text" onChange={e => setQuery(e.target.value)} />
        </div>
    );
}