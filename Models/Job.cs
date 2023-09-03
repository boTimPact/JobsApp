namespace Worktastic.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public int? Salary { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}