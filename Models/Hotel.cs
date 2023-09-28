namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Hotel {
    [Key]
    public int HotelId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }

    public int CityId { get; set; }
    [ForeignKey("CityId")]
    public City? City { get; set; }
    public List<Room>? Rooms { get; set; }
}