using System;
using System.Collections.Generic;

namespace TestWebApi.Models;

public partial class UserRecord
{
    public long UrId { get; set; }

    public long Uid { get; set; }

    public long TimeStamp { get; set; }

    public int FollowerNum { get; set; }

    public int FollowingNum { get; set; }

    public int ArchiveNum { get; set; }

    public int LikeNum { get; set; }

    public virtual User UidNavigation { get; set; } = null!;

    public virtual User? User { get; set; }
}
