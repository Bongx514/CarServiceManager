using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceManager.Models
{
    [Table("tblMaintenanceTypeLookUp")]
    public class MaintenanceTypeLookUp
    {
        public int pkiMaintenanceTypeID { get; set; }
        public string? txtMaintenanceType { get; set; }
    }
}
