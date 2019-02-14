using System.Windows.Forms;

namespace AIBehaviours
{
    internal sealed class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
}