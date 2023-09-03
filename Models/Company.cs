using System.Security.Claims;

namespace Worktastic.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public byte[]? Image { get; set; }
        public List<Job> Jobs { get; set; }
        
    }
}
