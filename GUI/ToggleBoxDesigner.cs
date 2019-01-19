using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ToggleBoxDesigner : ParentControlDesigner
    {

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            if (this.Control is ToggleBox)
            {
                this.EnableDesignMode(
                   ((ToggleBox)this.Control).WorkingArea, "WorkingArea");
            }
        }


    }
}
