using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceManager.Models
{
    [Table("tblVehicles")]
    public class Vehicles
    {
        [Key]
        public int pkiVehicleID { get; set; }
        public int? fkiUserID { get; set; }
        public int? fkiMakeID { get; set; }
        public string? txtModelName { get; set; }
        public int? makeYear { get; set; }
        public long? mileage { get; set; }
        public bool? isUnderWarranty { get; set; }
        public DateTime? dateCreated { get; set; }
        public bool? isActive { get; set; }

    }
}
