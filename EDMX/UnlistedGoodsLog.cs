namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnlistedGoodsLog")]
    public partial class UnlistedGoodsLog
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

        public decimal Price { get; set; }

        public int StockQty { get; set; }

        public decimal StockAMT { get; set; }

        public int UnlistedQty { get; set; }

        public decimal UnlistedAMT { get; set; }

        public int ScrapQty { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
