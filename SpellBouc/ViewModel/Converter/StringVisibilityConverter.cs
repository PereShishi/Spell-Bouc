using System;
using System.Windows;
using System.Windows.Data;

namespace SpellBouc.ViewModel.Commands
{
    /* Converteur qui permet de convertir la visibilité d'un élément en string */
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringVisibilityConverter : IValueConverter
    {
        public const string Invert = "Invert";

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a Visibility.");

            string? stringValue = (string?)value;

            if (stringValue == "" || stringValue == null || stringValue == string.Empty)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
