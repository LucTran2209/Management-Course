namespace CMS_API.Dtos.InputDto
{
    public class ContentDetailInputDto
    {
        public int CourseContentId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
