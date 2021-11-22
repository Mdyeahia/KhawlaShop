using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Entities
{
    public class Category:BaseEntities
    {
        public virtual List<Product> Products { get; set; }
        public List<CategoryPicture> CategoryPictures { get; set; }

    }
}
