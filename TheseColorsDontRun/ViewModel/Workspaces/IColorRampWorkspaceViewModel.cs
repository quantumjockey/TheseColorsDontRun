///////////////////////////////////////
#region Namespace Directives

using System.Windows.Media;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public interface IColorRampWorkspaceViewModel : IWorkspaceViewModel
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
