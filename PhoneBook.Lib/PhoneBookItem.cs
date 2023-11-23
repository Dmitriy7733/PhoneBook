namespace PhoneBook.Lib
{
    public record PhoneBookItem
    {
        public int Id { get; set; }
        public string? LastName {  get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}