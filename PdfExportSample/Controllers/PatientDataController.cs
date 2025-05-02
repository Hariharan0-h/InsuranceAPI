using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfExportSample.Data;
using PdfExportSample.Model;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Npgsql.Internal;

namespace PdfExportSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientDataController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PatientData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientData>>> GetPatientTable()
        {
            return await _context.PatientTable.ToListAsync();
        }

        // GET: api/PatientData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientData>> GetPatientData(int id)
        {
            var patientData = await _context.PatientTable.FindAsync(id);

            if (patientData == null)
            {
                return NotFound();
            }

            return patientData;
        }

        // PUT: api/PatientData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientData(int id, PatientData patientData)
        {
            if (id != patientData.Id)
            {
                return BadRequest();
            }

            _context.Entry(patientData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportToPdf()
        {
            var patients = await _context.PatientTable.ToListAsync();

            using var memoryStream = new MemoryStream();
            var writerProperties = new WriterProperties(); // Disable smart mode
            var writer = new PdfWriter(memoryStream, writerProperties);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Patient Data List").SetFontSize(16));

            foreach (var patient in patients)
            {
                var patientInfo = $"ID: {patient.Id}\nName: {patient.Name}\n\n----------------------------";
                document.Add(new Paragraph(patientInfo));
            }

            document.Close();

            return File(memoryStream.ToArray(), "application/pdf", "PatientData.pdf");
        }

        // POST: api/PatientData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientData>> PostPatientData(PatientData patientData)
        {
            _context.PatientTable.Add(patientData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientData", new { id = patientData.Id }, patientData);
        }

        // DELETE: api/PatientData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientData(int id)
        {
            var patientData = await _context.PatientTable.FindAsync(id);
            if (patientData == null)
            {
                return NotFound();
            }

            _context.PatientTable.Remove(patientData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientDataExists(int id)
        {
            return _context.PatientTable.Any(e => e.Id == id);
        }
    }
}
