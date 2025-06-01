﻿using System;
using System.Collections.Generic;

namespace Lab11SantiagoPisconte.Api.Models;

public partial class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
