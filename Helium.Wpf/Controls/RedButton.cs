using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Helium.Wpf.Controls
{
    public class RedButton : Button
    {
        static RedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RedButton), new FrameworkPropertyMetadata(typeof(RedButton)));
        }
    }
}
