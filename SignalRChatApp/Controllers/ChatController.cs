using Microsoft.AspNetCore.Mvc;

namespace SignalRChatApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
