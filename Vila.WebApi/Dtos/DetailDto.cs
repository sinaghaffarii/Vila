using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.Dtos
{
    public class DetailDto
    {
        public int DetailId { get; set; }
        [Required]
        public int VilaId { get; set; }
        public string What { get; set; }
        public string Value { get; set; }
    }
}
