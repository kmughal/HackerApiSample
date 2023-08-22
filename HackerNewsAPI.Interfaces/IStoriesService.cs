using HackerNewsAPI.Models;

namespace HackerNewsAPI.Interfaces;

public interface IStoriesService
{
    Task<HackerNewsStory[]> GetStories(int takeStories);
}