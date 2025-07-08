namespace FactoryMethodPattern.Payments.Abstractions
{
    public class Payment
    {
        public int CustomerId { get; set; }
        public double ChargedAmount { get; set; }
        public Guid ReferenceNumber { get; set; }
    }
}