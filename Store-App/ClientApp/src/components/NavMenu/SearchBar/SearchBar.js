import { useState } from 'react';
import { AiOutlineSearch } from 'react-icons/ai'
import { MdClear } from 'react-icons/md'
import { useNavigate, createSearchParams } from 'react-router-dom'
import "./SearchBar.css"

export const SearchBar = () => {

    //Create query and suggestions state
    const [query, setQuery] = useState("");
    const [productSuggestions, setProductSuggestions] = useState([]);
    const [categorySuggestions, setCategorySuggestions] = useState([]);

    //Create nav state for navigating to the search page
    const navigate = useNavigate();

    //Get the suggestions from the controller with the given search value
    const fetchData = (value) => {
        if (value === "") {
            clearSuggestions();
        } else {
            fetch(`product/searchProducts/${value}`)
                .then((response) => response.json())
                .then((json) => {
                    setProductSuggestions(json);
                });
            fetch(`category/searchCategories/${value}`)
                .then((response) => response.json())
                .then((json) => {
                    setCategorySuggestions(json);
                });
        }
    }

    const clearSuggestions = () => {
        setProductSuggestions([]);
        setCategorySuggestions([]);
    }

    const handleChange = (value) => {
        setQuery(value)
        fetchData(value)
    }

    //Navigate to search page with input
    const search = (product, category) => {
        clearSuggestions();
        navigate({
            pathname: "search",
            search: createSearchParams({
                product: product,
                category: category,
            }).toString()
        });
    }

    return (
        <div className="search">
            <div className="search-bar-container">
                <AiOutlineSearch className="search-icon" onClick={() => search(query, null) } />
                <input type="text"
                    value={query}
                    placeholder="Search for categories or items"
                    onKeyDown={(key) => { if (key.key === 'Enter') { search(query, null); } }}
                    onChange={(e) => handleChange(e.target.value)} />
                <MdClear className="clear-icon" onClick={() => { setQuery(""); clearSuggestions(); }} />
            </div>
            <div className="suggestions-container">
                {categorySuggestions.length > 0 && (
                    <div className="suggestions-header">Categories</div>
                )}
                {categorySuggestions?.map((value) => (
                    <div className="suggestion-item" onClick={() => {
                        setQuery(value.name);
                        search(null, value.categoryId);
                    }
                    }> <div className="suggestion-text"> {value.name} </div> </div>
                ))}
                {productSuggestions.length > 0 && (
                    <div className="suggestions-header">Products</div>
                )}
                {productSuggestions?.map((value) => (
                    <div className="suggestion-item" onClick={() => {
                        setQuery(value.productName);
                        search(value.productName, null);
                    }
                    }> <div className="suggestion-text"> {value.productName} </div> </div>
                ))}
            </div>
        </div>
    );
}