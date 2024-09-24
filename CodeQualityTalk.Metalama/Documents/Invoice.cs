// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using CodeQualityTalk.Abstractions;

namespace CodeQualityTalk.Documents;

internal class Invoice : IDocument
{
    public string OwnerId { get; }

    public string Name { get; }

    public Invoice( string ownerId, string name )
    {
        this.OwnerId = ownerId;
        this.Name = name;
    }
}