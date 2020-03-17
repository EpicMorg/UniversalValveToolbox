namespace UniversalValveToolbox.UI.View
{
    using System.Drawing;
    using System.Windows.Forms;

    public class IconComboBox : ComboBox
    {
        public IconComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < this.Items.Count)
            {
                var item = (IconComboBoxItem)this.Items[e.Index];

                e.Graphics.DrawIcon(item.Icon, e.Bounds.Left, e.Bounds.Top);
                e.Graphics.DrawString(item.Title, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Icon.Width, e.Bounds.Top + 2);
            }

            base.OnDrawItem(e);
        }
    }
}
