using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
    }
}