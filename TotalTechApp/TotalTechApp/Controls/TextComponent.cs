using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace TotalTechApp.Controls
{
    public class TextComponent : BindableObject
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(TextComponent),
                                    default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
