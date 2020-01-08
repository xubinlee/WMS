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

        [StringLength(50)]
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
        public int ReturnedQty { get; set; }

        [Key]
        [Column(Order = 17)]
        public decimal ReturnedAMT { get; set; }

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
        public int NonArrivalQty { get; set; }

        [Key]
        [Column(Order = 21)]
        public decimal NonArrivalAMT { get; set; }

        [Key]
        [Column(Order = 22)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExtraPresellQty { get; set; }

        [Key]
        [Column(Order = 23)]
        public decimal ExtraPresellAMT { get; set; }

        [Key]
        [Column(Order = 24)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExtraSoldQty { get; set; }

        [Key]
        [Column(Order = 25)]
        public decimal ExtraSoldAMT { get; set; }

        [Key]
        [Column(Order = 26)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IntraPresellQty { get; set; }

        [Key]
        [Column(Order = 27)]
        public decimal IntraPresellAMT { get; set; }

        [Key]
        [Column(Order = 28)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IntraSoldQty { get; set; }

        [Key]
        [Column(Order = 29)]
        public decimal IntraSoldAMT { get; set; }

        [Key]
        [Column(Order = 30)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NonChargeOffQty { get; set; }

        [Key]
        [Column(Order = 31)]
        public decimal NonChargeOffAMT { get; set; }

        [Key]
        [Column(Order = 32)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NonRecordedQty { get; set; }

        [Key]
        [Column(Order = 33)]
        public decimal NonRecordedAMT { get; set; }

        [Key]
        [Column(Order = 34)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupBuyingQty { get; set; }

        [Key]
        [Column(Order = 35)]
        public decimal GroupBuyingAMT { get; set; }

        [Key]
        [Column(Order = 36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DisasterQty { get; set; }

        [Key]
        [Column(Order = 37)]
        public decimal DisasterAMT { get; set; }

        [Key]
        [Column(Order = 38)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OtherQty { get; set; }

        [Key]
        [Column(Order = 39)]
        public decimal OtherAMT { get; set; }

        [Key]
        [Column(Order = 40)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinalDiffQty { get; set; }

        [Key]
        [Column(Order = 41)]
        public decimal FinalDiffAMT { get; set; }

        [Key]
        [Column(Order = 42)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScrapQty { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
