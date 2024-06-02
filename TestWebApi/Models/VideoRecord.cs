using System;
using System.Collections.Generic;

namespace TestWebApi.Models;

public partial class VideoRecord
{
    public long VrId { get; set; }

    public string BvId { get; set; } = null!;

    public long TimeStamp { get; set; }

    public int Likes { get; set; }

    public int Dislikes { get; set; }

    public int Coins { get; set; }

    public int Favorites { get; set; }

    public int Shares { get; set; }

    public int Danmaku { get; set; }

    public int Comments { get; set; }

    public int Views { get; set; }

    public int ViewingNum { get; set; }

    public virtual Video Bv { get; set; } = null!;

    public virtual Video? Video { get; set; }
}
