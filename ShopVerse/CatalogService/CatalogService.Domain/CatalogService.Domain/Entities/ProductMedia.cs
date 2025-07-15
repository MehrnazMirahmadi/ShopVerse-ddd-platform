using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Catalog.Domain.Entities;

public class ProductMedia : Entity<ProductMediaId>
{
    public ProductMediaTypeId MediaTypeId { get; private set; }
    public string Url { get; private set; }
    public long FileSize { get; private set; }
    public string UploadingUserName { get; private set; }
    public bool IsConfirmed { get; private set; }
    public string? ConfirmedByUserName { get; private set; }

    private ProductMedia(ProductMediaId id, ProductMediaTypeId mediaTypeId, string url, long fileSize, string uploadingUserName, bool isConfirmed, string? confirmedByUserName)
    {
        Id = id;
        MediaTypeId = mediaTypeId;
        Url = url;
        FileSize = fileSize;
        UploadingUserName = uploadingUserName;
        IsConfirmed = isConfirmed;
        ConfirmedByUserName = confirmedByUserName;
    }

    public static ProductMedia Create(ProductMediaId id, ProductMediaTypeId mediaTypeId, string url, long fileSize, string uploadingUserName)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("Url is required");
        if (string.IsNullOrWhiteSpace(uploadingUserName)) throw new ArgumentException("Uploader username is required");

        return new ProductMedia(id, mediaTypeId, url, fileSize, uploadingUserName, false, null);
    }

    public void Confirm(string confirmedByUserName)
    {
        IsConfirmed = true;
        ConfirmedByUserName = confirmedByUserName;
    }
}