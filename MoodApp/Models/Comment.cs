namespace MoodApp.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }


        //public ICollection<Enrollment> Enrollments { get; set; }
    }
}