//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewsApiProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            this.Comment = new HashSet<Comment>();
        }
    
        public int NewsId { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public System.DateTime Date { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        public int ReadNumber { get; set; }
        public int MemberId { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Members Members { get; set; }
    }
}