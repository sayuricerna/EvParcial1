using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EVParcial1.Models;

[Index("Especialidad", Name = "IX_Medicos_Especialidad")]
[Index("Cedula", Name = "UQ_Medicos_Cedula", IsUnique = true)]
public partial class Medico
{
    [Key]
    public int MedicoId { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string Cedula { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Especialidad { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Medico")]
    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}
