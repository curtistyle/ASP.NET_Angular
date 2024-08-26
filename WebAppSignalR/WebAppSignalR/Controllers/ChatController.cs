using Microsoft.AspNetCore.Mvc;

namespace WebAppSignalR.Controllers
{
    public class ChatController : Controller
    {
        public static Dictionary<int, string> Rooms = 
            new Dictionary<int, string>() 
            {
                {1, "Music" },
                {2, "Devs" },
                {3, "Games" },
            };

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Room(int room)
        {
            return View("Room", room);
        }


    }
}
