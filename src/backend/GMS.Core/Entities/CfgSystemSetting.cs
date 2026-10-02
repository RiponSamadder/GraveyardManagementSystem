using System;
using System.Collections.Generic;

namespace GMS.Core.Entities;

public partial class CfgSystemSetting
{
    public long SettingId { get; set; }

    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string? Description { get; set; }

    public bool IsEncrypted { get; set; }

    public bool IsActive { get; set; }
}
