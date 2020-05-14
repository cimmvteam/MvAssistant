namespace MaskAutoCleaner.Dal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mac_recipe_param
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid pkid { get; set; }

        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pkidx { get; set; }*/

        [StringLength(32)]
        public string machine{ get; set; }
        public string side { get; set; }
        public string category { get; set; }
        public string param_name { get; set; }
        public long is_editable { get; set; }


        [StringLength(64)]
        public string is_no_data { get; set; }

    }
}
