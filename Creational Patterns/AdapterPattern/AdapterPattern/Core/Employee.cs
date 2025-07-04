namespace AdapterPattern.Core
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<PayItem> PayItems { get; set; }
    }
}
