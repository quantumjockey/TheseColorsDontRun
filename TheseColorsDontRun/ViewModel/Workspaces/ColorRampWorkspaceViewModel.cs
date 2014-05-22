///////////////////////////////////////
#region Namespace Directives

using System.Windows.Media;
using TheseColorsDontRun.Extensions;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class ColorRampWorkspaceViewModel : WorkspaceViewModel, IColorRampWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Render-specific
        private LinearGradientBrush _brush;
        private bool _isDynamic;
        protected double _offset;
        private GradientStopCollection _ramp;
        protected Color[] _rampHues;

        #endregion

        ////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Object for painting ramp gradients on interface controls.
        /// </summary>
        public LinearGradientBrush Brush
        {
            get
            {
                return _brush;
            }
            set
            {
                _brush = value;
                OnPropertyChanged("Brush");
            }
        }

        /// <summary>
        /// Indicates and/or selects the offset from one end of the ramp to the other. (Range: 0-10)
        /// </summary>
        /// <remarks>
        /// The range of values for this member are set between 0 and 10 for data-binding compatibility with WPF slider controls.
        /// </remarks>
        public virtual double Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                Refresh(255, 255, 255);
                OnPropertyChanged("Offset");
            }
        }

        /// <summary>
        /// Contains the range of color gradients that can be selected from.
        /// </summary>
        public GradientStopCollection Ramp
        {
            get
            {
                return _ramp;
            }
            set
            {
                _ramp = value;
                OnPropertyChanged("Ramp");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// Creates a new Color Ramp with fixed channel maxima.
        /// </summary>
        /// <param name="isDynamic">Indicates whether or not gradients on the ramp change with the offset.</param>
        /// <param name="rampHues">An array containing the colors for each gradient stop on the color ramp.</param>
        public ColorRampWorkspaceViewModel(bool isDynamic, Color[] rampHues)
        {
            _isDynamic = isDynamic;
            _rampHues = rampHues;
            Offset = 5;
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// Changes the ramp gradient.
        /// </summary>
        /// <param name="rampHues">An array of colors that will be used to construct the ramp.</param>
        public virtual void ChangeRampGradient(Color[] rampHues)
        {
            _rampHues = rampHues;
            Refresh(255, 255, 255);
        }

        /// <summary>
        /// Refresh the contents of the color ramp based on the channel maxima.
        /// </summary>
        public void Refresh(byte maxR, byte maxB, byte maxG)
        {
            if (Offset != 0)
            {
                Ramp = Ramp.CreateColorRamp(_isDynamic, Offset, _rampHues, maxR, maxB, maxG);
                Brush = Brush.Refresh(Ramp);
            }
        }

        #endregion
    }
}
