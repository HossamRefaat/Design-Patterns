namespace FactoryMethodPattern.Payments.Abstractions
{
    public interface IPaymentMethod
    {
        Payment Charge(int customerId, double amount); 
    }
}