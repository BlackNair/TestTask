namespace TestTaskConfig.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
    }
}
