using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemDataFW
{
    [Table("Phones")]
    public class PhoneEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index]
        public int ContactId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        //[Index]
        public string Number { get; set; }
    }

}
