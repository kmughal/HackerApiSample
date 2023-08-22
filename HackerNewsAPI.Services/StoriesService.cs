using HackerNewsAPI.Interfaces;
using HackerNewsAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HackerNewsAPI.Services;

public class StoriesService : IStoriesService
{
    private readonly HttpClient _httpClient;
    private readonly Settings _settings;
    private readonly IMemoryCache _memoryCache;

    public StoriesService(IHttpClientFactory httpClientFactory, IOptions<Settings> options, IMemoryCache memoryCache)
    {
        _httpClient = httpClientFactory.CreateClient();
        _settings = options.Value;
        _memoryCache = memoryCache;
    }

    public async Task<HackerNewsStory[]> GetStories(int takeStories)
    {

        if (!_memoryCache.TryGetValue("cachedStories", out HackerNewsStory[] stories))
        {
            stories = await GetStoriesFromHacker(takeStories);
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            _memoryCache.Set("cachedStories", stories, cacheEntryOptions);
        }

        return stories;

    }

    private async Task<HackerNewsStory[]> GetStoriesFromHacker(int takeStories)
    {
        var bestStoryIdsResponse = await _httpClient.GetStringAsync(_settings.StoriesUrl);
        var bestStoryIds = JsonConvert.DeserializeObject<int[]>(bestStoryIdsResponse);

        var bestStories = await Task.WhenAll(bestStoryIds!.Take(takeStories).Select(GetStoryDetailsAsync));
        return bestStories;
    }

    private async Task<HackerNewsStory> GetStoryDetailsAsync(int storyId)
    {
        var storyUrl = string.Format(_settings.StoryItemUrl, storyId);
        var storyResponse = await _httpClient.GetStringAsync(storyUrl);
        if (!string.IsNullOrWhiteSpace(storyResponse))
        {
            var story = JsonConvert.DeserializeObject<HackerNewsStory>(storyResponse);
            return story!;
        }
        return null;
    }

}