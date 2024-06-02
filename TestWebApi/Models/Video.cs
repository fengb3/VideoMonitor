using System;
using System.Collections.Generic;

namespace TestWebApi.Models;

public partial class Video
{
    public string BvId { get; set; } = null!;

    public int? TypeId { get; set; }

    public long AuthorUid { get; set; }

    public long? MostRecentVideoRecordId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long UploadTimeStamp { get; set; }

    public string CoverUrl { get; set; } = null!;

    public long? UserUid { get; set; }

    public virtual User AuthorU { get; set; } = null!;

    public virtual VideoRecord? MostRecentVideoRecord { get; set; }

    public virtual VideoType? Type { get; set; }

    public virtual User? UserU { get; set; }

    public virtual ICollection<VideoRecord> VideoRecords { get; set; } = new List<VideoRecord>();
}
