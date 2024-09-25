// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

#pragma warning disable RS2008

namespace CodeQualityTalk.Analyzers
{
    [DiagnosticAnalyzer( LanguageNames.CSharp )]
    public class FactoryNameAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor _diagnosticDescriptor = new(
            "MyAnalyzerWarning01",
            "Types implementing IDocumentFactory must be named *Factory",
            "The type {0} must have the Factory suffix because it implements IDocumentFactory",
            "Naming",
            DiagnosticSeverity.Warning,
            true );
        
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = [_diagnosticDescriptor];

        public override void Initialize( AnalysisContext context )
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
            context.RegisterSymbolAction( AnalyzeSymbol, SymbolKind.NamedType );
        }

        private static void AnalyzeSymbol( SymbolAnalysisContext context )
        {
            if ( context.Symbol is INamedTypeSymbol namedTypeSymbol &&
                 !namedTypeSymbol.Name.EndsWith( "Factory", StringComparison.Ordinal ) &&
                 namedTypeSymbol.AllInterfaces.Any( i => i.Name == "IDocumentFactory" ) )
            {
                foreach ( var syntaxReference in namedTypeSymbol.DeclaringSyntaxReferences )
                {
                    if ( syntaxReference.GetSyntax( context.CancellationToken ) is BaseTypeDeclarationSyntax syntax )
                    {
                        context.ReportDiagnostic(
                            Diagnostic.Create(
                                _diagnosticDescriptor,
                                syntax.Identifier.GetLocation(),
                                syntax.Identifier.Text ) );
                    }
                }
            }
        }

    }
}