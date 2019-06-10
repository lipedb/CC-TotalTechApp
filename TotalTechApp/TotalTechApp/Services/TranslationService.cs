using System.Resources;

namespace TotalTechApp.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ResourceManager resourceManager;

        public TranslationService(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public string Get(string textIdentifier)
        {
            return resourceManager.GetString(textIdentifier);
        }
    }
}

