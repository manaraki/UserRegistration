using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Repository;
namespace Datalayer.Services
{
    public class UserRepository : IUserRepository
    {
        //Declare an object of UserInfoEntities
        UserInfoEntities _db;

        public UserRepository() 
        {
            // Instantiate database
            _db = new UserInfoEntities();
        }
        public void Insert(User user)
        {
            _db.Users.Add(user);
        }

        public void Delete(User user)
        {
            _db.Entry(user).State=EntityState.Deleted;
        }

        public List<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User GetById(int id)
        {
            return _db.Users.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _db.Entry(user).State=EntityState.Modified;
        }
    }
}
