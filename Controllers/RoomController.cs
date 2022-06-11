using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Room.Models;
using System.Data.SqlClient;
using System.Data;

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
        List<RoomTypeModal> RoomTypeList = new List<RoomTypeModal>();
        SqlConnection connection = connectDB();

        connection.Open();

        String sql = "SELECT RoomTypeId,RoomType FROM RoomTypes";

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
                        }
                    );
                }
            }
        }

        String sql1 = "SELECT TOP 1 RoomId FROM Rooms ORDER BY RoomId DESC; ";

        using (SqlCommand command = new SqlCommand(sql1, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ViewBag.RoomId = reader.GetInt32(0) + 1;
                }
            }
        }
        ViewData["RoomTypeModal"] = RoomTypeList;
        return View();
    }

    public IActionResult InputRoomType()
    {
        SqlConnection connection = connectDB();

        connection.Open();

        String sql = "SELECT TOP 1 RoomTypeId FROM RoomTypes ORDER BY RoomTypeId DESC; ";

        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ViewBag.RoomTypeId = reader.GetInt32(0) + 1;
                }
            }
        }
        return View();
    }

    [HttpPost]
    public IActionResult RegisterRoom(RoomModal room_args)
    {
        SqlConnection connection = connectDB();
        connection.Open();
        String sqlStatement = "SET IDENTITY_INSERT Rooms ON;INSERT INTO Rooms (\"RoomId\",\"RoomName\", \"RoomDescription\", \"RoomTypeId\") VALUES (" + room_args.RoomId + ", '" + room_args.RoomName + "', '" + room_args.RoomDescription + "', " + room_args.RoomTypeId + ");";
        using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
        {
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        return Redirect("/");
    }

    [HttpPost]
    public IActionResult RegisterRoomType(RoomTypeModal room_args)
    {
        SqlConnection connection = connectDB();
        connection.Open();
        String sqlStatement = "SET IDENTITY_INSERT RoomTypes ON;INSERT INTO RoomTypes (\"RoomTypeId\", \"RoomType\", \"RoomTypeDescription\", \"IsActive\") VALUES (" + room_args.RoomTypeId + ",'" + room_args.RoomType + "', '" + room_args.RoomTypeDescription + "', " + Convert.ToInt32(room_args.IsActive) + ");";
        using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
        {
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        return Redirect("/");
    }

    [HttpPost]
    public IActionResult EditRoom(RoomModal room_args)
    {
        SqlConnection connection = connectDB();
        connection.Open();
        String sqlStatement = "UPDATE Rooms SET RoomName='" + room_args.RoomName + "', RoomDescription='" + room_args.RoomDescription + "', RoomTypeId=" + room_args.RoomTypeId + " WHERE RoomId=" + room_args.RoomId + ";";
        using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
        {
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        return Redirect("/");
    }

    [HttpPost]
    public IActionResult EditRoomType(RoomTypeModal room_args)
    {
        SqlConnection connection = connectDB();
        connection.Open();
        String sqlStatement = "UPDATE RoomTypes SET RoomType='" + room_args.RoomType + "', RoomTypeDescription='" + room_args.RoomTypeDescription + "', IsActive=" + Convert.ToInt32(room_args.IsActive) + " WHERE RoomTypeId=" + room_args.RoomTypeId + ";";
        using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
        {
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        return Redirect("/");
    }

    public IActionResult EditRoom(int RoomId, string RoomName, string RoomDescription, int RoomTypeId)
    {
        // Console.WriteLine(RoomId + RoomName + RoomDescription + RoomTypeId);
        List<RoomTypeModal> RoomTypeList = new List<RoomTypeModal>();
        SqlConnection connection = connectDB();

        connection.Open();

        String sql = "SELECT RoomTypeId,RoomType FROM RoomTypes";

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
                        }
                    );
                }
            }
        }
        var room = new RoomModal()
        {
            RoomId = RoomId,
            RoomName = RoomName,
            RoomDescription = RoomDescription,
            RoomTypeId = RoomTypeId
        };
        ViewBag.Room = room;
        ViewData["RoomTypeModal"] = RoomTypeList;
        return View();
    }

    public IActionResult EditRoomType(int RoomTypeId, string RoomType, string RoomTypeDescription, bool IsActive)
    {
        var room = new RoomTypeModal()
        {
            RoomTypeId = RoomTypeId,
            RoomType = RoomType,
            RoomTypeDescription = RoomTypeDescription,
            IsActive = IsActive
        };
        ViewBag.RoomType = room;
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
