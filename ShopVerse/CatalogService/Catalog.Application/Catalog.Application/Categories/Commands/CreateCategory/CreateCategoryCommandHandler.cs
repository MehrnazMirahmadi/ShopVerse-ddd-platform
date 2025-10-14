using ShopVerse.BuildingBlocks.CQRS;

namespace Catalog.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler
    : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
{
    public Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
