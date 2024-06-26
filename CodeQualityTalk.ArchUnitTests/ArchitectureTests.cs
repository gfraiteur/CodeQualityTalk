using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using CodeQualityTalk.Abstractions;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using Type = ArchUnitNET.Loader.Type;

namespace CodeQualityTalk.Tests;

public class ArchitectureTests
{
    private readonly Architecture _testedProjects = new ArchLoader().LoadAssembly(typeof(IDocumentFactory).Assembly).Build();
    
    [Fact]
    public void TypesDerivedFromIFactory_ShouldBe_NamedFactory()
    {
        Types()
            .That()
            .ImplementInterface(typeof(IDocumentFactory))
            .Should()
            .HaveName("^.*Factory$", true)
            .Check(_testedProjects);
    }

    [Fact]
    public void TypesDerivedFromIDocument_ShouldBe_InDocumentsNamespace()
    {
        Types()
            .That()
            .ImplementInterface(typeof(IDocument))
            .Should()
            .HaveFullName(@"^CodeQualityTalk\.Documents\..*", true)
            .Check(_testedProjects);
    }

    [Fact]
    public void Abstractions_CannotUse_DocumentsNamespace()
    {
        Types()
            .That()
            .ResideInNamespace("CodeQuality.Abstractions")
            .Should()
            .NotDependOnAnyTypesThat()
            .ResideInNamespace("CodeQuality.Documents")
            .Check(_testedProjects);
            
        
    }
}