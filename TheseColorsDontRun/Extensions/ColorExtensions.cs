///////////////////////////////////////
#region Namespace Directives

using System;
using System.Reflection;
using System.Windows.Media;

#endregion
///////////////////////////////////////

namespace TheseColorsDontRun.Extensions
{
    public static class ColorExtensions
    {
        ////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// Retrieves the floating point ScRGB value from the specified color channel.
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="_channelName"></param>
        /// <returns>ScRGB channel value between 0.0 and 1.0</returns>
        public static float GetScRgbChannelValue(this Color _color, string _channelName)
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
