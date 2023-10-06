using System;
using System.Collections.Generic;

namespace WebApplicationKinoAPI0510;

public partial class Comment
{
    public int Id { get; set; }

    public string? TextContent { get; set; }

    public DateTime Date { get; set; }

    public int? UserId { get; set; }

    public int? TitleId { get; set; }

    public virtual Title? Title { get; set; }

    public virtual User? User { get; set; }
}
