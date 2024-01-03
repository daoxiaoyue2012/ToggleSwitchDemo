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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToggleSwitchDemo
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ToggleSwitchDemo"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ToggleSwitchDemo;assembly=ToggleSwitchDemo"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ToggleSwitch/>
    ///
    /// </summary>
    public class ToggleSwitch : CheckBox
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch()
        {
            Checked += ToggleSwitch_Checked;
            Unchecked += ToggleSwitch_Unchecked;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            AnimateButton(false);
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            AnimateButton(true);
        }

        private void AnimateButton(bool isChecked)
        {
            if (GetTemplateChild("button") is Border button)
            {
                Storyboard storyboard = new Storyboard();
                ThicknessAnimation animation = new ThicknessAnimation();
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animation.EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut };

                if (isChecked)
                {
                    animation.From = new Thickness(4, 0, 0, 0);
                    animation.To = new Thickness(ActualWidth - (ActualHeight - 8) - 4, 0, 0, 0);
                }
                else
                {
                    animation.To = new Thickness(4, 0, 0, 0);
                    animation.From = new Thickness(ActualWidth - (ActualHeight - 8) - 4, 0, 0, 0);
                }
                Storyboard.SetTarget(animation, button);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                storyboard.Children.Add(animation);

                if (isChecked)
                {
                    Resources.Remove("toLeft");
                    Resources["toRight"] = storyboard;
                }
                else
                {
                    Resources.Remove("toRight");
                    Resources["toLeft"] = storyboard;
                }
                storyboard.Begin(this);
            }
        }
    }
}
