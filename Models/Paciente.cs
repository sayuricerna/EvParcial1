using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EVParcial1.Models;

[Index("Apellido", Name = "IX_Pacientes_Apellido")]
[Index("Cedula", Name = "UQ_Pacientes_Cedula", IsUnique = true)]
public partial class Paciente
{
    [Key]
    public int PacienteId { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string Cedula { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    [Required(ErrorMessage = "Este campo es requerido")]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    [Required(ErrorMessage = "Este campo es requerido")]
    public string Apellido { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaNacimiento { get; set; }


    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Paciente")]
    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}
