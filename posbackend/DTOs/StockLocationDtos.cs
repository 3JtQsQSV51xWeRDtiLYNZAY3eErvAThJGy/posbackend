using System;
using System.Text.Json.Serialization;

namespace posbackend.DTOs
{
    public class StockLocationQueryParameters
    {
        [JsonPropertyName("tenant_id")]
        public Guid? TenantId { get; set; }

        [JsonPropertyName("store_id")]
        public Guid? StoreId { get; set; }
    }

    public class CreateStockLocationDto
    {
        [JsonPropertyName("tenant_id")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("store_id")]
        public Guid StoreId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; } = false;
    }

    public class UpdateStockLocationDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; } = true;
    }

    public class StockLocationDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("tenant_id")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("store_id")]
        public Guid StoreId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
