//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookPages
    {
        public int ItemId { get; set; }
        public int BookPage { get; set; }
        public string Text { get; set; }
    
        public virtual Items Items { get; set; }
    }
}
