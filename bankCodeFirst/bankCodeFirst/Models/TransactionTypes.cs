namespace bankCodeFirst.Models
{
    public class TransactionTypes
    {
        public string Id { get; set; } = "";
        public string Code { get; set; } = "";
        public string? Text { get; set; } = null;
        
        public List<Transactions>? Transactions { get; set; } = null;
    }
}
