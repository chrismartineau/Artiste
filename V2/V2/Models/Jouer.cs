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
    
    public partial class Jouer
    {
        public int JouerID { get; set; }
        public Nullable<int> ArtisteID { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        public Nullable<int> VersionID { get; set; }
    
        public virtual Artiste Artiste { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual Version Version { get; set; }
    }
}
