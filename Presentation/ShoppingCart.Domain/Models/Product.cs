using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//Code first approach (One used last year was Database first


namespace ShoppingCart.Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID {get; set;}

        [Required]
        public string Name {get; set;}
        
        [Required]
        public double Price {get; set;}

        public string Description {get; set;}

        [Required]
        public virtual Category Category { get; set; }//This is the relationship

        [ForeignKey("Category")]
        public int CategoryId { get; set; } //This is the actual foreign key; this is a way how to address the relationship

        public string ImageUrl { get; set; }

        public int Stock { get; set; }

        [DefaultValue(false)]
        public bool Disable { get; set; } //to refresh database always run Add -Migration & update-Database

    }
}
