using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Grade
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Grade1 { get; set; }

   // public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
