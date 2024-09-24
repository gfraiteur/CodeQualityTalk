// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace CodeQualityTalk.Abstractions;

public interface IDocument
{
    string OwnerId { get; }

    string Name { get; }
}