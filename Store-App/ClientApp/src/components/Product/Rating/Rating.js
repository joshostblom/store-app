import { FaStar } from "react-icons/fa";
import { FaRegStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa";
import "./Rating.css"


export const Rating = ({ rating }) => {

    //Find star amounts based on rating
    const fullStars = parseInt(rating);
    const emptyStars = parseInt(5 - rating);
    const halfStars = 5 - fullStars - emptyStars > 0 ? 1 : 0;

    //Define functions to get star UI given amounts
    const getFullStars = (amount) => {
        const content = [];
        for (let i = 0; i < amount; i++) {
            content.push(<FaStar />);
        }
        return content;
    }
    const getHalfStars = (amount) => {
        const content = [];
        for (let i = 0; i < amount; i++) {
            content.push(<FaStarHalfAlt />);
        }
        return content;
    }
    const getEmptyStars = (amount) => {
        const content = [];
        for (let i = 0; i < amount; i++) {
            content.push(<FaRegStar />);
        }
        return content;
    }

    //Display stars
    return (
        <div className="stars">
            {getFullStars(fullStars)}{getHalfStars(halfStars)}{getEmptyStars(emptyStars)}
        </div>
    )
}