namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Room {
    [Key]
    public int RoomId { get; set; }
    public string? Name { get; set; }
    public int Capacity { get; set; }
    public string? Image { get; set; }

    public int HotelId { get; set; }
    [ForeignKey("HotelId")]
    public Hotel? Hotel { get; set; }
    public List<Booking>? Bookings {get; set;}
}