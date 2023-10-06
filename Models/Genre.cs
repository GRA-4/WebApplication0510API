using System;
using System.Collections.Generic;

namespace WebApplicationKinoAPI0510;

public partial class Genre
{
    public int Id { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
