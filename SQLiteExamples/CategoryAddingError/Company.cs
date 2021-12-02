using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryAddingError
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index]
        public string Title { get; set; }


        [Required]
        [Index]
        public int CountryId { get; set; }
        public Country Country{ get; set; }

        [Required]
        [Index]
        public int OccupationId { get; set; }
        public Occupation Occupation{ get; set; }
    }
}
