///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows.Media;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class SingleHueColorRampWorkspaceViewModel : ColorRampWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// Creates a color ramp that goes from the specified color to black.
        /// </summary>
        /// <param name="hue"></param>
        public SingleHueColorRampWorkspaceViewModel(Color hue) : this(true, hue) { }

        /// <summary>
        /// Creates a color ramp that goes from the specified color to black.
        /// </summary>
        /// <param name="isDynamic"></param>
        /// <param name="hue"></param>
        public SingleHueColorRampWorkspaceViewModel(bool isDynamic, Color hue) : base(isDynamic, new Color[2] { Colors.Black, hue }) { }

        #endregion
    }
}
