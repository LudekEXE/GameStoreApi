namespace GameStore.Api.Dtos.GamesOpertionsDto;

public record GameSummaryDto(
    int Id,
    string Name,
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate);