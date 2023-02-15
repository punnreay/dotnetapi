namespace bankCodeFirst.Models
{
    public class Transactions
    {
        public string Id { get; set; } = "";

        public decimal Amount { get; set; } = 0.0m;
        public string? Note { get; set; } = "";
        public DateTime? Date { get; set; }

        public string AccountId { get; set; } = "";
        public string TransactionTypeId { get; set; } = "";


        public Account? Accounts { get; set; } = null;
        public TransactionTypes? TransactionTypes { get; set; } = null;
    }


    public class TransactionDTO
    {
        public decimal Amount { get; set; } = 0.0m;
        public string? Note { get; set; } = "";
       
    }
    public enum TranType
    {
        tranType01, tranType02, tranType03, tranType04, tranType05,
    }
}
