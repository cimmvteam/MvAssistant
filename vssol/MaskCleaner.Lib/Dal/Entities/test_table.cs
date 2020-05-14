namespace MaskAutoCleaner.Dal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestTable")]
    public partial class test_table
    {
        [Key]
        public Guid pkid { get; set; }

        public int? test1 { get; set; }

        [StringLength(50)]
        public string varchar50 { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        public string varchar_max { get; set; }

        public decimal? decimal_18_0 { get; set; }

        [StringLength(10)]
        public string nchar10 { get; set; }

        [Column(TypeName = "xml")]
        public string test7 { get; set; }
    }
}
