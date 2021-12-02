using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryAddingError
{
    public class Occupation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index]
        public string Title { get; set; }
    }
}
