using CodeQualityTalk.Documents;

namespace CodeQualityTalk.Abstractions;

public static class DocumentHelper
{
    public static bool IsInvoice(IDocument document) => document is Invoice;
}