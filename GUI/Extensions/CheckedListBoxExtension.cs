namespace System.Windows.Forms
{
    public static class CheckedListBoxExtension
    {
        public static void ClearItemChecked(this CheckedListBox checkedListBox)
        { 
            while (checkedListBox.CheckedIndices.Count > 0)
                checkedListBox.SetItemChecked(checkedListBox.CheckedIndices[0], false);
        }
    }
}
