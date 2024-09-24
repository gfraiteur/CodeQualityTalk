// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using CodeQualityTalk.Abstractions;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Validation;

namespace CodeQualityTalk.Architecture;

public class AdvancedFabric : ProjectFabric
{
    private const string _documentNamespace = "CodeQualityTalk.Documents";

    private static readonly DiagnosticDefinition<(INamedType Type, string Namespace)> _warning = new(
        "MY001",
        Severity.Warning,
        "(Metalama) The '{0}' type must be in the '{1}' namespace." );

    public override void AmendProject( IProjectAmender amender )
    {
        // Validate namespace.
        amender.SelectReflectionType( typeof(IDocument) )
            .ValidateInboundReferences( ValidateNamespace, ReferenceGranularity.Type, ReferenceKinds.BaseType );
    }

    private static void ValidateNamespace( ReferenceValidationContext context )
    {
        if ( context.Origin.Namespace.FullName != _documentNamespace )
        {
            context.Diagnostics.Report( _warning.WithArguments( (context.Origin.Type, _documentNamespace) ) );
        }
    }
}