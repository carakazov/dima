using MySocialNetwork.DAO;
using MySocialNetwork.DTO;

namespace MySocialNetwork.Utils
{
    public class DialogMapper
    {
        private Mapper mapper = new Mapper();
/*
        public DialogDto GetDialogDto(Dialog dialog)
        {
            DialogDto dialogDto = new DialogDto()
            {
                FirstUser = mapper.FromUserAuthorDto(dialog.FirstUser),
                SecondUser = mapper.FromUserAuthorDto(dialog.SecondUser),
                DialogWall = mapper.FromWallToWallDto(dialog.Wall)
            };
            return dialogDto;
        }*/
    }
}