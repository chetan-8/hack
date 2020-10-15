using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo1.Core.EntityModel
{
    [Table("Customer", Schema = "dbo")]
    public class Customer
    {
      
        [Column(TypeName = "INT")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        [Required]
        public string CustomerName { get; set; }
    }
}
