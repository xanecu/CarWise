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
    
    public partial class empregado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public empregado()
        {
            this.compras = new HashSet<compra>();
        }
    
        public int numemp { get; set; }
        public int nif { get; set; }
        public string posto { get; set; }
        public Nullable<decimal> salario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra> compras { get; set; }
        public virtual pessoa pessoa { get; set; }
    }
}
