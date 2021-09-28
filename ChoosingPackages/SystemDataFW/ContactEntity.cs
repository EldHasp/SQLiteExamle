using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemDataFW
{
    [Table("Contacts")]
    public class ContactEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[Index]
        //[Column(TypeName = )]
        public string Name { get; set; }
    }

}
