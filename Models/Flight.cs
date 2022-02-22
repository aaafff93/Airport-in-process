namespace Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Flight")]
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            BoardingPasses = new HashSet<BoardingPass>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlightID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Departure { get; set; }

        [Required]
        [StringLength(50)]
        public string Arrival { get; set; }

        public TimeSpan BoardingTime { get; set; }

        public TimeSpan LastCallTime { get; set; }

        public TimeSpan OutTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public short AvailableSeats { get; set; }

        public short SoldSeats { get; set; }

        public int AircraftID { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardingPass> BoardingPasses { get; set; }

        public virtual Price Price { get; set; }
    }
}
