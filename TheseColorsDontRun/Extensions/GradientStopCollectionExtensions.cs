///////////////////////////////////////
#region Namespace Directives

using System;
using System.Reflection;
using System.Windows.Media;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.Extensions
{
    public static class GradientStopCollectionExtensions
    {
        ////////////////////////////////////////
        #region Public Methods


        /// <summary>
        /// Retrieves the color that most closely represents what would be found on the scale offset.
        /// </summary>
        /// <param name="ramp">The color scale the return value is derived from.</param>
        /// <param name="offset">The offset on the color scale for which a color is to be found..</param>
        /// <param name="maxR">The maximum R-channel value.</param>
        /// <param name="maxG">The maximum G-channel value.</param>
        /// <param name="maxB">The maximum B-channel value.</param>
        /// <param name="alphaEnabled">Whether or not value-matching for the alpha channel is enabled.</param>
        /// <returns>The color closest to the specified offset on the specified color scale.</returns>
        public static Color GetRelativeColor(this GradientStopCollection ramp, double offset, int maxR, int maxG, int maxB, bool alphaEnabled)
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

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        /// <summary>
        /// Calculates the ScRGB channel value associated with the specified offset on a color scale.
        /// </summary>
        /// <param name="_before">Gradient stop positioned before the offset on the color scale.</param>
        /// <param name="_after">Gradient stop positioned after the offset on the color scale.</param>
        /// <param name="_colorComponent"></param>
        /// <param name="_offset"></param>
        /// <param name="_maxValue"></param>
        /// <returns></returns>
        private static float CalculateChannelValue(GradientStop _before, GradientStop _after, string _colorComponent, double _offset, int _maxValue)
        {
            double afterOffset = _after.Offset;
            double beforeOffset = _before.Offset;

            float max = (float)_maxValue / (float)255;

            float afterColorChannelValue = _after.Color.GetScRgbChannelValue(_colorComponent);
            float beforeColorChannelValue = _before.Color.GetScRgbChannelValue(_colorComponent);

            double scaleFactor = (_offset - beforeOffset) / (afterOffset - beforeOffset);

            double channelRange = afterColorChannelValue - beforeColorChannelValue;

            float newChannel = (float)(scaleFactor * channelRange);

            float result = (float)(newChannel + beforeColorChannelValue);

            return (result < max) ? result : max;
        }

        #endregion

    }
}
