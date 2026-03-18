

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string County { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;

        protected Address()
        { }

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string county, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            County = county;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string county, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
            ArgumentException.ThrowIfNullOrWhiteSpace(county);
            ArgumentException.ThrowIfNullOrWhiteSpace(state);
            ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

            return new Address(firstName, lastName, emailAddress, addressLine, county, state, zipCode);

        }
    }
}
