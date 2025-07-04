namespace PayrollSystem.Core
{
    public class Employee
    {
        public string FullName { get; set; }
        public IEnumerable<PayItem> PayItems { get; set; }
    }
}
