namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VInventoryLog")]
    public partial class VInventoryLog
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
        [StringLength(20)]
        public string GoodsCode { get; set; }

        [StringLength(50)]
        public string GoodsName { get; set; }

        [StringLength(20)]
        public string Barcode { get; set; }

        [StringLength(50)]
        public string SPEC { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Qty { get; set; }
    }
}
