namespace HackNewsApp.Domain.Entities
{
    public sealed class HackNewsItem
    {
        public int? Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Uri { get; init; } = string.Empty;
        public string PublishedBy { get; init; } = string.Empty;
        public DateTime PublishedTime { get; init; }
        public int CommentsCount { get; init; }
        public int Score { get; init; }

    }
}
