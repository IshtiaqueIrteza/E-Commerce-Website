using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMEntity;

namespace PMRepository
{
    public class UserRepository
    {
        private ProductDBContext context = new ProductDBContext();

        public User LoginValidate(string Username, string Password)
        {
            User validUser = this.context.Users.SingleOrDefault(u => u.Username == Username && u.Password == Password);
            return validUser;
        }

        public string RegistrationValidate(User user)
        {
            User validUserByUsername = this.context.Users.SingleOrDefault(u => u.Username == user.Username);
            User validUserByEmail = this.context.Users.SingleOrDefault(u => u.Email == user.Email);
            Admin validAdmin = this.context.Admins.SingleOrDefault(u => u.Username == user.Username);

            if (validUserByUsername == null && validUserByEmail == null && validAdmin == null)
            {
                this.context.Users.Add(user);
                this.context.SaveChanges();
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }

        public List<User> ReturnTopUsers()
        {
            var result = (from i in this.context.Users
                          orderby i.TotalSpent descending
                          select i).Take(5);

            List<User> uList = result.ToList();

            return uList;
        }

        public List<Transaction> ReturnUserTransaction()
        {
            return this.context.Transactions.ToList();
        }
    }
}
