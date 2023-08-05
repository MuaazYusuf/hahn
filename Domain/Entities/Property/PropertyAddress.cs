using Domain.Base;


namespace Domain.Entities
{
    public class PropertyAddress
    {
        public string? Locality { get; set; } = "";
        public string? PostalCode { get; set; } = "";
        public string? FormattedAddress { get; set; } = "";


        public PropertyAddress(string locality, string postalCode, string formattedAddress)
        {
            Locality = locality;
            PostalCode = postalCode;
            FormattedAddress = formattedAddress;
        }
    }
}