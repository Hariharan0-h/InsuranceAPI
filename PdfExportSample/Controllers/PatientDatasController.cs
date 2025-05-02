using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfExportSample.Data;
using PdfExportSample.Model;

namespace PdfExportSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDatasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PatientDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientData>>> GetPatientTable()
        {
            return await _context.PatientTable.ToListAsync();
        }

        // GET: api/PatientDatas/5
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

        // PUT: api/PatientDatas/5
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

        // POST: api/PatientDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientData>> PostPatientData(PatientData patientData)
        {
            _context.PatientTable.Add(patientData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientData", new { id = patientData.Id }, patientData);
        }

        // DELETE: api/PatientDatas/5
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
        [HttpGet("DownloadPdf/{id}")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var patient = await _context.PatientTable.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            using var stream = new MemoryStream();
            var writer = new iText.Kernel.Pdf.PdfWriter(stream);
            var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);


            document.Add(new iText.Layout.Element.Paragraph("In-Patient Discharge Bill").SetFontSize(16));

            document.Add(new iText.Layout.Element.Paragraph($"Name: {patient.Name}"));
            document.Add(new iText.Layout.Element.Paragraph($"Age: {patient.Age}"));
            document.Add(new iText.Layout.Element.Paragraph($"Sex: {patient.Sex}"));
            document.Add(new iText.Layout.Element.Paragraph($"Date of Admission: {patient.DateOfAdmission:yyyy-MM-dd}"));
            document.Add(new iText.Layout.Element.Paragraph($"Date of Discharge: {patient.DateOfDischarge:yyyy-MM-dd}"));
            document.Add(new iText.Layout.Element.Paragraph($"Surgery: {patient.Surgery}"));
            document.Add(new iText.Layout.Element.Paragraph($"Approved Amount: ${patient.ApprovedAmount:N2}"));


            document.Close();
            var pdfBytes = stream.ToArray();

            return File(pdfBytes, "application/pdf", $"Patient_{id}_DischargeBill.pdf");
        }

    }

}
