// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using CodeQualityTalk.Documents;

namespace CodeQualityTalk.Abstractions;

public static class DocumentHelper
{
    public static bool IsValidInvoice( IDocument document ) => document is Invoice { IsValid: true };
}