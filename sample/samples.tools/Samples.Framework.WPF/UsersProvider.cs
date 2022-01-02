using System.Collections.Generic;
using System.Linq;

namespace Samples.Framework.WPF
{
    public static class UsersProvider
    {
        static UsersProvider()
        {
            Users = new List<User>()
            {
                new User("Berlin"),
                new User("Denver"),
                new User("Helsinki"),
                new User("Moscou"),
                new User("Nairobi"),
                new User("Oslo"),
                new User("Rio"),
                new User("Tokio"),
                new User("Professor","Foundry"),
            }.OrderByDescending(u => u.Name).ToList();
        }
        public static IEnumerable<User> Users { get; }
    }
    public class User
    {
        public User(string name, params string[] groups)
        {
            Name = name;
            IsProfessor = name == "Professor";
            Groups = new List<string>(groups);
        }
        public string Name { get; }
        public bool IsProfessor { get; }
        public List<string> Groups { get; }
    }
}
