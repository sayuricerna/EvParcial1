using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace EVParcial1.Models;

[Index("FechaConsulta", Name = "IX_Consultas_Fecha")]
public partial class Consulta
{
    [Key]
    public int ConsultaId { get; set; }

    public int PacienteId { get; set; }

    public int MedicoId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaConsulta { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string MotivoConsulta { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Diagnostico { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Tratamiento { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    [ForeignKey("MedicoId")]
    [InverseProperty("Consulta")]
    [ValidateNever]
    public virtual Medico Medico { get; set; } = null!;

    [ForeignKey("PacienteId")]
    [InverseProperty("Consulta")]
    [ValidateNever]
    public virtual Paciente Paciente { get; set; } = null!;
}
