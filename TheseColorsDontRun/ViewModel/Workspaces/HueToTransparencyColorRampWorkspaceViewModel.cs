///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows.Media;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class HueToTransparencyColorRampWorkspaceViewModel : ColorRampWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// Creates a color ramp that goes from the specified color to transparency.
        /// </summary>
        /// <param name="hue"></param>
        public HueToTransparencyColorRampWorkspaceViewModel(Color hue) : this(true, hue) { }

        /// <summary>
        /// Creates a color ramp that goes from the specified color to transparency.
        /// </summary>
        /// <param name="isDynamic"></param>
        /// <param name="hue"></param>
        public HueToTransparencyColorRampWorkspaceViewModel(bool isDynamic, Color hue) : base(isDynamic, new Color[2] { Colors.Transparent, hue }) { }

        #endregion
    }
}
