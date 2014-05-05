///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows.Media;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.Extensions
{
    public static class LinearGradientBrushExtensions
    {
        ////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// Refreshes a LinearGradientBrush's gradient collection in a manner that prevents data-bindings from
        /// breaking upon refresh.
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="_ramp">Collection of gradients to replace brush's current gradients.</param>
        public static LinearGradientBrush Refresh(this LinearGradientBrush brush, GradientStopCollection _ramp)
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

        #endregion
    }
}
