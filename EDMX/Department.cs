namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(20)]
        public string StationCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Province { get; set; }

        [StringLength(10)]
        public string City { get; set; }

        [StringLength(10)]
        public string County { get; set; }

        [StringLength(20)]
        public string Contacts { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string BWAddress { get; set; }

        [StringLength(50)]
        public string ContactTel { get; set; }

        [StringLength(50)]
        public string ContactCellPhone { get; set; }

        [StringLength(50)]
        public string QQ { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        [StringLength(10)]
        public string Level { get; set; }

        [StringLength(20)]
        public string Category { get; set; }

        public int Status { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        [Column(TypeName = "ntext")]
        public string Remark { get; set; }

        public bool IsDel { get; set; }
    }
}
