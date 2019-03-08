#region

using System.Windows.Forms;

#endregion

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