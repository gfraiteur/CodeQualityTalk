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
            "MY001",
            "Types implementing IDocumentFactory must be named *Factory",
            "(Analyzer) The type {0} must have the Factory suffix because it implements IDocumentFactory",
            "Naming",
            DiagnosticSeverity.Warning,
            true );

        public override void Initialize( AnalysisContext context )
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
            context.RegisterSemanticModelAction( this.AnalyzeSemanticModel );
        }

        private void AnalyzeSemanticModel( SemanticModelAnalysisContext context )
        {
            new Walker( context ).Visit( context.FilterTree.GetRoot() );
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = [_diagnosticDescriptor];

        private class Walker : CSharpSyntaxWalker
        {
            private readonly SemanticModelAnalysisContext _context;

            public Walker( SemanticModelAnalysisContext context )
            {
                this._context = context;
            }

            public override void VisitClassDeclaration( ClassDeclarationSyntax node )
            {
                this.InspectType( node );
                base.VisitClassDeclaration( node );
            }

            public override void VisitRecordDeclaration( RecordDeclarationSyntax node )
            {
                this.InspectType( node );
                base.VisitRecordDeclaration( node );
            }

            public override void VisitStructDeclaration( StructDeclarationSyntax node )
            {
                this.InspectType( node );
                base.VisitStructDeclaration( node );
            }

            private void InspectType( TypeDeclarationSyntax type )
            {
                var typeSymbol = this._context.SemanticModel.GetDeclaredSymbol( type );

                if ( typeSymbol != null &&
                     !type.Identifier.Text.EndsWith( "Factory" ) &&
                     typeSymbol.AllInterfaces.Any( i => i.Name == "IDocumentFactory" ) )
                {
                    this._context.ReportDiagnostic(
                        Diagnostic.Create(
                            _diagnosticDescriptor,
                            type.Identifier.GetLocation(),
                            type.Identifier.Text ) );
                }
            }
        }
    }
}