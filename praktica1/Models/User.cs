using System;
using System.Collections.Generic;

namespace praktica1.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserSurname { get; set; } = null!;

    public string UserPatronymic { get; set; } = null!;

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int? RoleId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }
}
