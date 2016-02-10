//using System.Collections.Generic;
//using System.Linq;

//namespace Tetris
//{
//    public class UserContainer
//    {
//        private List<User> Users { get; set; }

//        public void AddUser(User newUser)
//        {
//            if (Users.Exists(c => c.Username == newUser.Username.ToLowerInvariant())) throw new UserAlreadyExistExceptions();
//            Users.Add(newUser);
//            return;
//        }

//        public User GetUser(string username)
//        {
//            return Users.SingleOrDefault(c => c.Username.ToLowerInvariant() == username.ToLowerInvariant());
//        }
//    }
//}
