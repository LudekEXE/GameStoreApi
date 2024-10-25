using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos.PlayerOperationsDto;

public record CreatePlayerDto
(
    [Required][StringLength(30)]string Email,
    [Required][StringLength(30)]string Nick,
    [Required][MinLength(8)] string Password
);