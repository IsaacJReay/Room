using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Room.Models;

namespace Room.Controllers;

public class RoomController : Controller
{
    private readonly ILogger<RoomController> _logger;

    public RoomController(ILogger<RoomController> logger)
    {
        _logger = logger;
    }

    public IActionResult ViewRoom()
    {
        List<RoomModal> RoomList = new List<RoomModal>();
        ViewData["RoomModal"] = RoomList;
        return View();
    }

    public IActionResult ViewRoomType()
    {
        List<RoomTypeModal> RoomTypeList = new List<RoomTypeModal>();
        ViewData["RoomTypeModal"] = RoomTypeList;
        return View();
    }

    public IActionResult ViewForm()
    {
        return View();
    }

    public IActionResult ProcessForm()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}