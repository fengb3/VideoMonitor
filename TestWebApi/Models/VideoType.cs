using System;
using System.Collections.Generic;

namespace TestWebApi.Models;

public partial class VideoType
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
