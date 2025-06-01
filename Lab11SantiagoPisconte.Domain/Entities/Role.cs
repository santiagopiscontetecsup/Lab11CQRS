﻿using System;
using System.Collections.Generic;

namespace Lab11SantiagoPisconte.Api.Models;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
