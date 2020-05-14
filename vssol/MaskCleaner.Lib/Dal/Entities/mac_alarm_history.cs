namespace MaskAutoCleaner.Dal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mac_alarm_history
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid pkid { get; set; }

        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pkidx { get; set; }*/

        [StringLength(32)]
        public string alarm_id{ get; set; }
        public string alarm_description { get; set; }
        public DateTime? datetime { get; set; }

        [Column(TypeName = "xml")]
        public string xml { get; set; }


    }
}
