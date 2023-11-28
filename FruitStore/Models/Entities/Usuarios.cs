using System;
using System.Collections.Generic;

namespace FruitStore.Models.Entities;

public partial class Usuarios
{
    public int IdUsuarios { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correoelectronico { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int Rol { get; set; }
}
