namespace Vila.WebApi.Dtos
{
    public class VilaSearchDto
    {
        public int VilaId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Shahr { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public long DayPrice { get; set; }
        public long SellPrice { get; set; }
        public DateTime BuildDate { get; set; }
        public List<DetailDto> Details { get; set; }
    }
}
