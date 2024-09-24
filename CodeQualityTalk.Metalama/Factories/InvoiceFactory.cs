// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using CodeQualityTalk.Abstractions;
using CodeQualityTalk.Documents;

namespace CodeQualityTalk.Factories;

public class InvoiceFactory : IDocumentFactory
{
    public IDocument CreateDocument( string name, DocumentCreationContext context ) => new Invoice( context.OwnerId, name );
}