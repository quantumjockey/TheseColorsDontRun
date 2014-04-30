///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.ViewModel.Workspaces
{
    public class ColorRampWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Render-specific
        private LinearGradientBrush _brush;
        private GradientStopCollection _ramp;
        private double _sliderValue;

        #endregion

        ////////////////////////////////////////
        #region Properties

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

        public double SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                Refresh();
                OnPropertyChanged("SliderValue");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public ColorRampWorkspaceViewModel()
        {
            SliderValue = 5;
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ramp"></param>
        /// <param name="offset"></param>
        /// <param name="maxR"></param>
        /// <param name="maxG"></param>
        /// <param name="maxB"></param>
        /// <param name="alphaEnabled"></param>
        /// <returns></returns>
        public Color GetRelativeColor(GradientStopCollection ramp, double offset, int maxR, int maxG, int maxB, bool alphaEnabled)
        {
            double startBoundaryOffset = 0.0;
            double finishBoundaryOffset = 1.0;

            GradientStop startBoundary = new GradientStop();
            GradientStop finishBoundary = new GradientStop();

            foreach (GradientStop boundary in ramp)
            {
                if (boundary.Offset <= offset && boundary.Offset > startBoundaryOffset)
                {
                    startBoundary = boundary;
                }
                if (boundary.Offset > offset && boundary.Offset <= finishBoundaryOffset)
                {
                    finishBoundary = boundary;
                    break;
                }
            }

            var color = Color.FromScRgb(
                (alphaEnabled) ? CalculateChannelValue(startBoundary, finishBoundary, "ScA", offset, 255) : (float)1.0,
                CalculateChannelValue(startBoundary, finishBoundary, "ScR", offset, maxR),
                CalculateChannelValue(startBoundary, finishBoundary, "ScG", offset, maxG),
                CalculateChannelValue(startBoundary, finishBoundary, "ScB", offset, maxB)
                );

            return color;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Refresh()
        {
            Ramp = CreateColorRamp(SliderValue);
            Brush = RefreshBrush(Brush);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GradientStopCollection CreateColorRamp(double offset)
        {
            double stopOne, stopTwo, stopThree, stopFour;

            stopOne = 0.0;
            stopFour = 1.0;
            stopTwo = (offset / 10.0) * 0.5;
            stopThree = 1.0 - stopTwo;

            List<GradientStop> stops = new List<GradientStop>();
            stops.Add(new GradientStop(Colors.White, stopOne));
            stops.Add(new GradientStop(Colors.Blue, stopTwo));
            stops.Add(new GradientStop(Colors.Red, stopThree));
            stops.Add(new GradientStop(Colors.Yellow, stopFour));
            return new GradientStopCollection(stops);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        private LinearGradientBrush RefreshBrush(LinearGradientBrush brush)
        {
            if (brush == null)
            {
                brush = new LinearGradientBrush();
            }
            brush.GradientStops.Clear();
            foreach (GradientStop item in _ramp)
            {
                brush.GradientStops.Add(item);
            }
            brush.StartPoint = new System.Windows.Point(0, 1);
            brush.EndPoint = new System.Windows.Point(0, 0);
            return brush;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_before"></param>
        /// <param name="_after"></param>
        /// <param name="_colorComponent"></param>
        /// <param name="_offset"></param>
        /// <param name="_maxValue"></param>
        /// <returns></returns>
        private float CalculateChannelValue(GradientStop _before, GradientStop _after, string _colorComponent, double _offset, int _maxValue)
        {
            double afterOffset = _after.Offset;
            double beforeOffset = _before.Offset;

            float max = (float)_maxValue / (float)255;

            float afterColorChannelValue = GetScRgbChannelValue(_after.Color, _colorComponent);
            float beforeColorChannelValue = GetScRgbChannelValue(_before.Color, _colorComponent);

            double scaleFactor = (_offset - beforeOffset) / (afterOffset - beforeOffset);

            double channelRange = afterColorChannelValue - beforeColorChannelValue;

            float newChannel = (float)(scaleFactor * channelRange);

            float result = (float)(newChannel + beforeColorChannelValue);

            return (result < max) ? result : max;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="_channelName"></param>
        /// <returns></returns>
        private float GetScRgbChannelValue(Color _color, string _channelName)
        {
            float channelValue = (float)0.0;

            PropertyInfo[] properties = (typeof(Color)).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == _channelName)
                {
                    channelValue = (float)properties[i].GetValue(_color, null);
                    break;
                }
            }

            return channelValue;
        }

        #endregion
    }
}
