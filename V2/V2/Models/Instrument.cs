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
    
    public partial class Instrument
    {
        public Instrument()
        {
            this.Jouer = new HashSet<Jouer>();
        }
    
        public int InstrumentID { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<Jouer> Jouer { get; set; }
    }
}