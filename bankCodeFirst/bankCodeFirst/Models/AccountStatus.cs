namespace bankCodeFirst.Models
{
    public class AccountStatus
    {
        public string Id { get; set; } = "";
        public string Code { get; set; } = "";
        public string? Text { get; set; } = null;

        public List<Account>? Accounts { get; set; } = null;

    }
}
