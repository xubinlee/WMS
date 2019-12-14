namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StocktakingLogDtl")]
    public partial class StocktakingLogDtl
    {
        public Guid ID { get; set; }

        public Guid HdID { get; set; }

        public Guid DeptID { get; set; }

        [Required]
        [StringLength(20)]
        public string DeptCode { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        public Guid WarehouseID { get; set; }

        [StringLength(20)]
        public string WarehouseCode { get; set; }

        [StringLength(50)]
        public string WarehouseName { get; set; }

        [Required]
        [StringLength(20)]
        public string GoodsCode { get; set; }

        [StringLength(50)]
        public string GoodsName { get; set; }

        [StringLength(20)]
        public string Barcode { get; set; }

        [StringLength(30)]
        public string SPEC { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        public int StockQty { get; set; }

        public int CheckQty { get; set; }

        public decimal Price { get; set; }

        [StringLength(20)]
        public string ScanBill { get; set; }

        [StringLength(20)]
        public string Checker { get; set; }

        public DateTime? CheckingDate { get; set; }
    }
}
