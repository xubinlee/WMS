namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InventoryLog")]
    public partial class InventoryLog
    {
        public Guid ID { get; set; }

        public Guid HdID { get; set; }

        public Guid DeptID { get; set; }

        [Required]
        [StringLength(20)]
        public string DeptCode { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        [Required]
        [StringLength(20)]
        public string GoodsCode { get; set; }

        [StringLength(50)]
        public string GoodsName { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        [StringLength(50)]
        public string SPEC { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        public int Qty { get; set; }
    }
}
