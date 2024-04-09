namespace CMS_MVC.Models
{
    public class ContentDetail
    {
        public int ContentDetailId { get; set; }
        public int CourseContentId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
