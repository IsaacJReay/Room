using System.ComponentModel.DataAnnotations;

namespace Room.Models
{
    public class RoomTypeModal
    {
        [Key]
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; } = default!;
        public string RoomTypeDescription { get; set; } = default!;
        public bool IsActive { get; set; }

    }
}