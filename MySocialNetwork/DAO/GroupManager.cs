using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MySocialNetwork.DTO;

namespace MySocialNetwork.DAO
{
    public class GroupManager
    {
        private SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
        private PageManager pageManager = new PageManager();

        public void AcceptRequest(int userId, int groupId)
        {
            GroupEnteringRequest request = dbContext.GroupEnteringRequests
                .Where(r => r.SenderId == userId && r.GroupId == groupId).First();
            request.AnswerDate = DateTime.Now;
            dbContext.SaveChanges();
        }

        public void DeleteRequest(int userId, int groupId)
        {
            GroupEnteringRequest request = dbContext.GroupEnteringRequests
                .Where(r => r.SenderId == userId && r.GroupId == groupId).First();
            dbContext.GroupEnteringRequests.Remove(request);
            dbContext.SaveChanges();
        }
        
        public List<GroupEnteringRequest> GetRequests(int groupId)
        {
            return dbContext.GroupEnteringRequests.Where(g => g.GroupId == groupId &&
                                                              g.AnswerDate == DateTime.MinValue).ToList();
        }

        public bool CheckRequest(int userId, int groupId)
        {
            return dbContext.GroupEnteringRequests.Any(r => r.SenderId == userId && r.GroupId == groupId);
        }

        public void AddNewGroup(Group group, int creatorId)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Group addedGroup = dbContext.Groups.Add(group);
                    Wall wall = pageManager.CreateWall(WallTypes.Main, "Main");
                    addedGroup.Walls.Add(wall);
                    dbContext.SaveChanges();
                    Subscribe(addedGroup.Id, creatorId, GroupRoles.Admin);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void AddRequest(int userId, int groupId)
        {
            GroupEnteringRequest request = new GroupEnteringRequest()
            {
                GroupId = groupId,
                SenderId = userId,
                SendingDate = DateTime.Now,
                AnswerDate = DateTime.MinValue
            };
            dbContext.GroupEnteringRequests.Add(request);
            dbContext.SaveChanges();
        }
        
        public Group GetGroupById(int groupId)
        {
            return dbContext.Groups.Where(g => g.Id == groupId).First();
        }
        
        public void Subscribe(int groupId, int userId, GroupRoles role)
        {
            GroupRole groupRole = dbContext.GroupRoles.Where(r => r.Title == role.ToString()).First();
            ReaderProfileState state = dbContext.ReaderProfileStates
                .Where(s => s.Title == ReaderProfileStates.FullAccess.ToString()).First();
            ReaderProfile profile = new ReaderProfile()
            {
                GroupId = groupId,
                UserId = userId,
                GroupRoleId = groupRole.Id,
                ReaderProfileStateId = state.Id
            };
            dbContext.ReaderProfiles.Add(profile);
            dbContext.SaveChanges();
        }

        public int GetMainWallId(int groupId)
        {
            List<Wall> groupsWalls = dbContext.Walls.Where(w => w.GroupId == groupId).Include(w => w.WallType).ToList();
            foreach (Wall wall in groupsWalls)
            {
                if (wall.WallType.Title == WallTypes.Main.ToString())
                {
                    return wall.Id;
                }
            }

            return 0;
        }
        
        public List<Group> GetGroupsOfUser(int userId)
        {
            IEnumerable<ReaderProfile> profiles = dbContext.ReaderProfiles.Where(p => p.UserId == userId).ToList();
            List<Group> groups = new List<Group>();
            foreach (ReaderProfile profile in profiles)
            {
                Group group = dbContext.Groups.Where(g => g.Id == profile.GroupId).First();
                groups.Add(group);
            }

            return groups;
        }

        public GroupRoles GetUserRoleInGroup(int userId, int groupId)
        {
            if (dbContext.ReaderProfiles.Any(p => p.GroupId == groupId && p.UserId == userId))
            {
                ReaderProfile profile = dbContext.ReaderProfiles.Where(p => p.GroupId == groupId && p.UserId == userId)
                    .First();
                GroupRole role = dbContext.GroupRoles.Where(r => r.Id == profile.GroupRoleId).First();
                if (role.Title == GroupRoles.Admin.ToString())
                {
                    return GroupRoles.Admin;
                }
                else if (role.Title == GroupRoles.Moder.ToString())
                {
                    return GroupRoles.Moder;
                }
                else
                {
                    return GroupRoles.Reader;
                }
            }
            else
            {
                return GroupRoles.Reader;
            }
        }

        public List<Group> GetGroupsByName(string groupTitle)
        {
            List<Group> groups = new List<Group>();
            if (dbContext.Groups.Any(g => g.Title == groupTitle))
            {
                groups = dbContext.Groups.Where(g => g.Title == groupTitle).ToList();
            }

            return groups;
        }
    }
}