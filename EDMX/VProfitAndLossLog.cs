namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VProfitAndLossLog")]
    public partial class VProfitAndLossLog
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
        public int CheckQty { get; set; }

        [Key]
        [Column(Order = 11)]
        public decimal CheckAMT { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiffQty { get; set; }

        [Key]
        [Column(Order = 13)]
        public decimal DiffAMT { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransitQty { get; set; }

        [Key]
        [Column(Order = 15)]
        public decimal TransitAMT { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NonArrivalQty { get; set; }

        [Key]
        [Column(Order = 17)]
        public decimal NonArrivalAMT { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NonInStoreQty { get; set; }

        [Key]
        [Column(Order = 19)]
        public decimal NonInStoreAMT { get; set; }

        [Key]
        [Column(Order = 20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoldQty { get; set; }

        [Key]
        [Column(Order = 21)]
        public decimal SoldAMT { get; set; }

        [Key]
        [Column(Order = 22)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NonChargeOffQty { get; set; }

        [Key]
        [Column(Order = 23)]
        public decimal NonChargeOffAMT { get; set; }

        [Key]
        [Column(Order = 24)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReturnedQty { get; set; }

        [Key]
        [Column(Order = 25)]
        public decimal ReturnedAMT { get; set; }

        [Key]
        [Column(Order = 26)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupBuyingQty { get; set; }

        [Key]
        [Column(Order = 27)]
        public decimal GroupBuyingAMT { get; set; }

        [Key]
        [Column(Order = 28)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OtherQty { get; set; }

        [Key]
        [Column(Order = 29)]
        public decimal OtherAMT { get; set; }

        [Key]
        [Column(Order = 30)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinalDiffQty { get; set; }

        [Key]
        [Column(Order = 31)]
        public decimal FinalDiffAMT { get; set; }

        [Key]
        [Column(Order = 32)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScrapQty { get; set; }

        [StringLength(50)]
        public string Reason { get; set; }
    }
}
