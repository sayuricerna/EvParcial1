using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EVParcial1.Models;

namespace EVParcial1.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly ClinicaBuenaVidaContext _context;

        public ConsultasController(ClinicaBuenaVidaContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var clinicaBuenaVidaContext = _context.Consultas.Include(c => c.Medico).Include(c => c.Paciente);
            return View(await clinicaBuenaVidaContext.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        //public IActionResult Create()
        //{
        //    ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nombre");
        //    ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nombre");
        //
        //   return View();
        //}
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(
                _context.Medicos,
                "MedicoId",
                "Nombre"
            );

            ViewData["PacienteId"] = new SelectList(
                _context.Pacientes,
                "PacienteId",
                "Nombre"
            );

            return View();
        }


        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(Consulta consulta)
        {
            consulta.FechaConsulta = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("PacienteId: " + consulta.PacienteId);
            System.Diagnostics.Debug.WriteLine("MedicoId: " + consulta.MedicoId);


            if (consulta.PacienteId == 0 || consulta.MedicoId == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar paciente y médico");
            }
            foreach (var error in ModelState)
            {
                foreach (var subError in error.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Campo: {error.Key} - Error: {subError.ErrorMessage}"
                    );
                }
            }


            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // 🔥 MISMO SelectList que el GET
            ViewData["MedicoId"] = new SelectList(
                _context.Medicos,
                "MedicoId",
                "Nombre",
                consulta.MedicoId
            );

            ViewData["PacienteId"] = new SelectList(
                _context.Pacientes,
                "PacienteId",
                "Nombre",
                consulta.PacienteId
            );

            return View(consulta);
        }

        //public async Task<IActionResult> Create([Bind("ConsultaId,PacienteId,MedicoId,FechaConsulta,MotivoConsulta,Diagnostico,Tratamiento,Observaciones,Estado")] Consulta consulta)
        //{
        //    consulta.FechaConsulta = DateTime.Now;

        //    Console.WriteLine("PacienteId recibido: " + consulta.PacienteId);
        //    Console.WriteLine("MedicoId recibido: " + consulta.MedicoId);

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(consulta);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", consulta.MedicoId);
        //    ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido", consulta.PacienteId);
        //    return View(consulta);
        //}

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", consulta.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido", consulta.PacienteId);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultaId,PacienteId,MedicoId,FechaConsulta,MotivoConsulta,Diagnostico,Tratamiento,Observaciones,Estado")] Consulta consulta)
        {
            if (id != consulta.ConsultaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.ConsultaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", consulta.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido", consulta.PacienteId);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consultas.Any(e => e.ConsultaId == id);
        }
    }
}
