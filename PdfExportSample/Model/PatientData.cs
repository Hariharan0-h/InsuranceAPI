using System.ComponentModel.DataAnnotations;

namespace PdfExportSample.Model
{
    public class PatientData
    {
        [Key]
        public int Id { get; set;}
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
