namespace CodeQualityTalk.Abstractions;

public interface IDocument
{
    string OwnerId { get; }
    string Name { get; }
}