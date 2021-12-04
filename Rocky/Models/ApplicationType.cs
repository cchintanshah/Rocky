using System.ComponentModel.DataAnnotations;

namespace Rocky.Models
{
    public class ApplicationType
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = " Please use valid name")]
        public string Name { get; set; }
    }
}
