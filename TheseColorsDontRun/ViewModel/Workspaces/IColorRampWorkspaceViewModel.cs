///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows.Media;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public interface IColorRampWorkspaceViewModel
    {
        // Property signatures
        LinearGradientBrush Brush { get; set; }
        double Offset { get; set; }
        GradientStopCollection Ramp { get; set; }

        // Method signatures
        void ChangeRampGradient(Color[] rampHues);
        void Refresh(byte maxR, byte maxB, byte maxG);
    }
}
