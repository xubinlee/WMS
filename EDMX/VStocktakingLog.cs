namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VStocktakingLog")]
    public partial class VStocktakingLog
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string BillNo { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime BillDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid ID { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid HdID { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid DeptID { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string DeptCode { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid WarehouseID { get; set; }

        [StringLength(20)]
        public string WarehouseCode { get; set; }

        [StringLength(50)]
        public string WarehouseName { get; set; }

        [Key]
        [Column(Order = 7)]
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

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockQty { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CheckQty { get; set; }

        [Key]
        [Column(Order = 10)]
        public decimal Price { get; set; }

        [StringLength(20)]
        public string ScanBill { get; set; }

        [StringLength(20)]
        public string Checker { get; set; }

        public DateTime? CheckingDate { get; set; }
    }
}
