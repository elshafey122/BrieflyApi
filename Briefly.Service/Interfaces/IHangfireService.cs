
namespace Briefly.Service.Interfaces
{
    public interface IHangfireService
    {
        Task DeleteOldRssArticle();
        Task CreateNewArticlesFromRsses();

        Task AiServiceArticlesGeneration();
        Task GenerateClusters();
    }
}