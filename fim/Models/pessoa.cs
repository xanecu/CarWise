//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fim.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pessoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pessoa()
        {
            this.clientes = new HashSet<cliente>();
            this.empregados = new HashSet<empregado>();
        }
    
        public int nif { get; set; }
        public string nome { get; set; }
        public Nullable<System.DateTime> datanasc { get; set; }
        public Nullable<int> idade { get; set; }
        public string rua { get; set; }
        public string codigopostal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cliente> clientes { get; set; }
        public virtual codigospostai codigospostai { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empregado> empregados { get; set; }
    }
}
