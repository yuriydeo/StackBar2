using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackBarControlLib.Themes
{
    public partial class ResourceDictionary
    {
        public static void StackBarRowDoubleClickHandler(object sender, MouseButtonEventArgs e)
        {
            StackBarRowItemControl row = (StackBarRowItemControl)sender;
            row.IsPreviewMode = !row.IsPreviewMode;
        }
    }
}
