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


        public override bool Equals(object? obj)
        {
            return obj is Job job &&
                   Id == job.Id &&
                   Title == job.Title &&
                   Description == job.Description &&
                   EqualityComparer<Company>.Default.Equals(Company, job.Company) &&
                   Location == job.Location &&
                   Type == job.Type &&
                   Category == job.Category &&
                   Salary == job.Salary &&
                   StartDate == job.StartDate &&
                   EqualityComparer<TimeSpan?>.Default.Equals(Duration, job.Duration);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Title);
            hash.Add(Description);
            hash.Add(Company);
            hash.Add(Location);
            hash.Add(Type);
            hash.Add(Category);
            hash.Add(Salary);
            hash.Add(StartDate);
            hash.Add(Duration);
            return hash.ToHashCode();
        }
    }
}