using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using MySocialNetwork.DTO;

namespace MySocialNetwork.DAO
{
    public class DialogManager
    {
        private SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
        private PageManager pageManager = new PageManager();
        private UserManager userManager = new UserManager();
        public Dialog OpenDialog(int firstUserId, int secondUserId)
        {
            Dialog dialog;
            try
            {
                if (dbContext.Dialogs.Any(d => d.FirstUserId == firstUserId && d.SecondUserId == secondUserId))
                {
                    dialog = LoadDialog(firstUserId, secondUserId);
                }
                else
                {
                    StartDialog(firstUserId, secondUserId);
                    dialog = LoadDialog(firstUserId, secondUserId);
                }
            }
            catch
            {
                throw;
            }
            return dialog;
        }
        
        private Dialog LoadDialog(int firstUserId, int secondUserId)
        {
            Dialog dialog = dbContext.Dialogs.Where(d => d.FirstUserId == firstUserId && d.SecondUserId == secondUserId)
                .Include(d => d.Wall).First();
            IEnumerable<Post> posts = pageManager.GetPostsOfWall(dialog.WallId);
            dialog.Wall.Posts = posts.ToList();
            return dialog;
        }
        
        private void StartDialog(int firstUserId, int secondUserId)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Wall wall = pageManager.CreateWall(WallTypes.Dialog, "Dialog");
                    wall = dbContext.Walls.Add(wall);
                    dbContext.SaveChanges();
                    Dialog dialog = new Dialog()
                    {
                        FirstUserId = firstUserId,
                        SecondUserId = secondUserId,
                        Unread = false
                    };
                    dialog.Wall = wall;
                    dbContext.Dialogs.Add(dialog);
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

        public List<RePostingPoint> GetRePostingPoints(string login)
        {
            List<RePostingPoint> points = new List<RePostingPoint>();
            User user = userManager.GetUserByLogin(login);
            RePostingPoint mainWall = new RePostingPoint()
            {
                Title = "My wall",
                DestinationWallId = user.Walls.Where(m => m.WallType.Title == WallTypes.Main.ToString()).First().Id
            };
            points.Add(mainWall);
            Dialog dialog;
            foreach (Friendship friendship in user.Friendships)
            {
                int friendId = friendship.FriendId;
                if (friendId < user.Id)
                {
                    dialog = OpenDialog(friendId, user.Id);
                }
                else
                {
                    dialog = OpenDialog(user.Id, friendId);
                }

                RePostingPoint point = new RePostingPoint()
                {
                    Title = friendship.Friend.FirstName + " " + friendship.Friend.SecondName,
                    DestinationWallId = dialog.WallId
                };
                points.Add(point);
            }
            return points;
        }
    }
}