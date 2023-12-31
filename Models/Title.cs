﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebApplicationKinoAPI0510.Models;

namespace WebApplicationKinoAPI0510;

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

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public static implicit operator Title(ObservableCollection<Title> v)
    {
        throw new NotImplementedException();
    }
}
