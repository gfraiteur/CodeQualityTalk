namespace CodeQualityTalk;

public class CreditNoteCreator : IDocumentFactory
{
    public IDocument CreateDocument(string name, DocumentCreationContext context)
        => new CreditNote(context.OwnerId, name);
}