using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemDataModel
{
    [Table("Phones")]
    public class PhoneEntity : Entity<PhoneDto, PhoneEntity>
    {
        [Required]
        [Index]
        public int ContactId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Index]
        public string Number { get; set; }

        public override PhoneDto Create()
        {
            return new PhoneDto(Id.Value, ContactId, Title, Number);
        }

        protected override void OverrideCopyFrom(PhoneDto idDto)
        {
            ContactId = idDto.ContactId;
            Title = idDto.Title;
            Number = idDto.Number;
        }

        protected override bool OverrideEquals(PhoneDto idDto)
        {
            return
                ContactId == idDto.ContactId &&
                Title == idDto.Title &&
                Number == idDto.Number;
        }
    }

}
