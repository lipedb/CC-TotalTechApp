using System.Windows.Input;
using Xamarin.Forms;

namespace TotalTechApp.Controls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
            nameof(IconSource),
            typeof(string),
            typeof(CustomEntry),
            default(string));

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomEntry),
            default(Color),
            BindingMode.TwoWay);

        public static readonly BindableProperty IconHeightProperty =
            BindableProperty.Create(nameof(IconHeight), typeof(int), typeof(CustomEntry), 16);

        public static readonly BindableProperty IconWidthProperty =
            BindableProperty.Create(nameof(IconWidth), typeof(int), typeof(CustomEntry), 16);

        public static readonly BindableProperty IconAlignmentProperty =
            BindableProperty.Create(nameof(IconAlignment), typeof(IconAlignment), typeof(CustomEntry), IconAlignment.Left);

        public static readonly BindableProperty UnfocusedCommandProperty = BindableProperty.Create(
            nameof(UnfocusedCommand),
            typeof(ICommand),
            typeof(CustomEntry),
            null);

        public static readonly BindableProperty FocusedCommandProperty = BindableProperty.Create(
            nameof(FocusedCommand),
            typeof(ICommand),
            typeof(CustomEntry),
            null);

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }


        public IconAlignment IconAlignment
        {
            get { return (IconAlignment)GetValue(IconAlignmentProperty); }
            set { SetValue(IconAlignmentProperty, value); }
        }


        public string IconSource
        {
            get => GetValue(IconSourceProperty) as string;
            set => SetValue(IconSourceProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Command<FocusEventArgs> FocusedCommand
        {
            get => (Command<FocusEventArgs>)GetValue(FocusedCommandProperty);
            set => SetValue(FocusedCommandProperty, value);
        }

        public Command<FocusEventArgs> UnfocusedCommand
        {
            get => (Command<FocusEventArgs>)GetValue(UnfocusedCommandProperty);
            set => SetValue(UnfocusedCommandProperty, value);
        }

        public CustomEntry()
        {
            this.Unfocused += Handle_Unfocused;
            this.Focused += Handle_Focused; ;
        }

        void Handle_Unfocused(object sender, FocusEventArgs e)
        {
            UnfocusedCommand?.Execute(e);
        }

        void Handle_Focused(object sender, FocusEventArgs e)
        {
            FocusedCommand?.Execute(e);
        }

    }

    public enum IconAlignment
    {
        Left,
        Right
    }
}
