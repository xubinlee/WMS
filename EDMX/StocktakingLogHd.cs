namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StocktakingLogHd")]
    public partial class StocktakingLogHd
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(30)]
        public string BillNo { get; set; }

        public DateTime BillDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Checker { get; set; }

        [Required]
        [StringLength(20)]
        public string CheckingTime { get; set; }

        public int Status { get; set; }
    }
}
