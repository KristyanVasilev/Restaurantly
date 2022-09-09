namespace Restarauntly.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetCategories();
    }
}
