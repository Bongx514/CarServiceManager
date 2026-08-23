using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceManager.Models
{
    [Table("tblVehicleMakeLookUp")]
    public class VehicleMakeLookUp
    {
        [Key]
        public int pkiMakeID { get; set; }
        public string? txtMakeName { get; set; }
    }
}
