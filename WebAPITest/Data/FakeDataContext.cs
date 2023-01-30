using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPITest.Data
{
    public static class FakeDataContext
    {
        public static List<AppUser> InitData()
        {
            List<AppUser> items = new List<AppUser>();

            items.Add(new AppUser {
                Id = 1,
                UserName = "admin",
                Gender = "male",
                DisplayName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                Created = DateTime.Parse("2020-06-24"),
                LastActive = DateTime.Parse("2020-06-21"),
            });

            items.Add(new AppUser
            {
                Id = 2,
                UserName = "Lisa",
                Gender = "female",
                DisplayName = "Lisa",
                FirstName = "Lisa",
                LastName = "Thatcher",
                Created = DateTime.Parse("2020-06-24"),
                LastActive = DateTime.Parse("2020-06-21"),
            });

            items.Add(new AppUser
            {
                Id = 3,
                UserName = "Karen",
                Gender = "female",
                DisplayName = "Karen",
                FirstName = "Karen",
                LastName = "Solace",
                Created = DateTime.Parse("2019 -12-09"),
                LastActive = DateTime.Parse("2020-05-06")
            });

            items.Add(new AppUser
            {
                Id = 4,
                UserName = "Margo",
                Gender = "female",
                DisplayName = "Margo",
                FirstName = "Margo",
                LastName = "Hansley",
                Created = DateTime.Parse("2019-08-10"),
                LastActive = DateTime.Parse("2020-05-12")
            });

            items.Add(new AppUser
            {
                Id = 5,
                UserName = "Lois",
                Gender = "female",
                DisplayName = "Lois",
                FirstName = "Lois",
                LastName = "Levine",
                Created = DateTime.Parse("2019-04-24"),
                LastActive = DateTime.Parse("2020-06-17")
            });

            items.Add(new AppUser
            {
                Id = 6,
                UserName = "Ruthie",
                Gender = "female",
                DisplayName = "Ruthie",
                FirstName = "Ruthie",
                LastName = "Elsher",
                Created = DateTime.Parse("2019-04-30"),
                LastActive = DateTime.Parse("2020-06-21")
            });

            items.Add(new AppUser
            {
                Id = 7,
                UserName = "Todd",
                Gender = "male",
                DisplayName = "Todd",
                FirstName = "Todd",
                LastName = "Hudson",
                Created = DateTime.Parse("2019-04-29"),
                LastActive = DateTime.Parse("2020-05-16")
            });

            items.Add(new AppUser
            {
                Id = 8,
                UserName = "Porter",
                Gender = "male",
                DisplayName = "Porter",
                FirstName = "Porter",
                LastName = "Mason",
                Created = DateTime.Parse("2020-04-05"),
                LastActive = DateTime.Parse("2020-06-23")
            });

            items.Add(new AppUser
            {
                Id = 9,
                UserName = "Mayo",
                Gender = "male",
                DisplayName = "Mayo",
                FirstName = "Mayo",
                LastName = "Wyatt",
                Created = DateTime.Parse("2020-03-14"),
                LastActive = DateTime.Parse("2020-05-17")
            });

            items.Add(new AppUser
            {
                Id = 10,
                UserName = "Skinner",
                Gender = "male",
                DisplayName = "Skinner",
                FirstName = "Skinner",
                LastName = "Hudson",
                Created = DateTime.Parse("2019-01-28"),
                LastActive = DateTime.Parse("2020-06-07")
            });

            items.Add(new AppUser
            {
                Id = 11,
                UserName = "Davis",
                Gender = "male",
                DisplayName = "Davis",
                FirstName = "Davis",
                LastName = "Carter",
                Created = DateTime.Parse("2020-02-25"),
                LastActive = DateTime.Parse("2020-06-11")
            });
            return items;
        }
    }
}
