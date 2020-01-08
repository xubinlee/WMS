namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InventoryLossLog")]
    public partial class InventoryLossLog
    {
        public Guid ID { get; set; }

        public Guid HdID { get; set; }

        public Guid DeptID { get; set; }

        [Required]
        [StringLength(20)]
        public string DeptCode { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(20)]
        public string GoodsCode { get; set; }

        [StringLength(50)]
        public string GoodsName { get; set; }

        public int StockQty { get; set; }

        public int CheckQty { get; set; }

        public int DiffQty { get; set; }

        public int LossQty { get; set; }

        public decimal Price { get; set; }

        public decimal StockAMT { get; set; }

        public decimal CheckAMT { get; set; }

        public decimal DiffAMT { get; set; }

        public decimal DiffRate { get; set; }

        public decimal PreCheckSellAMT { get; set; }

        public decimal LossAMT { get; set; }

        public decimal LossRate { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
