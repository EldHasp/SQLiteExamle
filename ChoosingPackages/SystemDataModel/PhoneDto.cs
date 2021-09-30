namespace SystemDataModel
{
    public class PhoneDto : IdDto
    {
        public int ContactId { get; }

        public string Title { get; }

        public string Number { get; }

        public PhoneDto(int contactId, string title, string number)
        {
            ContactId = contactId;
            Title = title;
            Number = number;
        }

        internal PhoneDto(int id, int contactId, string title, string number)
            : base(id)
        {
            ContactId = contactId;
            Title = title;
            Number = number;
        }
    }

}
