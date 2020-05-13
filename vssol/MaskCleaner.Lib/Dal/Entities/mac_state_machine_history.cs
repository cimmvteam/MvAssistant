namespace MaskAutoCleaner.Dal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mac_state_machine_history
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid pkid { get; set; }

        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pkidx { get; set; }*/

        [StringLength(32)]
        public string state_machine_name{ get; set; }
        public DateTime? datetime { get; set; }

        [Column(TypeName = "xml")]
        public string xml { get; set; }


    }
}
