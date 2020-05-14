namespace MaskAutoCleaner.Dal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mac_log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid pkid { get; set; }

        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pkidx { get; set; }*/

        [StringLength(32)]
        public string log_level { get; set; }
        public string claim_time { get; set; }
        public string sender { get; set; }
        public string message { get; set; }
        public string description { get; set; }


    }
}

