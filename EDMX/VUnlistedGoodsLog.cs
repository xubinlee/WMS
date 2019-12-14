namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VUnlistedGoodsLog")]
    public partial class VUnlistedGoodsLog
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

        [StringLength(20)]
        public string Category { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(20)]
        public string GoodsCode { get; set; }

        [StringLength(50)]
        public string GoodsName { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal Price { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockQty { get; set; }

        [Key]
        [Column(Order = 9)]
        public decimal StockAMT { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UnlistedQty { get; set; }

        [Key]
        [Column(Order = 11)]
        public decimal UnlistedAMT { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScrapQty { get; set; }

        [StringLength(50)]
        public string Reason { get; set; }
    }
}
