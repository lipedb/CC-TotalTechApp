using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TotalTechApp.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "TotalTechApp.Resources.Strings";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            return resourceManager.GetString(Text);
        }

        public static ResourceManager ResourceManager
        {
            get { return new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly); }
        }
    }
}


