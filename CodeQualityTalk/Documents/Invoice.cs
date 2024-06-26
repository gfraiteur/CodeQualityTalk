using CodeQualityTalk.Abstractions;

namespace CodeQualityTalk.Documents;

internal class Invoice : IDocument
{
    public string OwnerId { get;  }
    public string Name { get;  }

    public Invoice(string ownerId, string name)
    {
        OwnerId = ownerId;
        Name = name;
    }

    public bool IsValid => string.IsNullOrEmpty(this.OwnerId);
}