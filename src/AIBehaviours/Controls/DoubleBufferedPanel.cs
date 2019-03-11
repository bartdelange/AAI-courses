using System.Windows.Forms;

namespace AIBehaviours.Controls
{
    public sealed class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
}