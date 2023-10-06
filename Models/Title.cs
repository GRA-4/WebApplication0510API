using System;
using System.Collections.Generic;

namespace WebApplicationKinoAPI0510.Models;

public partial class Title
{
    public int Id { get; set; }

    public string? TitleName { get; set; }

    public string? TitleAdditionalName { get; set; }

    public string? Description { get; set; }

    public int? Date { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<FaveList> FaveLists { get; set; } = new List<FaveList>();
}
