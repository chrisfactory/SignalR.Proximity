using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Samples.Framework.WPF
{
    public class UserViewer : ContentControl
    {
        public static readonly DependencyProperty UserNameProperty;
        public static readonly DependencyProperty UserImageSourceProperty;
        public static readonly DependencyProperty UserMessageProperty;
        static UserViewer()
        {
            var targetType = typeof(UserViewer);
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserViewer), new FrameworkPropertyMetadata(targetType));
            UserImageSourceProperty = DependencyProperty.Register(nameof(UserImageSource), typeof(ImageSource), targetType, new PropertyMetadata(null));
            UserNameProperty = DependencyProperty.Register(nameof(UserName), typeof(string), targetType, new PropertyMetadata(string.Empty));
            UserMessageProperty = DependencyProperty.Register(nameof(UserMessage), typeof(UserMessageViewModel), targetType, new PropertyMetadata(null));

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




        public UserMessageViewModel UserMessage
        {
            get { return (UserMessageViewModel)GetValue(UserMessageProperty); }
            set { SetValue(UserMessageProperty, value); }
        }
         

    }
}
