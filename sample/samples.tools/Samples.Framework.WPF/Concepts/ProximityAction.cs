using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Samples.Framework.WPF
{
    public class ProximityAction : ContentControl
    {
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty TargetUsersItemsSourcesProperty;
        public static readonly DependencyProperty TargetGroupsItemsSourcesProperty;
        static ProximityAction()
        {
            var targetType = typeof(ProximityAction);
            DefaultStyleKeyProperty.OverrideMetadata(targetType, new FrameworkPropertyMetadata(targetType));
            CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), targetType, new PropertyMetadata(null));
            TargetUsersItemsSourcesProperty = DependencyProperty.Register(nameof(TargetUsersItemsSources), typeof(IEnumerable<SelectedItem>), targetType, new PropertyMetadata(null));
            TargetGroupsItemsSourcesProperty= DependencyProperty.Register(nameof(TargetGroupsItemsSources), typeof(IEnumerable<SelectedItem>), targetType, new PropertyMetadata(null));
        }


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IEnumerable<SelectedItem> TargetUsersItemsSources
        {
            get
            {
                return (IEnumerable<SelectedItem>)GetValue(TargetUsersItemsSourcesProperty);
            }
            set
            {
                SetValue(TargetUsersItemsSourcesProperty, value);
            }
        }

        public IEnumerable<SelectedItem> TargetGroupsItemsSources
        {
            get { return (IEnumerable<SelectedItem>)GetValue(TargetGroupsItemsSourcesProperty); }
            set { SetValue(TargetGroupsItemsSourcesProperty, value); }
        }

    }
    public class SelectedItem : ViewModelBase
    {
        private bool _IsSelected;
        public SelectedItem(string name, object data)
        {
            Name = name;
            Data = data;
        }
        public string Name { get; }
        public object Data { get; }


        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    base.Notify();
                }
            }
        }
    }
}
