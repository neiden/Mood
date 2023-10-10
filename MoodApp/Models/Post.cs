namespace MoodApp.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime PostDate { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }


    }
}