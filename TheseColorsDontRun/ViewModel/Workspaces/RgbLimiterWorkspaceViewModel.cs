///////////////////////////////////////
#region Namespace Directives

using System;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class RgbLimiterWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Channel-Related

        /// <summary>
        /// Indicates the maximum red channel byte value. (Range: 0-255)
        /// </summary>
        public ColorChannelLimiterWorkspaceViewModel R { get; set; }

        /// <summary>
        /// Indicates the maximum green channel byte value. (Range: 0-255)
        /// </summary>
        public ColorChannelLimiterWorkspaceViewModel G { get; set; }

        /// <summary>
        /// Indicates the maximum blue channel byte value. (Range: 0-255)
        /// </summary>
        public ColorChannelLimiterWorkspaceViewModel B { get; set; }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// Creates a new workspace for adjusting RGB color values.
        /// </summary>
        public RgbLimiterWorkspaceViewModel()
        {
            R = new ColorChannelLimiterWorkspaceViewModel();
            G = new ColorChannelLimiterWorkspaceViewModel();
            B = new ColorChannelLimiterWorkspaceViewModel();

            R.Offset = G.Offset = B.Offset = 10;
        }

        #endregion
    }
}
