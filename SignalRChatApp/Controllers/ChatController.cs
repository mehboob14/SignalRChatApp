using Microsoft.AspNetCore.Mvc;

namespace SignalRChatApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View("login");
        }
        public IActionResult JoinRoom(string displayName, string roomName)
        {
            if(string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(roomName))
            {
                return RedirectToAction("login");
            }
            ViewBag.DisplayName = displayName;
            ViewBag.RoomName = roomName;

            return View("Index");

        }
    }
}
