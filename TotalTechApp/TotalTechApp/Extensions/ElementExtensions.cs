using System;
using System.Linq;
using Xamarin.Forms;

namespace TotalTechApp.Extensions
{
    public static class ElementExtensions
    {
        public static T GetEffect<T>(this Element element) where T : RoutingEffect
        {
            return (T)element.Effects.FirstOrDefault(e => e is T);
        }

        public static bool AddEffect<T>(this Element element) where T : RoutingEffect
        {
            if (element.GetEffect<T>() == null)
            {
                element.Effects.Add(Activator.CreateInstance<T>());
                return true;
            }

            return false;
        }

        public static bool RemoveEffect<T>(this Element element) where T : RoutingEffect
        {
            T effect = element.GetEffect<T>();
            if (effect != null)
            {
                element.Effects.Remove(effect);
                return true;
            }

            return false;
        }
    }
}

