// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using CodeQualityTalk.Abstractions;

namespace CodeQualityTalk.Factories;

public class CreditNoteCreator : IDocumentFactory
{
    public IDocument CreateDocument( string name, DocumentCreationContext context ) => new CreditNote( context.OwnerId, name );
}