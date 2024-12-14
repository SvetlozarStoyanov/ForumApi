namespace Models.DTOs.Users.Output
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
    }
}
