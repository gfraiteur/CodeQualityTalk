using Metalama.Extensions.Architecture;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;

namespace CodeQualityTalk.Architecture;

public class Fabric : ProjectFabric
{
    public override void AmendProject(IProjectAmender amender)
    {
        // Validate dependencies.
        amender
            .Select(c=>c.GlobalNamespace.GetDescendant("CodeQualityTalk.Documents")!)
            .CannotBeUsedFrom( x => x.Namespace("CodeQualityTalk.Abstractions"));
    }
}