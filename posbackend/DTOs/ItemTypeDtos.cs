using System;
using System.Text.Json.Serialization;

namespace posbackend.DTOs
{
    public class ItemTypeQueryParameters
    {
        [JsonPropertyName("tenant_id")]
        public Guid? TenantId { get; set; }

        [JsonPropertyName("search")]
        public string? Search { get; set; }

        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("is_service")]
        public bool? IsService { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 10;
    }

    public class ItemTypeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } // Remains int (serial4)

        [JsonPropertyName("tenant_id")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("track_stock_default")]
        public bool TrackStockDefault { get; set; }

        [JsonPropertyName("is_service")]
        public bool IsService { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
