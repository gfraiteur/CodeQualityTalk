namespace CodeQualityTalk;

internal class Invoice : IDocument
{
    public string OwnerId { get;  }
    public string Name { get;  }

    public Invoice(string ownerId, string name)
    {
        OwnerId = ownerId;
        Name = name;
    }
}