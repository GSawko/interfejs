using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    static class MaskedTextBoxExtension
    {
        public static string TextOrDefault(this MaskedTextBox maskedTextBox)
        {
            return string.IsNullOrWhiteSpace(maskedTextBox.Text) ? null : maskedTextBox.Text;
        }
    }
}
