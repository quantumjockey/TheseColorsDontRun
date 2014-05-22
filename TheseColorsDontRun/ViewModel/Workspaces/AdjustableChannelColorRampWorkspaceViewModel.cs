///////////////////////////////////////
#region Namespace Directives

using System.Windows.Media;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class AdjustableChannelColorRampWorkspaceViewModel : ColorRampWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Properties

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

        /// <summary>
        /// Indicates and/or selects the offset from one end of the ramp to the other. (Range: 0-10)
        /// </summary>
        /// <remarks>
        /// The range of values for this member are set between 0 and 10 for data-binding compatibility with WPF slider controls.
        /// </remarks>
        public override double Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                if (R != null && G != null && B != null)
                {
                    Refresh(R.MaxValue, G.MaxValue, B.MaxValue);
                }
                OnPropertyChanged("Offset");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// Creates a new Color Ramp with adjustable maxima for each color channel.
        /// </summary>
        /// <param name="rampHues">An array containing the colors for each gradient stop on the color ramp.</param>
        public AdjustableChannelColorRampWorkspaceViewModel(Color[] rampHues) : this(true, rampHues) { }

        /// <summary>
        /// Creates a new Color Ramp with adjustable maxima for each color channel.
        /// </summary>
        /// <param name="isDynamic">Indicates whether or not gradients on the ramp change with the offset.</param>
        /// <param name="rampHues">An array containing the colors for each gradient stop on the color ramp.</param>
        public AdjustableChannelColorRampWorkspaceViewModel(bool isDynamic, Color[] rampHues)
            : base(isDynamic, rampHues)
        {
            R = new ColorChannelLimiterWorkspaceViewModel();
            R.PropertyChanged += ChannelLimit_PropertyChanged;
            G = new ColorChannelLimiterWorkspaceViewModel();
            G.PropertyChanged += ChannelLimit_PropertyChanged;
            B = new ColorChannelLimiterWorkspaceViewModel();
            B.PropertyChanged += ChannelLimit_PropertyChanged;

            Offset = 5;
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// Changes the ramp gradient with the specified channel maxima;
        /// </summary>
        /// <param name="rampHues">An array of colors that will be used to construct the ramp.</param>
        public override void ChangeRampGradient(Color[] rampHues)
        {
            _rampHues = rampHues;
            Refresh(R.MaxValue, G.MaxValue, B.MaxValue);
        }

        #endregion

        ////////////////////////////////////////
        #region Events

        void ChannelLimit_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Refresh(R.MaxValue, G.MaxValue, B.MaxValue);
        }

        #endregion
    }
}
