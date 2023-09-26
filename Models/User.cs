using Microsoft.AspNetCore.Identity;

namespace Worktastic.Models
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte? Age { get; set; }
        public bool IsCompanyAccount { get; set; } = false;
        public Company? Company { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   UserName == user.UserName &&
                   NormalizedUserName == user.NormalizedUserName &&
                   Email == user.Email &&
                   Firstname == user.Firstname &&
                   Lastname == user.Lastname &&
                   Age == user.Age &&
                   IsCompanyAccount == user.IsCompanyAccount &&
                   EqualityComparer<Company?>.Default.Equals(Company, user.Company);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(UserName);
            hash.Add(NormalizedUserName);
            hash.Add(Email);
            hash.Add(Firstname);
            hash.Add(Lastname);
            hash.Add(Age);
            hash.Add(IsCompanyAccount);
            hash.Add(Company);
            return hash.ToHashCode();
        }
    }
}
