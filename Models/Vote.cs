﻿using System;
using System.Collections.Generic;

namespace WebApplicationKinoAPI0510;

public partial class Vote
{
    public int? Rating { get; set; }

    public int UserId { get; set; }

    public int TitleId { get; set; }

    public virtual Title Title { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
