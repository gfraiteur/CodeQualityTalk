using CodeQualityTalk.Documents;

namespace CodeQualityTalk.Abstractions;

public static class DocumentHelper
{
    public static bool IsValueInvoice(IDocument document) => document is Invoice { IsValid: true };
}