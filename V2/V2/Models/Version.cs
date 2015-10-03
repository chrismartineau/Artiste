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
    using System.Web.Mvc;
    
    public partial class Version
    {
        public Version()
        {
            this.Achat = new HashSet<Achat>();
            this.Jouer = new HashSet<Jouer>();
        }
    
        public int VersionID { get; set; }
        public int ChansonID { get; set; }
        public int AlbumID { get; set; }
        public string Commentaire { get; set; }
        public Nullable<System.DateTime> DateCreation { get; set; }
        [AllowHtml]
        public string Demo { get; set; }
        public Nullable<int> Duree { get; set; }
        public Nullable<int> NbEcoutes { get; set; }
        public Nullable<decimal> Prix { get; set; }
        public Nullable<bool> Visible { get; set; }
    
        public virtual ICollection<Achat> Achat { get; set; }
        public virtual Album Album { get; set; }
        public virtual Chanson Chanson { get; set; }
        public virtual ICollection<Jouer> Jouer { get; set; }
    }
}
