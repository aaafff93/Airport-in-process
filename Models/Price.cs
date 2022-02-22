namespace Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlightID { get; set; }

        public decimal? FirstClass { get; set; }

        public decimal? BusinessClass { get; set; }

        public decimal EconomyClass { get; set; }

        public virtual Flight Flight { get; set; }
    }
}
