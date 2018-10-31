using System.Collections.Generic;
using SampleAspNet.Models;

namespace SampleAspNet.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }
        public int ProductIdToDelete { get; set; }
    }
}
