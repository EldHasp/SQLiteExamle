using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemDataModel
{
    [Table("Contacts")]
    public class ContactEntity : Entity<ContactDto, ContactEntity>
    {

        [Required]
        [Index]
        public string Name { get; set; }

        public override ContactDto Create()
            => new ContactDto(Id.Value, Name);

        protected override void OverrideCopyFrom(ContactDto idDto)
        {
            Name = idDto.Name;
        }

        protected override bool OverrideEquals(ContactDto idDto)
        {
            return Name == idDto.Name;
        }
    }
}
