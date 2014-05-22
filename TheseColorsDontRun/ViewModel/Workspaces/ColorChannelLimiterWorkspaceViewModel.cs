///////////////////////////////////////
#region Namespace Directives

using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class ColorChannelLimiterWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const byte _maxChannelValue = 255;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Channel-related
        private byte _maxValue;
        private double _offset;

        #endregion

        ////////////////////////////////////////
        #region Channel-Related

        /// <summary>
        /// Indicates the maximum byte value for this channel.
        /// </summary>
        public byte MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
                OnPropertyChanged("MaxValue");
            }
        }

        /// <summary>
        /// Indicates and/or selects the offset from one end of the ramp to the other. (Range: 0-10)
        /// </summary>
        /// <remarks>
        /// The range of values for this member are set between 0 and 10 for data-binding compatibility with WPF slider controls.
        /// </remarks>
        public double Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                MaxValue = ScaleMaxChannelValue(value, _maxChannelValue);
                OnPropertyChanged("Offset");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// Creates a new workspace for adjusting RGB color channel.
        /// </summary>
        public ColorChannelLimiterWorkspaceViewModel()
        {
            Offset = 10.0;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private byte ScaleMaxChannelValue(double _value, byte _maxChannelValue)
        {
            return (byte)((_value / 10.0) * _maxChannelValue);
        }

        #endregion
    }
}
