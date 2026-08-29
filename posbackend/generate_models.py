import os

base_dir = r"C:\Users\pak25\source\repos\posbackend\posbackend\Models"

models = {
    "Tenant": [
        ("id", "int", "[Key]"),
        ("parent_id", "int?", ""),
        ("company_name", "string", ""),
        ("settings", "string", ""), 
        ("is_active", "bool", ""),
        ("created_at", "DateTime", ""),
        ("deleted_at", "DateTime?", "")
    ],
    "Store": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("name", "string", ""),
        ("phone", "string", ""),
        ("email", "string", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", ""),
        ("deleted_at", "DateTime?", "")
    ],
    "User": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("store_id", "int?", ""),
        ("role_id", "int", ""),
        ("username", "string", ""),
        ("email", "string", ""),
        ("password_hash", "string", ""),
        ("first_name", "string", ""),
        ("last_name", "string", ""),
        ("phone", "string", ""),
        ("is_active", "bool", ""),
        ("last_login_at", "DateTime?", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", ""),
        ("deleted_at", "DateTime?", "")
    ],
    "Role": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("name", "string", ""),
        ("permissions", "string", ""), 
        ("is_system", "bool", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "Product": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("category_id", "int?", ""),
        ("name", "string", ""),
        ("description", "string", ""),
        ("item_type", "string", ""),
        ("track_stock", "bool", ""),
        ("is_purchaseable", "bool", ""),
        ("duration_minutes", "int?", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", ""),
        ("deleted_at", "DateTime?", "")
    ],
    "ProductVariant": [
        ("id", "int", "[Key]"),
        ("product_id", "int", ""),
        ("sku", "string", ""),
        ("barcode", "string", ""),
        ("cost_price", "decimal", ""),
        ("sell_price", "decimal", ""),
        ("attributes", "string", ""), 
        ("is_active", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "Category": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("parent_id", "int?", ""),
        ("name", "string", ""),
        ("sort_order", "int", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "BomItem": [
        ("id", "int", "[Key]"),
        ("composite_product_id", "int", ""),
        ("ingredient_variant_id", "int", ""),
        ("quantity", "decimal", ""),
        ("created_at", "DateTime", "")
    ],
    "BundleItem": [
        ("id", "int", "[Key]"),
        ("bundle_product_id", "int", ""),
        ("component_variant_id", "int", ""),
        ("quantity", "int", ""),
        ("created_at", "DateTime", "")
    ],
    "StockLocation": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("store_id", "int", ""),
        ("name", "string", ""),
        ("is_default", "bool", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "StockSnapshot": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("location_id", "int", ""),
        ("variant_id", "int", ""),
        ("current_qty", "decimal", ""),
        ("last_updated_at", "DateTime", "")
    ],
    "StockLedger": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("variant_id", "int", ""),
        ("location_id", "int", ""),
        ("movement_type", "string", ""),
        ("quantity", "decimal", ""),
        ("balance_after", "decimal", ""),
        ("reference_type", "string", ""),
        ("reference_id", "int?", ""),
        ("note", "string", ""),
        ("occurred_at", "DateTime", ""),
        ("created_by", "int", ""),
        ("created_at", "DateTime", "")
    ],
    "Supplier": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("name", "string", ""),
        ("phone", "string", ""),
        ("email", "string", ""),
        ("address", "string", ""),
        ("tax_id", "string", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", ""),
        ("deleted_at", "DateTime?", "")
    ],
    "PurchaseOrder": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("supplier_id", "int", ""),
        ("po_number", "string", ""),
        ("status", "string", ""),
        ("total_amount", "decimal", ""),
        ("ordered_at", "DateTime?", ""),
        ("received_at", "DateTime?", ""),
        ("created_by", "int", ""),
        ("updated_at", "DateTime?", "")
    ],
    "PurchaseItem": [
        ("id", "int", "[Key]"),
        ("po_id", "int", ""),
        ("variant_id", "int", ""),
        ("quantity", "decimal", ""),
        ("unit_cost", "decimal", ""),
        ("received_qty", "decimal", ""),
        ("created_at", "DateTime", "")
    ],
    "Order": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("order_type_id", "int", ""),
        ("customer_id", "int?", ""),
        ("visit_id", "int?", ""),
        ("order_number", "string", ""),
        ("subtotal", "decimal", ""),
        ("discount_total", "decimal", ""),
        ("tax", "decimal", ""),
        ("total", "decimal", ""),
        ("status", "string", ""),
        ("note", "string", ""),
        ("created_by", "int", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "OrderItem": [
        ("id", "int", "[Key]"),
        ("order_id", "int", ""),
        ("variant_id", "int", ""),
        ("quantity", "decimal", ""),
        ("unit_price", "decimal", ""),
        ("discount_total", "decimal", ""),
        ("tax", "decimal", ""),
        ("item_category", "string", ""),
        ("staff_user_id", "int?", ""),
        ("created_at", "DateTime", "")
    ],
    "OrderType": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("code", "string", ""),
        ("name", "string", ""),
        ("affects_stock", "bool", ""),
        ("requires_payment_first", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "Payment": [
        ("id", "int", "[Key]"),
        ("order_id", "int", ""),
        ("method", "string", ""),
        ("amount", "decimal", ""),
        ("reference", "string", ""),
        ("paid_at", "DateTime", ""),
        ("created_by", "int", ""),
        ("created_at", "DateTime", "")
    ],
    "Bill": [
        ("id", "int", "[Key]"),
        ("customer_id", "int", ""),
        ("visit_id", "int?", ""),
        ("amount", "decimal", ""),
        ("paid_amount", "decimal", ""),
        ("due_date", "DateTime?", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "Customer": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("group_id", "int?", ""),
        ("parent_id", "int?", ""),
        ("name", "string", ""),
        ("phone", "string", ""),
        ("email", "string", ""),
        ("address", "string", ""),
        ("note", "string", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", ""),
        ("deleted_at", "DateTime?", "")
    ],
    "CustomerGroup": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("name", "string", ""),
        ("default_discount_pct", "decimal", ""),
        ("created_at", "DateTime", "")
    ],
    "ServiceVisit": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("store_id", "int", ""),
        ("customer_id", "int?", ""),
        ("product_id", "int", ""),
        ("staff_user_id", "int?", ""),
        ("resource_id", "int?", ""),
        ("visit_type", "string", ""),
        ("status", "string", ""),
        ("walk_in_name", "string", ""),
        ("scheduled_start_at", "DateTime?", ""),
        ("checked_in_at", "DateTime?", ""),
        ("actual_start_at", "DateTime?", ""),
        ("completed_at", "DateTime?", ""),
        ("note", "string", ""),
        ("vehicle_id", "int?", ""),
        ("pet_id", "int?", ""),
        ("package_id", "int?", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "ServiceResource": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("name", "string", ""),
        ("resource_type", "string", ""),
        ("hourly_rate", "decimal?", ""),
        ("peak_hourly_rate", "decimal?", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "StaffService": [
        ("id", "int", "[Key]"),
        ("user_id", "int", ""),
        ("product_id", "int", ""),
        ("custom_price", "decimal?", ""),
        ("commission_type", "string", ""),
        ("commission_value", "decimal?", ""),
        ("created_at", "DateTime", "")
    ],
    "StaffSchedule": [
        ("id", "int", "[Key]"),
        ("user_id", "int", ""),
        ("day_of_week", "int", ""),
        ("start_time", "TimeSpan", ""),
        ("end_time", "TimeSpan", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "QueueTicket": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("visit_id", "int", ""),
        ("reference_number", "string", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "ServiceQueue": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("store_id", "int", ""),
        ("queue_date", "DateTime", ""),
        ("next_number", "int", ""),
        ("created_at", "DateTime", "")
    ],
    "CustomField": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("entity_type", "string", ""),
        ("field_key", "string", ""),
        ("type", "string", ""),
        ("config", "string", ""),
        ("sort_order", "int", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "CustomFieldValue": [
        ("id", "int", "[Key]"),
        ("field_id", "int", ""),
        ("entity_type", "string", ""),
        ("entity_id", "int", ""),
        ("value", "string", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "Tag": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("name", "string", ""),
        ("color", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "Taggable": [
        ("id", "int", "[Key]"),
        ("tag_id", "int", ""),
        ("entity_type", "string", ""),
        ("entity_id", "int", ""),
        ("created_at", "DateTime", "")
    ],
    "StaffCommission": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("user_id", "int", ""),
        ("order_item_id", "int?", ""),
        ("visit_id", "int?", ""),
        ("commission_amount", "decimal", ""),
        ("rate_used", "decimal", ""),
        ("status", "string", ""),
        ("paid_at", "DateTime?", ""),
        ("created_at", "DateTime", "")
    ],
    "CustomerPackage": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("customer_id", "int", ""),
        ("product_id", "int", ""),
        ("total_units", "decimal", ""),
        ("remaining_units", "decimal", ""),
        ("expired_at", "DateTime?", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "PackageUsage": [
        ("id", "int", "[Key]"),
        ("customer_package_id", "int", ""),
        ("visit_id", "int?", ""),
        ("units_deducted", "decimal", ""),
        ("used_at", "DateTime", ""),
        ("recorded_by", "int", "")
    ],
    "CustomerVehicle": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("customer_id", "int", ""),
        ("license_plate", "string", ""),
        ("province", "string", ""),
        ("brand", "string", ""),
        ("model", "string", ""),
        ("vin_number", "string", ""),
        ("current_mileage", "int?", ""),
        ("created_at", "DateTime", "")
    ],
    "Quotation": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("customer_id", "int", ""),
        ("vehicle_id", "int?", ""),
        ("quote_number", "string", ""),
        ("parts_subtotal", "decimal", ""),
        ("labor_subtotal", "decimal", ""),
        ("total", "decimal", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "QuotationItem": [
        ("id", "int", "[Key]"),
        ("quotation_id", "int", ""),
        ("item_type", "string", ""),
        ("variant_id", "int?", ""),
        ("description", "string", ""),
        ("quantity", "decimal", ""),
        ("unit_price", "decimal", ""),
        ("created_at", "DateTime", "")
    ],
    "CustomerPet": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("customer_id", "int", ""),
        ("pet_name", "string", ""),
        ("species", "string", ""),
        ("breed", "string", ""),
        ("gender", "string", ""),
        ("birth_date", "DateTime?", ""),
        ("allergies", "string", ""),
        ("chronic_diseases", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "PetHealthRecord": [
        ("id", "int", "[Key]"),
        ("pet_id", "int", ""),
        ("visit_id", "int?", ""),
        ("weight_kg", "decimal?", ""),
        ("diagnosis", "string", ""),
        ("treatment_notes", "string", ""),
        ("next_due_date", "DateTime?", ""),
        ("created_at", "DateTime", "")
    ],
    "ResourceReservation": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("resource_id", "int", ""),
        ("variant_id", "int?", ""),
        ("visit_id", "int?", ""),
        ("reserved_start_at", "DateTime", ""),
        ("reserved_end_at", "DateTime", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "ResourcePeakSchedule": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("resource_id", "int?", ""),
        ("day_of_week", "int", ""),
        ("start_time", "TimeSpan", ""),
        ("end_time", "TimeSpan", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", "")
    ],
    "Class": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("course_product_id", "int", ""),
        ("teacher_user_id", "int?", ""),
        ("room_resource_id", "int?", ""),
        ("max_capacity", "int", ""),
        ("start_date", "DateTime", ""),
        ("end_date", "DateTime", ""),
        ("created_at", "DateTime", "")
    ],
    "ClassEnrollment": [
        ("id", "int", "[Key]"),
        ("class_id", "int", ""),
        ("student_customer_id", "int", ""),
        ("order_id", "int?", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "ClassAttendance": [
        ("id", "int", "[Key]"),
        ("class_id", "int", ""),
        ("student_customer_id", "int", ""),
        ("attended_at", "DateTime", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "Notification": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("customer_id", "int?", ""),
        ("channel", "string", ""),
        ("trigger_type", "string", ""),
        ("message", "string", ""),
        ("scheduled_at", "DateTime?", ""),
        ("sent_at", "DateTime?", ""),
        ("status", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "AuditLog": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("user_id", "int?", ""),
        ("action", "string", ""),
        ("entity_type", "string", ""),
        ("entity_id", "string", ""),
        ("changes", "string", ""),
        ("ip_address", "string", ""),
        ("user_agent", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "EventOutbox": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("event_type", "string", ""),
        ("payload", "string", ""),
        ("status", "string", ""),
        ("attempts", "int", ""),
        ("last_tried_at", "DateTime?", ""),
        ("created_at", "DateTime", "")
    ],
    "IndustryPreset": [
        ("id", "int", "[Key]"),
        ("code", "string", ""),
        ("name", "string", ""),
        ("default_config", "string", ""),
        ("created_at", "DateTime", "")
    ],
    "SubscriptionPlan": [
        ("id", "int", "[Key]"),
        ("name", "string", ""),
        ("monthly_price", "decimal", ""),
        ("features", "string", ""),
        ("is_active", "bool", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "Subscription": [
        ("id", "int", "[Key]"),
        ("tenant_id", "int", ""),
        ("plan_id", "int", ""),
        ("status", "string", ""),
        ("started_at", "DateTime?", ""),
        ("expires_at", "DateTime?", ""),
        ("created_at", "DateTime", ""),
        ("updated_at", "DateTime?", "")
    ],
    "Invoice": [
        ("id", "int", "[Key]"),
        ("subscription_id", "int", ""),
        ("amount", "decimal", ""),
        ("status", "string", ""),
        ("issued_at", "DateTime", ""),
        ("paid_at", "DateTime?", ""),
        ("created_at", "DateTime", "")
    ]
}

def to_pascal_case(snake_str):
    if not snake_str: return snake_str
    
    # Handle the slashes which are meant as "or" or multiple fields
    # Example: "brand/model" -> "brand", "model"
    # Actually the schema says brand/model, but we should make it one property if it's one column,
    # let's just replace '/' with 'Or'
    snake_str = snake_str.replace('/', '_or_')
    
    components = snake_str.split('_')
    return "".join(x.title() for x in components)

import codecs

for model_name, props in models.items():
    content = ["using System;", "using System.ComponentModel.DataAnnotations;", "using System.ComponentModel.DataAnnotations.Schema;", "", "namespace posbackend.Models", "{", f"    public class {model_name}", "    {"]
    
    for prop in props:
        col_name, data_type, attr = prop
        prop_name = to_pascal_case(col_name)
        if attr:
            content.append(f"        {attr}")
        if prop_name == "Id":
            prop_name = "Id"
        
        content.append(f"        public {data_type} {prop_name} {{ get; set; }}")
        
    content.append("    }")
    content.append("}")
    
    with codecs.open(os.path.join(base_dir, f"{model_name}.cs"), 'w', 'utf-8') as f:
        f.write("\n".join(content))

print("Created 52 files successfully!")
