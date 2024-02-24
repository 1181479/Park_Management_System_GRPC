namespace Park20.Backoffice.Core.Domain.ParkyWallets
{
    public class ParkyWallet
    {
        public int Id { get; set; }
        public int CurrentBalance { get; set; }
        public List<ParkyWalletMovements> Movements { get; set; }
    }
}
