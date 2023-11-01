namespace App.Application.DTOs;

public class GetNewsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public DateTime PublishDate { get; set; }
}
