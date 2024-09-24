// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Workspaces;

if ( args.Length != 1 )
{
    Console.Error.WriteLine( "Usage CodeQualityTalk.Verifier.exe <csproj>" );

    return 1;
}

var workspace = Workspace.Load( args[0] );

// TypesDerivedFromIFactory_ShouldBe_NamedFactory
workspace.SourceCode.Types
    .Single( t => t.Name == "IDocumentFactory" )
    .GetDerivedTypes()
    .Where( t => !t.Name.EndsWith( "Factory" ) )
    .Report( Severity.Warning, "MY001", "The type name must end with Factory because it implements IDocumentFactory." );

// TypesDerivedFromIDocument_ShouldBe_InDocumentsNamespace
workspace.SourceCode.Types
    .Single( t => t.Name == "IDocument" )
    .GetDerivedTypes()
    .Where( t => t.ContainingNamespace.FullName != "CodeQualityTalk.Documents" )
    .Report(
        Severity.Warning,
        "MY002",
        "The type must be in the CodeQualityTalk.Documents namespace because it implements IDocument." );

// Abstractions_CannotUse_DocumentsNamespace
workspace.SourceCode.Types
    .Where( t => t.ContainingNamespace.FullName == "CodeQualityTalk.Abstractions" )
    .SelectMany( t => t.GetOutboundReferences() )
    .Where(
        r =>
        {
            var ns = r.DestinationDeclaration.GetNamespace()!.FullName;

            return ns.StartsWith( "CodeQualityTalk" ) && ns != "CodeQualityTalk.Abstractions";
        } )
    .Report(
        Severity.Warning,
        "MY003",
        "The CodeQualityTalks.Abstractions namespace must only not have dependencies to other namespaces." );

Console.WriteLine( $"{DiagnosticReporter.ReportedWarnings + DiagnosticReporter.ReportedErrors} architecture violations found." );

return DiagnosticReporter.ReportedErrors > 0 ? 2 : DiagnosticReporter.ReportedWarnings > 0 ? 1 : 0;