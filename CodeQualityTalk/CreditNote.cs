using CodeQualityTalk.Abstractions;

namespace CodeQualityTalk;

internal class CreditNote : IDocument
{
    public string OwnerId { get;  }
    public string Name { get;  }

    public CreditNote(string ownerId, string name)
    {
        OwnerId = ownerId;
        Name = name;
    }
}