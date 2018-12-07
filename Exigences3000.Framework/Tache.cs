//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exigences3000.Framework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tache
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tache()
        {
            this.Exigence = new HashSet<Exigence>();
        }
    
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int id_responsable { get; set; }
        public Nullable<System.DateTime> date_debut { get; set; }
        public Nullable<int> id_jalon { get; set; }
        public Nullable<System.DateTime> date_fin { get; set; }
        public Nullable<System.DateTime> date__debut_prevue { get; set; }
    
        public virtual Responsable Responsable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exigence> Exigence { get; set; }
    }
}
