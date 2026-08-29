using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace posbackend.DTOs
{
    public class ProductQueryParameters
    {
        [JsonPropertyName("search")]
        public string? Search { get; set; }

        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }

        [JsonPropertyName("item_type")]
        public string? ItemType { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 10;
    }

    public class CreateProductVariantDto
    {
        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("cost_price")]
        public decimal CostPrice { get; set; }

        [JsonPropertyName("sell_price")]
        public decimal SellPrice { get; set; }

        [JsonPropertyName("attributes")]
        public string? Attributes { get; set; }
    }

    public class CreateProductDto
    {
        [JsonPropertyName("tenant_id")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }

        [JsonPropertyName("item_type")]
        public string? ItemType { get; set; }

        [JsonPropertyName("track_stock")]
        public bool TrackStock { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("is_purchaseable")]
        public bool IsPurchaseable { get; set; } = true;

        [JsonPropertyName("duration_minutes")]
        public int? DurationMinutes { get; set; }

        [JsonPropertyName("variants")]
        public List<CreateProductVariantDto>? Variants { get; set; }
    }

    public class UpdateProductDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }

        [JsonPropertyName("item_type")]
        public string? ItemType { get; set; }

        [JsonPropertyName("track_stock")]
        public bool TrackStock { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; } = true;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("is_purchaseable")]
        public bool? IsPurchaseable { get; set; }

        [JsonPropertyName("duration_minutes")]
        public int? DurationMinutes { get; set; }
    }

    public class UpdateProductVariantDto
    {
        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("cost_price")]
        public decimal CostPrice { get; set; }

        [JsonPropertyName("sell_price")]
        public decimal SellPrice { get; set; }

        [JsonPropertyName("attributes")]
        public string? Attributes { get; set; }

        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }
    }

    public class ProductVariantDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("cost_price")]
        public decimal CostPrice { get; set; }

        [JsonPropertyName("sell_price")]
        public decimal SellPrice { get; set; }

        [JsonPropertyName("attributes")]
        public string? Attributes { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    public class ProductDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("tenant_id")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("item_type")]
        public string? ItemType { get; set; }

        [JsonPropertyName("track_stock")]
        public bool TrackStock { get; set; }

        [JsonPropertyName("is_purchaseable")]
        public bool IsPurchaseable { get; set; }

        [JsonPropertyName("duration_minutes")]
        public int? DurationMinutes { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("variants")]
        public List<ProductVariantDto> Variants { get; set; } = new();
    }

    public class PagedResultDto<T>
    {
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages => Limit > 0 ? (int)Math.Ceiling((double)TotalCount / Limit) : 0;

        [JsonPropertyName("data")]
        public List<T> Data { get; set; } = new();
    }
}
