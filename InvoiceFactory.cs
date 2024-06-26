namespace CodeQualityTalk;

public class InvoiceFactory : IDocumentFactory
{
    public IDocument CreateDocument(string name, DocumentCreationContext context)
        => new Invoice(context.OwnerId, name);
}