using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(Category category)
    : ICommand<CreateCategoryResult>;
public record CreateCategoryResult
{
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }
    public CategoryId? categoryId { get; init; }

    public static CreateCategoryResult Success(Category category)
        => new CreateCategoryResult { IsSuccess = true , categoryId = category.Id };
    public static CreateCategoryResult Failure(string errorMessage) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage };
}
