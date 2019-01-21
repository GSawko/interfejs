using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class RichTextBoxExtension
    {
        public static string TextOrDefault(this RichTextBox richTextBox)
        {
            return string.IsNullOrWhiteSpace(richTextBox.Text) ? null : richTextBox.Text;
        }
    }
}
