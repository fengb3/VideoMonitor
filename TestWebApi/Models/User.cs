using System;
using System.Collections.Generic;

namespace TestWebApi.Models;

public partial class User
{
    public long Uid { get; set; }

    public long? MostRecentUserRecordId { get; set; }

    public string Name { get; set; } = null!;

    public string FaceUrl { get; set; } = null!;

    public virtual UserRecord? MostRecentUserRecord { get; set; }

    public virtual ICollection<UserRecord> UserRecords { get; set; } = new List<UserRecord>();

    public virtual ICollection<Video> VideoAuthorUs { get; set; } = new List<Video>();

    public virtual ICollection<Video> VideoUserUs { get; set; } = new List<Video>();
}
