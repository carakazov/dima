using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Ajax.Utilities;
using MySocialNetwork.DTO;
using WebGrease.Css.Extensions;

namespace MySocialNetwork.DAO
{
    public class UserManager
    {
        private SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
        private FriendshipManager friendshipManager = new FriendshipManager();
        public User GetUserByLogin(string login)
        {
            try
            {
                User user = dbContext.Users.Where(u => u.Login == login).Include(u => u.Walls).Include(u => u.ScoredPosts).First();
                foreach (Wall wall in user.Walls)
                {
                    WallType wallType = dbContext.WallTypes.Where(wt => wt.Id == wall.WallTypeId).First();
                    wall.WallType = wallType;
                }

                List<Friendship> friendships = dbContext.Friendships.Where(f => f.UserId == user.Id)
                    .Include(f => f.Friend).Include(f => f.Type).ToList();
                user.Friendships = friendships;
                user.SentRequests = friendshipManager.GetSentRequestsOfUser(user.Id);
                user.ReceivedRequests = friendshipManager.GetReceivedRequestsOfUser(user.Id);
                return user;
            }
            catch
            {
                throw;
            }
        }

        public List<User> GetUserOfGroup(int groupId)
        {
            List<User> users = new List<User>();
            Group group = dbContext.Groups.Where(g => g.Id == groupId).Include(g => g.ReaderProfiles).First();
            foreach (ReaderProfile profile in group.ReaderProfiles)
            {
                User user = dbContext.Users.Where(u => u.Id == profile.UserId).First();
                users.Add(user);
            }

            return users;
        }
        
        public List<User> GetAllUsers()
        {
            return dbContext.Users.ToList();
        }

        public void BanUser(int userId)
        {
            User user = dbContext.Users.Where(u => u.Id == userId).First();
            user.Banned = !user.Banned;
            dbContext.SaveChanges();
        }
        
        public void AddUser(User user)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    User addedUser = dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    int mainWallTypeId = dbContext.WallTypes.Where(wt => wt.Title == WallTypes.Main.ToString()).First()
                        .Id;
                    int photoWallTypeId = dbContext.WallTypes.Where(wt => wt.Title == WallTypes.Photo.ToString())
                        .First().Id;
                    Wall mainWall = new Wall()
                    {
                        OwnerId = addedUser.Id,
                        WallTypeId = mainWallTypeId,
                        Title = WallTypes.Main.ToString()
                    };
                    Wall avatarsWall = new Wall()
                    {
                        OwnerId =  addedUser.Id,
                        WallTypeId = photoWallTypeId,
                        Title = "Avatars"
                    };
                    FriendshipType commonFriendship = new FriendshipType()
                    {
                        Title = "common",
                        TypeOwnerId = addedUser.Id
                    };
                    dbContext.FriendshipTypes.Add(commonFriendship);
                    dbContext.Walls.Add(mainWall);
                    dbContext.Walls.Add(avatarsWall);
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public List<User> FindUsers(FindUsersDto userInfo)
        {
            try
            {
                List<User> foundedUsers = dbContext.Users.ToList();
                if (userInfo.FirstName != null)
                {
                    foundedUsers = foundedUsers.Where(u => u.FirstName == userInfo.FirstName).ToList();
                }

                if (userInfo.SecondName != null)
                {
                    foundedUsers = foundedUsers.Where(u => u.SecondName == userInfo.SecondName).ToList();
                }

                if (userInfo.MiddleName != null)
                {
                    foundedUsers = foundedUsers.Where(u => u.MiddleName == userInfo.MiddleName).ToList();
                }

                if (userInfo.MinAge == null)
                {
                    userInfo.MinAge = 0;
                }

                if (userInfo.MaxAge == null)
                {
                    userInfo.MaxAge = 120;
                }

                DateTime now = DateTime.Now;
                foundedUsers = foundedUsers.Where(u => FindCorrectAge(userInfo.MinAge, userInfo.MaxAge, u.BirthDate))
                    .ToList();
                return foundedUsers;
            }
            catch
            {
                throw;
            }
        }

        public User GetById(int id)
        {
            return dbContext.Users.Where(u => u.Id == id).Include(w => w.ReaderProfiles).First();
        }
        
        private bool FindCorrectAge(int minAge, int maxAge, DateTime birthday)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = now - birthday;
            int age = timeSpan.Days / 365;
            if (age <= maxAge && age >= minAge)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}