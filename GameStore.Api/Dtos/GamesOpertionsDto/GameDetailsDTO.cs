namespace GameStore.Api.Dtos.GamesOpertionsDto;

public record GameDetailsDto
(
    int Id,
    string Name,
    int GenreId,
    decimal Price, 
    DateOnly ReleaseDate
);