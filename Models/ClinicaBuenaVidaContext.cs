using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EVParcial1.Models;

public partial class ClinicaBuenaVidaContext : IdentityDbContext<IdentityUser>
{
    public ClinicaBuenaVidaContext()
    {
    }

    public ClinicaBuenaVidaContext(DbContextOptions<ClinicaBuenaVidaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consulta> Consultas { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

}
