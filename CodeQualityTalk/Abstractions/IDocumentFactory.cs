namespace CodeQualityTalk.Abstractions;

public interface IDocumentFactory
{
    IDocument CreateDocument(string name, DocumentCreationContext context);
}