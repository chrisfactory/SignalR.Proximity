using System.Windows;
using System.Windows.Controls;

namespace Samples.Framework.WPF
{
    public class UserMessage : Control
    {
        static UserMessage()
        {
            var targetType = typeof(UserMessage);
            DefaultStyleKeyProperty.OverrideMetadata(targetType, new FrameworkPropertyMetadata(targetType));
        }


    }
}
