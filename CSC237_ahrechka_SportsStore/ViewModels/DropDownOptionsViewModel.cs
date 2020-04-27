using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.ViewModels
{
    public class DropDownOptionsViewModel
    {
        public Dictionary<string, string> Items { get; set; }
        public string SelectedValue { get; set; }
        public string DefaultValue  { get; set; }
        public string DefaultText { get; set; }
        public bool HasDefault => !string.IsNullOrEmpty(DefaultText);
    }
}
