//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Tetris
//{
////    public class User
////    {
////        public User()
////        {
////            Id  = Guid.NewGuid();
////            Scores = new List<ScoreModel>();
////        }
////        public Guid Id { get; set; }

////        private List<ScoreModel> Scores { get; set; }    
////        public string Username { get; private set; }
////        public string Cridentials { get; set; }

////        public static void CreateNewUser(string username,string password)
////        {
            
////            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
////            UserContainer(new User() {Cridentials = credentials, Username = username});
////        }

////        public  void Login(string username, string password)
////        {
////            if (Auth())
////            {
////                this.Username = username;
////            }
////        }

////        private bool Auth(string username, string password)
////        {
////            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));

////            return false;
////        }

      

////    }
   
////}
