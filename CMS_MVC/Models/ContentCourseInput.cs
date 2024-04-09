namespace CMS_MVC.Models
{
    public class ContentCourseInput
    {
        public int CourseId { get; set; }
        public int ContentTypeId { get; set; }
        public string ContentTitle { get; set; } = null!;
        public string? Content { get; set; }
    }
}
