using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views.InputMethods;
using TotalTechApp.Controls;
using TotalTechApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Resource;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace TotalTechApp.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry element;
        public CustomEntryRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (CustomEntry)this.Element;

            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.IconSource))
            {
                switch (element.IconAlignment)
                {
                    case IconAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.IconSource), null, null, null);
                        break;
                    case IconAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.IconSource), null);
                        break;
                }
            }
            editText.CompoundDrawablePadding = 25;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                editText.BackgroundTintList = ColorStateList.ValueOf(element.BorderColor.ToAndroid());
            }
            else
            {
                editText.Background.SetColorFilter(element.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }

            editText.SetHintTextColor(ColorStateList.ValueOf(element.BorderColor.ToAndroid()));

            SetReturnType(element);
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;
            int pixelWidth = (int)Context.ToPixels(element.IconWidth);
            int pixelHeight = (int)Context.ToPixels(element.IconHeight);
            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, pixelWidth, pixelHeight, true));
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            element = (CustomEntry)this.Element;
            var editText = this.Control;
            if (e.PropertyName == nameof(element.IsPassword))
            {
                ChangeFont();
                Control.SetSelection(Control.Text.Length);
            }
            if (e.PropertyName == nameof(element.BorderColor))
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    editText.BackgroundTintList = ColorStateList.ValueOf(element.BorderColor.ToAndroid());
                }
                else
                {
                    editText.Background.SetColorFilter(element.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                }

                editText.SetHintTextColor(ColorStateList.ValueOf(element.BorderColor.ToAndroid()));
            }
            if (e.PropertyName == nameof(element.IconSource))
            {
                if (!string.IsNullOrEmpty(element.IconSource))
                {
                    switch (element.IconAlignment)
                    {
                        case IconAlignment.Left:
                            editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.IconSource), null, null, null);
                            break;
                        case IconAlignment.Right:
                            editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.IconSource), null);
                            break;
                    }
                }
            }
        }

        private void SetReturnType(CustomEntry entry)
        {
            ReturnType type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel(ImeAction.Go.ToString(), ImeAction.Go);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel(ImeAction.Next.ToString(), ImeAction.Next);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel(ImeAction.Send.ToString(), ImeAction.Send);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel(ImeAction.Search.ToString(), ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel(ImeAction.Done.ToString(), ImeAction.Done);
                    break;
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (((CustomEntry)Element).IsPassword)
                ChangeFont();
            base.Draw(canvas);
        }

        protected void ChangeFont()
        {
            Typeface typeface = Typeface.CreateFromAsset(Context.Assets, "Forza-Medium.otf");
            var tfStyle = TypefaceStyle.Normal;
            Control.SetTypeface(typeface, tfStyle);
        }

    }
}