namespace bankCodeFirst.Models
{
    public class Account
    {
        public string Id { get; set; } = "";
        public string Number { get; set; } = "";
        public string Holder { get; set; } = "";
        public decimal Balance { get; set; } = 0.0m;
        public string AccountStatusId { get; set; } = "";
        public AccountStatus?  AccountStatus { get; set; }= null;
      

        public List<Transactions>? Transactions { get; set; } = null;


    }

    public class AccountDTO
    {
        public string Holder { get; set; } = "";
        public decimal Balance { get; set; } = 0.0m;
    }
    public enum status
    {
        accStatus01, accStatus02, accStatus03, accStatus04
    }
}
