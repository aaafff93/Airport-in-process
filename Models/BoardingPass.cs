namespace Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoardingPass")]
    public partial class BoardingPass
    {
        public int BoardingPassID { get; set; }

        [Required]
        [StringLength(50)]
        public string PassengerName { get; set; }

        [Required]
        [StringLength(20)]
        public string Passport { get; set; }

        [Required]
        [StringLength(15)]
        public string Seat { get; set; }

        public decimal Price { get; set; }

        public int FlightID { get; set; }

        public virtual Flight Flight { get; set; }
    }
}
