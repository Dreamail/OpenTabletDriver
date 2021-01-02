using System.Numerics;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Desktop.Conversion
{
    [PluginName("Wacom, VEIKK")]
    public class ConversionFactorAreaConverter : IAreaConverter
    {
        public virtual DeviceVendor Vendor => DeviceVendor.Wacom & DeviceVendor.VEIKK;

        public string Top => "Top";
        public string Bottom => "Bottom";
        public string Left => "Left";
        public string Right => "Right";

        protected double GetConversionFactor(TabletState tablet)
        {
            var digitizer = tablet.Digitizer;
            return digitizer.MaxX / digitizer.Width;
        }

        public Area Convert(TabletState tablet, double top, double bottom, double left, double right)
        {
            double conversionFactor = GetConversionFactor(tablet);
            var width = (right - left) / conversionFactor;
            var height = (bottom - top) / conversionFactor;
            var offsetX = (width / 2) + (left / conversionFactor);
            var offsetY = (height / 2) + (top / conversionFactor);

            return new Area((float)width, (float)height, new Vector2((float)offsetX, (float)offsetY), 0f);
        }
    }
}