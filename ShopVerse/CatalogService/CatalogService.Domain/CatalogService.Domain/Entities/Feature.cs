using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Catalog.Domain.Entities;

public class Feature : Aggregate<FeatureId>
{
    public string Name { get; private set; }

    private Feature(FeatureId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Feature Create(FeatureId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        return new Feature(id, name);
    }
}
