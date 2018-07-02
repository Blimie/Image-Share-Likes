using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _180509.Data;

namespace _180509.Models
{
    public class ViewImageViewModel
    {
        public Image Image { get; set; }
        public bool HasPermissionToLike { get; set; }
    }
}