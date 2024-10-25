namespace GameStore.Api.Entities;

public class Player
{
    public int Id { get; set; }
    public required string Emial { get; set; }
    public required string Nick { get; set; }
    public required string Password { get; set; }
}