using CodeQualityTalk.Abstractions;
using CodeQualityTalk.Documents;

namespace CodeQualityTalk.Factories;

public class InvoiceFactory : IDocumentFactory
{
    public IDocument CreateDocument(string name, DocumentCreationContext context)
        => new Invoice(context.OwnerId, name);
}