namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Goods
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public Guid? GoodsTypeID { get; set; }

        public Guid? PackagingID { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        [StringLength(50)]
        public string SPEC { get; set; }

        public decimal Price { get; set; }

        public decimal PriceNoTax { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public decimal InPutVAT { get; set; }

        public decimal OutPutVAT { get; set; }

        public decimal UpperLimit { get; set; }

        public decimal LowerLimit { get; set; }

        [Column(TypeName = "image")]
        public byte[] Pic { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public decimal CavityNumber { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public bool IsDel { get; set; }
    }
}
