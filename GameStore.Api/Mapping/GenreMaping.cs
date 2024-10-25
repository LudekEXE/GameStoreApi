using GameStore.Api.Dtos.GamesOpertionsDto;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GenreMaping
{
    public static GenreDto ToDto(this Genre genre)
    {
     return new GenreDto(genre.Id, genre.Name);
    }
}