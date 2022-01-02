using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
