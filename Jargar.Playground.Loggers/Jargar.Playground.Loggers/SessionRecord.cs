namespace Jargar.Playgrounds.Loggers;

public sealed record SessionRecord
{
    public required int Id { get; set; }

    public required DateTime CreatedDateTime { get; set; } = DateTime.Now;

    public required Guid UniqueId { get; set; }

    public required string UserId { get; set; }

    public required int OrganisationId { get; set; }

    public required string Email { get; set; }

    public required bool IsOnlineMode { get; set; }
}