using System.Collections.Generic;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Utils;

namespace MySocialNetwork.Services
{
    public class DialogService
    {
        private DialogMapper dialogMapper = new DialogMapper();
        private Mapper mapper = new Mapper();
        private DialogManager dialogManager = new DialogManager();
        private UserManager userManager = new UserManager();

        public DialogDto GetDialogDto(string login, int firstUserId, int secondUserId)
        {
            DialogDto dialogDto = new DialogDto()
            {
                User = GetTalker(login),
                WallId = GetDialogWallId(firstUserId, secondUserId)
            };
            return dialogDto;
        }
        
        public int GetDialogWallId(int firstUserId, int secondUserId)
        {
            int wallId;
            try
            {
                if (firstUserId < secondUserId)
                {
                    wallId = dialogManager.OpenDialog(firstUserId, secondUserId).WallId;
                }
                else
                {
                    wallId = dialogManager.OpenDialog(secondUserId, firstUserId).WallId;
                }
            }
            catch
            {
                throw;
            }
            return wallId;
        }

        public List<RePostingPoint> GetPoints(string login)
        {
            return dialogManager.GetRePostingPoints(login);
        }
        
        public UserInfoDto GetTalker(string talkerLogin)
        {
            User user = userManager.GetUserByLogin(talkerLogin);
            UserInfoDto userInfo = mapper.FromUserAuthorDto(user);
            return userInfo;
        }
        
    }
}