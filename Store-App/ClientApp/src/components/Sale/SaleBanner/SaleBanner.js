import "./SaleBanner.css"

export const SaleBanner = ({ sale }) => {

    const startDate = new Date(sale.startDate);
    const endDate = new Date(sale.endDate);

    return (
        <div className="saleText">
            Product {sale.percentOff}% off from {startDate.toDateString()} to {endDate.toDateString()}!
        </div>
    );
}