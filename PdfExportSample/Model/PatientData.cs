using System;
using System.ComponentModel.DataAnnotations;

namespace PdfExportSample.Model
{
    public class PatientData
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string MRN { get; set; } = string.Empty;
        public string UIN { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;

        public string BillNo { get; set; } = string.Empty;
        public DateTime BillDate { get; set; }
        public string IpaNo { get; set; } = string.Empty;
        public string PreauthId { get; set; } = string.Empty;
        public DateTime DateOfAdmission { get; set; }
        public DateTime DateOfDischarge { get; set; }
        public string CCN { get; set; } = string.Empty;

        public string Surgery { get; set; } = string.Empty;
        public decimal ApprovedAmount { get; set; }
        public int PackageRate { get; set; }
        public decimal BalanceAmountCollected { get; set; }
        public decimal CoPaymentAddition { get; set; }

        public string TPAName { get; set; } = string.Empty;
        public string InsuranceName { get; set; } = string.Empty;
    }
}
