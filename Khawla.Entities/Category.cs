using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Entities
{
    public class Category:BaseEntities
    {
        public bool IsFeatured { get; set; }
        public  List<Product> Products { get; set; }
        public  virtual List<CategoryPicture> CategoryPictures { get; set; }

    }
}
