namespace AppointmentSearch.Domain.Shared
{
    public record Currency
    {
        public static readonly Currency Usd = new("USD");
        public static readonly Currency Eur = new("EUR");
        public static readonly Currency Sol = new("SOL");
        public static readonly Currency None = new("");
        private Currency(string type)=> Type = type;
        public string?  Type{ get; init; }
        public static readonly IReadOnlyCollection<Currency> All = new[]
        {
            Usd,
            Eur,
            Sol
        };
        public static Currency FromType(string codigo)
        {
            return All.FirstOrDefault(x => x.Type == codigo)
                ?? throw new ApplicationException($"Currency type {codigo} not found");
        }
    }
}