//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportsClub.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class HighBoardClubMember
    {
        public int HighBoardMembers { get; set; }
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    
        public virtual Club Club { get; set; }
    }
}
