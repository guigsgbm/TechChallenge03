using App.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Application.DTOs;

public class CreateNewsDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}