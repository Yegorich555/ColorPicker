using System.Windows.Forms;

namespace ColorPicker
{
    class KeyBoardHandler : IMessageFilter
    {
        const int WM_KEYDOWN = 0x100;
        public event KeyEventHandler KeyDown;

        public KeyBoardHandler()
        {
            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                var args = new KeyEventArgs((Keys)(int)m.WParam);
                KeyDown?.Invoke(this, args);
                return args.Handled;
            }

            return false;
        }
    }

}
