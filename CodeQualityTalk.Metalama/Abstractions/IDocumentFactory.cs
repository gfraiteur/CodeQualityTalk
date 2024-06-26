using Metalama.Extensions.Architecture.Aspects;

namespace CodeQualityTalk.Abstractions;

[DerivedTypesMustRespectNamingConvention("*Factory")]
public interface IDocumentFactory
{
    IDocument CreateDocument(string name, DocumentCreationContext context);
}