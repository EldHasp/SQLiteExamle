using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryAddingError
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index]
        public string FullName { get; set; }


        [Index]
        public int? CountryId { get; set; }
        public Country Country{ get; set; }

        [Index]
        public int? OccupationId { get; set; }
        public Occupation Occupation{ get; set; }

        [Index]
        public int? CompanyId { get; set; }
        public Company Company{ get; set; }
    }
}
