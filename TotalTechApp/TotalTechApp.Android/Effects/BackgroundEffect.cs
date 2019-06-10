using System.ComponentModel;
using Android.Graphics.Drawables;
using TotalTechApp.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("TotalTechApp")]
[assembly: ExportEffect(typeof(TotalTechApp.Droid.Effects.BackgroundEffect), nameof(BackgroundEffect))]
namespace TotalTechApp.Droid.Effects
{
    public class BackgroundEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        UpdateBorder();
    }

    protected override void OnDetached()
    {
        RemoveBorder();
    }

    protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
    {
        base.OnElementPropertyChanged(args);

        if (args.PropertyName == TotalTechApp.Effects.BackgroundEffect.BackgroundColorProperty.PropertyName ||
            (args.PropertyName == TotalTechApp.Effects.BackgroundEffect.BorderColorProperty.PropertyName) ||
            (args.PropertyName == TotalTechApp.Effects.BackgroundEffect.BorderWidthProperty.PropertyName) ||
            (args.PropertyName == TotalTechApp.Effects.BackgroundEffect.RoundCornersProperty.PropertyName) ||
            (args.PropertyName == TotalTechApp.Effects.BackgroundEffect.RadiusProperty.PropertyName))
        {
            UpdateBorder();
        }
    }

    private void UpdateBorder()
    {
        var view = Control ?? Container;
        if (view == null)
            return;

        var cornerRadius = view.Context.ToPixels(TotalTechApp.Effects.BackgroundEffect.GetRadius(Element));
        var roundCorners = TotalTechApp.Effects.BackgroundEffect.GetRoundCorners(Element);
        var strokeWidth = (int)view.Context.ToPixels(TotalTechApp.Effects.BackgroundEffect.GetBorderWidth(Element));
        var strokeColor = TotalTechApp.Effects.BackgroundEffect.GetBorderColor(Element).ToAndroid();
        var color = TotalTechApp.Effects.BackgroundEffect.GetBackgroundColor(Element).ToAndroid();

        var drawable = new GradientDrawable();
        drawable.SetStroke(strokeWidth, strokeColor);
        drawable.SetCornerRadii(GetRadii(roundCorners, cornerRadius));
        drawable.SetColor(color);
        view.SetBackground(drawable);
    }

    private float[] GetRadii(RoundCorners roundCorners, float radius)
    {
        var corners = new float[8];
        if (roundCorners.HasFlag(RoundCorners.TopLeft))
        {
            corners[0] = radius;
            corners[1] = radius;
        }
        if (roundCorners.HasFlag(RoundCorners.TopRight))
        {
            corners[2] = radius;
            corners[3] = radius;
        }
        if (roundCorners.HasFlag(RoundCorners.BottomRight))
        {
            corners[4] = radius;
            corners[5] = radius;
        }
        if (roundCorners.HasFlag(RoundCorners.BottomLeft))
        {
            corners[6] = radius;
            corners[7] = radius;
        }
        return corners;
    }

    private void RemoveBorder()
    {
        var view = Control ?? Container;
        if (view != null)
        {
            view.SetBackground(null);
        }
    }
}
}
