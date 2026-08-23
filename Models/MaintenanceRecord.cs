using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceManager.Models
{
    [Table("tblMaintenanceRecord")]
    public class MaintenanceRecord
    {
        [Key]
        public int pkiMaintenanceID { get; set; }
        public int fkiVehicleID { get; set; }
        public int fkiMaintenanceTypeID { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public long? MaintenanceMileage { get; set; }
        public string? txtDescription { get; set; }
        public string? txtNotes { get; set; }
        public bool? isMaintenanceComplete { get; set; }
        public DateTime? dateCreated { get; set; }
    }
}
