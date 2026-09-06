namespace CarServiceManager.ViewModels
{
    public class vw_VehicleDetails
    {
        public int pkiVehicleId { get; set; }
        public int? fkiUserId { get; set; }
        public string? txtMakeName { get; set; }
        public string? txtModelName { get; set; }
        public int? makeYear { get; set; }
        public int? mileage { get; set; }
        public string? txtMaintenanceType { get; set; }
        public DateTime? MaintenanceDate  { get; set; }
        public long? MaintenanceMileage { get; set; }
        public string? txtDescription { get; set; }
        public string? txtNotes { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public bool? isUnderWarranty { get; set; }
        public bool? isActive { get; set; }
    }
}
