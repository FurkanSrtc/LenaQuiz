using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lena.Models.ViewModels
{
    public class FormViewModel
    {
       public List<FieldsViewModel> Fields { get; set; }


        public Forms Form { get; set; }
    }



    public class FieldsViewModel
    {
        public Nullable<bool> Required { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}