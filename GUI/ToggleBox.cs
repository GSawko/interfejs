using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace GUI
{

    /// <span class="code-SummaryComment"><summary></span>
    /// A test Control to demonstrate allowing nested Controls
    /// to accept child controls at design time.
    /// <span class="code-SummaryComment"></summary></span>
    [
    Designer(typeof(ToggleBoxDesigner))
    ]
    public partial class ToggleBox : UserControl
    {
        private bool on;
        private String text;
        public String Caption
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                groupBox.Text = value;
            }
        }
        public bool On
        {
            get
            {
                return on;
            }
            set
            {
                AutoSize = value; Height = 25; on = value;
            }
        }
        public ToggleBox()
        {
            InitializeComponent();
            Caption = Name;
            On = false;
            on = true;
            Height = 100;
            toggle.Click += (object sender,EventArgs e) => { On = !On; };
        }

        [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        /// <span class="code-SummaryComment"><summary></span>
        /// This property is essential to allowing the designer to work and
        /// the DesignerSerializationVisibility Attribute (above) is essential
        /// in allowing serialization to take place at design time.
        /// <span class="code-SummaryComment"></summary></span>
        public Panel WorkingArea
        {
            get
            {
                groupBox.SendToBack();
                panel.Visible = false ;
                return this.panel;
            }
        }
    }
    /*[Designer(typeof(ToggleBoxDesigner))]
        public partial class ToggleBox : UserControl
        {
            Panel panel;

            [Description("The text of the toggleBox")]
            public string Text
            {
                get { return groupBox.Text; }
                set { groupBox.Text = value; }
            }
            public bool On
            {
                get { return on; }
                set { if (value) { AutoSize = true; } else { AutoSize = false; Height = 25; } on = value; }
            }
            private bool on;

            public ToggleBox()
            {
                panel = new Panel();
                panel.Dock = DockStyle.Fill;
                InitializeComponent();
                Text = "ToggleBox";
                Height = 100;
                button.Click += Toggle;
                Load += (object sender, EventArgs a) => { On = true; };

            }
            public void Toggle(object sender, EventArgs a)
            {
                AutoSize = !AutoSize;
                On = !On;
            }


            // define a property called "DropZone"
            [Category("Appearance")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public Panel DropZone
            {
                get { return this.panel; }
            }
        }
        */
}
