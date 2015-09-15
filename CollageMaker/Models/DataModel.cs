using System.ComponentModel.DataAnnotations;

namespace CollageMaker.Models
{
    public class DataModel
    {
        [Display(Name = "Twitter Name")]
        [Required(ErrorMessage = "Twitter name is required field")]
        public string UserName { get; set; }
        [Display(Name = "Number Of Columns")]
        [Required(ErrorMessage = "Number of Columns is required field")]
        [Range(1, 150, ErrorMessage = "Invalid value")]
        public int NumberOfColumns { get; set; } = 10;
        [Display(Name = "Number Of Rows")]
        [Required(ErrorMessage = "Number Of Rows is required field")]
        [Range(1, 150, ErrorMessage = "Invalid value")]
        public int NumberOfRows { get; set; } = 10;
    }
}