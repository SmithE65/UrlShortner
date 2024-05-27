using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DTOs;

public record CreateDto([Url] [StringLength(8000)] string Url);