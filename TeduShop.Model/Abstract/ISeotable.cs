using System;
using System.Collections.Generic;
using System.Linq;[MaxLength(250)]
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Abstract
{
    interface ISeotable
    {
        string MetaKeyword { set; get; }
        string MetaDiscription { set; get; }

    }
}
