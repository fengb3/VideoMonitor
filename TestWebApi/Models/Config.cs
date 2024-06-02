﻿using System;
using System.Collections.Generic;

namespace TestWebApi.Models;

public partial class Config
{
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public long LastTimeUpdate { get; set; }
}
