namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfitAndLoss")]
    public partial class ProfitAndLoss
    {
        public Guid ID { get; set; }

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

        public int CheckQty { get; set; }

        public decimal CheckAMT { get; set; }

        public int DiffQty { get; set; }

        public decimal DiffAMT { get; set; }

        public int TransitQty { get; set; }

        public decimal TransitAMT { get; set; }

        public int ReturnedQty { get; set; }

        public decimal ReturnedAMT { get; set; }

        public int NonInStoreQty { get; set; }

        public decimal NonInStoreAMT { get; set; }

        public int NonArrivalQty { get; set; }

        public decimal NonArrivalAMT { get; set; }

        public int ExtraPresellQty { get; set; }

        public decimal ExtraPresellAMT { get; set; }

        public int ExtraSoldQty { get; set; }

        public decimal ExtraSoldAMT { get; set; }

        public int IntraPresellQty { get; set; }

        public decimal IntraPresellAMT { get; set; }

        public int IntraSoldQty { get; set; }

        public decimal IntraSoldAMT { get; set; }

        public int NonChargeOffQty { get; set; }

        public decimal NonChargeOffAMT { get; set; }

        public int NonRecordedQty { get; set; }

        public decimal NonRecordedAMT { get; set; }

        public int GroupBuyingQty { get; set; }

        public decimal GroupBuyingAMT { get; set; }

        public int DisasterQty { get; set; }

        public decimal DisasterAMT { get; set; }

        public int OtherQty { get; set; }

        public decimal OtherAMT { get; set; }

        public int FinalDiffQty { get; set; }

        public decimal FinalDiffAMT { get; set; }

        public int ScrapQty { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
