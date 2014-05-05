///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using TheseColorsDontRun.Extensions;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public abstract class ColorRampWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Render-specific
        private LinearGradientBrush _brush;
        private bool _isDynamic;
        protected double _offset;
        private GradientStopCollection _ramp;
        private Color[] _rampHues;

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
        /// <param name="isDynamic">Indicates wther or not gradients on the ramp change with the offset.</param>
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
        /// Refresh the contents of the color ramp based on the channel maxima.
        /// </summary>
        public void Refresh(byte maxR, byte maxB, byte maxG)
        {
            if (Offset != 0)
            {
                Ramp = CreateColorRamp(Offset, maxR, maxB, maxG);
                Brush = Brush.Refresh(Ramp);
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private GradientStopCollection CreateColorRamp(double sliderControlOffset, byte maxR, byte maxB, byte maxG)
        {
            double adjOffset = sliderControlOffset / 10.0;
            int totalStops = _rampHues.Length;

            double stopOffset;

            GradientStop[] stops = new GradientStop[totalStops];
            for (int i = 0; i < totalStops; i++)
            {
                Color color = Color.FromRgb(
                    (_rampHues[i].R < maxR) ? _rampHues[i].R : maxR,
                    (_rampHues[i].G < maxR) ? _rampHues[i].G : maxG,
                    (_rampHues[i].B < maxR) ? _rampHues[i].B : maxB
                    );

                if (_isDynamic)
                {
                    stopOffset = (double)i * (1.0 / totalStops) * adjOffset * (totalStops / 2.0);
                }
                else
                {
                    stopOffset = (double)i * (1.0 / totalStops);
                }

                stops[i] = new GradientStop(color, stopOffset);
            }

            return new GradientStopCollection(stops);
        }

        #endregion
    }
}
