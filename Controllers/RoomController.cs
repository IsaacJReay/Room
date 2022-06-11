using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Room.Models;
using System.Data.SqlClient;

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
        SqlConnection connection = connectDB();

        connection.Open();

        String sql = "SELECT * FROM Rooms";

        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    RoomList.Add(
                        new RoomModal()
                        {
                            RoomId = reader.GetInt32(0),
                            RoomName = reader.GetString(1),
                            RoomDescription = reader.GetString(2),
                            RoomTypeId = reader.GetInt32(3)
                        }
                    );
                }
            }
        }
        ViewData["RoomModal"] = RoomList;
        return View();
    }

    public IActionResult ViewRoomType()
    {
        List<RoomTypeModal> RoomTypeList = new List<RoomTypeModal>();
        SqlConnection connection = connectDB();

        connection.Open();

        String sql = "SELECT * FROM RoomTypes";

        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    RoomTypeList.Add(
                        new RoomTypeModal()
                        {
                            RoomTypeId = reader.GetInt32(0),
                            RoomType = reader.GetString(1),
                            RoomTypeDescription = reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
                        }
                    );
                }
            }
        }
        ViewData["RoomTypeModal"] = RoomTypeList;
        return View();
    }

    public IActionResult InputRoom()
    {
        return View();
    }

    public IActionResult InputRoomType()
    {
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

    SqlConnection connectDB()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        builder.DataSource = "localhost";
        builder.UserID = "sa";
        builder.Password = "Chheangmai@443";
        builder.InitialCatalog = "ChheangmaiDB";

        SqlConnection connection = new SqlConnection(builder.ConnectionString);

        return connection;
    }
}
