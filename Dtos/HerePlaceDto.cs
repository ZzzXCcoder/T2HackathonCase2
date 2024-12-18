namespace T2HackathonCase2.Dtos
{
    public class HerePlaceDto
    {
        public string Title { get; set; }
        public string Id { get; set; }

        public PositionDto Position { get; set; }

        public AddressDto Address { get; set; }

        public int Distance { get; set; }

        public List<CategoryDto> Categories { get; set; }

        public List<OpeningHourDto> OpeningHours { get; set; }

        public string? ImageUrl { get; set; }

    }
    public class PositionDto
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
    public class AddressDto
    {
        public string Label { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
    }
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
    }
    public class OpeningHourDto
    {
        public List<CategoryDto> Categories { get; set; }
        public List<string> Text { get; set; }
        public bool IsOpen { get; set; }
        public List<StructuredDto> Structured { get; set; }
    }
    public class StructuredDto
    {
        public string Start { get; set; }
        public string Duration { get; set; }
        public string Recurence { get; set; }
    }
}
