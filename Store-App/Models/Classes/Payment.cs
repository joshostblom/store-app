using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Payment : IPayment
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Payment(string cardName, string cardNumber, string cvv, DateTime expirationDate)
        {
            this.CardName = cardName;
            this.CardNumber = cardNumber;
            this.Cvv = cvv;
            this.ExpirationDate = expirationDate;
        }
    }
}
