using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.Dtos
{
    public class VilaDto
    {
       
        public int VilaId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public DateTime BuildDate { get; set; }
    }
}
