using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Services;

namespace MySocialNetwork.Controllers
{
    public class DialogController : Controller
    {
        private DialogService dialogService = new DialogService();
        public ActionResult OpenDialog(string login, int firstUserId, int secondUserId)
        {
            DialogDto dialog = dialogService.GetDialogDto(login, firstUserId, secondUserId);
            return View("Dialog", dialog);
        }
    }
}