//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace V2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Album
    {
        public Album()
        {
            this.Achat = new HashSet<Achat>();
            this.Version = new HashSet<Version>();
        }
    
        public int AlbumID { get; set; }
        public Nullable<System.DateTime> DateCreation { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Prix { get; set; }
        public string Nom { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<Achat> Achat { get; set; }
        public virtual ICollection<Version> Version { get; set; }
    }
}
