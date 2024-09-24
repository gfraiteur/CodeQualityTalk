// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using CodeQualityTalk.Abstractions;

namespace CodeQualityTalk;

internal class CreditNote : IDocument
{
    public string OwnerId { get; }

    public string Name { get; }

    public CreditNote( string ownerId, string name )
    {
        this.OwnerId = ownerId;
        this.Name = name;
    }
}