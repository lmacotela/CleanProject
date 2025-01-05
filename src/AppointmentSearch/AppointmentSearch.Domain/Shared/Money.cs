namespace AppointmentSearch.Domain.Shared
{
    public record Money(decimal Amount, Currency Currency)
    {
        public static Money Zero()
        {
            return new Money(0, Currency.None);
        }
        public static Money Zero(Currency Currency) 
        {
            return new Money(0, Currency);
        }
        public static Money operator +(Money firts, Money second)
        {
            if (firts.Currency != second.Currency)
            {
                throw new InvalidOperationException("Currency type not match");
            }
            return new Money(firts.Amount + second.Amount, firts.Currency);
        }
        
        
        public bool IsZero()=> this== Zero(Currency);
    }
}