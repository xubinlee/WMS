namespace EDMX
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserID { get; set; }

        public int ParentID { get; set; }

        public int SerialNo { get; set; }

        [Required]
        [StringLength(20)]
        public string Caption { get; set; }

        public bool CheckBoxState { get; set; }
    }
}
