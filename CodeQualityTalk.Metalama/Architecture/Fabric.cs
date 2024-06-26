using CodeQualityTalk.Abstractions;
using Metalama.Extensions.Architecture;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Validation;

namespace CodeQualityTalk.Architecture;

public class Fabric : ProjectFabric
{
    const string _documentNamespace = "CodeQualityTalk.Documents";

    private static DiagnosticDefinition<(INamedType Type, string Namespace)> _warning = new("MY001", Severity.Warning,
        "(Metalama) The '{0}' type must be in the '{1}' namespace.");

    public override void AmendProject(IProjectAmender amender)
    {
        // Validate dependencies.
        amender
            .Select(c=>c.GlobalNamespace.GetDescendant("CodeQuality.Documents")!)
            .CannotBeUsedFrom( x => x.Namespace("CodeQuality.Abstractions"));
        
        
        // Validate namespace.
        amender.SelectReflectionType(typeof(IDocument)).ValidateOutboundReferences(ValidateNamespace, ReferenceGranularity.Type, ReferenceKinds.BaseType );
    }
    
    private static void ValidateNamespace(ReferenceValidationContext context)
    {
        if (context.Origin.Namespace.FullName != _documentNamespace)
        {
            context.Diagnostics.Report(_warning.WithArguments((context.Origin.Type, _documentNamespace)));
        }
    }
}