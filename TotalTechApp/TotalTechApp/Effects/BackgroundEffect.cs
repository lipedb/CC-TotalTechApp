using TotalTechApp.Extensions;
using Xamarin.Forms;

namespace TotalTechApp.Effects
{
    public class BackgroundEffect : RoutingEffect
    {
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.CreateAttached(
            "BorderWidth",
            typeof(double),
            typeof(BackgroundEffect),
            0.0,
            propertyChanged: BackgroundPropertyChanged
        );

        public static readonly BindableProperty BorderColorProperty = BindableProperty.CreateAttached(
            "BorderColor",
            typeof(Color),
            typeof(BackgroundEffect),
            Color.Gray,
            propertyChanged: BackgroundPropertyChanged
        );

        public static readonly BindableProperty RadiusProperty = BindableProperty.CreateAttached(
            "Radius",
            typeof(double),
            typeof(BackgroundEffect),
            0.0,
            propertyChanged: BackgroundPropertyChanged
        );

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.CreateAttached(
            "BackgroundColor",
            typeof(Color),
            typeof(BackgroundEffect),
            Color.White,
            propertyChanged: BackgroundPropertyChanged
        );

        public static readonly BindableProperty RoundCornersProperty = BindableProperty.CreateAttached(
            "RoundCorners",
            typeof(RoundCorners),
            typeof(BackgroundEffect),
            RoundCorners.AllCorners,
            propertyChanged: BackgroundPropertyChanged
        );

        public static double GetBorderWidth(BindableObject bindable)
        {
            return (double)bindable.GetValue(BorderWidthProperty);
        }

        public static void SetBorderWidth(BindableObject bindable, double width)
        {
            bindable.SetValue(BorderWidthProperty, width);
        }

        public static Color GetBorderColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(BorderColorProperty);
        }

        public static void SetBorderColor(BindableObject bindable, Color color)
        {
            bindable.SetValue(BorderColorProperty, color);
        }

        public static double GetRadius(BindableObject bindable)
        {
            return (double)bindable.GetValue(RadiusProperty);
        }

        public static void SetRadius(BindableObject bindable, double radius)
        {
            bindable.SetValue(RadiusProperty, radius);
        }

        public static Color GetBackgroundColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(BackgroundColorProperty);
        }

        public static void SetBackgroundColor(BindableObject bindable, Color color)
        {
            bindable.SetValue(BackgroundColorProperty, color);
        }

        public static RoundCorners GetRoundCorners(BindableObject bindable)
        {
            return (RoundCorners)bindable.GetValue(RoundCornersProperty);
        }

        public static void SetRoundCorners(BindableObject bindable, RoundCorners roundCorners)
        {
            bindable.SetValue(RoundCornersProperty, roundCorners);
        }

        public BackgroundEffect()
            : base($"{nameof(TotalTechApp)}.{nameof(BackgroundEffect)}")
        {
        }

        private static void BackgroundPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = bindable as VisualElement;

            if (element == null)
            {
                return;
            }

            if (GetBorderWidth(bindable) > 0 || GetRadius(bindable) > 0)
            {
                element.AddEffect<BackgroundEffect>();
            }
            else
            {
                element.RemoveEffect<BackgroundEffect>();
            }
        }
    }

    public enum RoundCorners
    {
        TopLeft = 0x1,
        TopRight = 0x2,
        BottomLeft = 0x4,
        BottomRight = 0x8,
        AllCorners = 0xFFFFFF
    }
}
