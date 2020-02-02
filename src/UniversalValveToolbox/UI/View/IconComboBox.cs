using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalValveToolbox.UI.View {
    public class IconComboBox: ComboBox {

        public IconComboBox() {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnDrawItem(DrawItemEventArgs e) {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < Items.Count) {
                var item = (IconComboBoxItem)Items[e.Index];

                e.Graphics.DrawIcon(item.Icon, e.Bounds.Left, e.Bounds.Top);
                e.Graphics.DrawString(item.Title, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Icon.Width, e.Bounds.Top + 2);
            }

            base.OnDrawItem(e);
        }


    }
}
