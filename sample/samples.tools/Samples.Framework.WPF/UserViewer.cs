using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Samples.Framework.WPF
{
    public class UserViewer : ContentControl
    {
        public static readonly DependencyProperty UserNameProperty;
        public static readonly DependencyProperty UserImageSourceProperty;
        static UserViewer()
        {
            var targetType = typeof(UserViewer);
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserViewer), new FrameworkPropertyMetadata(targetType));
            UserImageSourceProperty = DependencyProperty.Register(nameof(UserImageSource), typeof(ImageSource), targetType, new PropertyMetadata(null));
            UserNameProperty = DependencyProperty.Register(nameof(UserName), typeof(string), targetType, new PropertyMetadata(string.Empty));
        }
         

        public ImageSource UserImageSource
        {
            get { return (ImageSource)GetValue(UserImageSourceProperty); }
            set { SetValue(UserImageSourceProperty, value); }
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
    }
}
