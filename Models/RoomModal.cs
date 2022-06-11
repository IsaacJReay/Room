using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Room.Models
{
    public class RoomModal
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomName { get; set; } = default!;
        public string RoomDescription { get; set; } = default!;
        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomTypeModal RoomType { get; set; } = default!;

    }
}
