using System.Windows;

namespace Client.Converters
{
    public class InvertedBooleanToVisibilityConverter : BoolToVisibilityConverter<Visibility>
    {
        public InvertedBooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        {
        }
    }
}
