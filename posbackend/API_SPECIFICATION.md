# POS Backend API Specification (v1)

**Base URL**: `http://localhost:5000/api/v1`  
**Content-Type**: `application/json`  
**Authentication**: JWT Bearer Token (`Authorization: Bearer <your_token>`)  
**Framework**: C# .NET 9.0 ASP.NET Core  

---

## 🔐 Authentication Header Format

สำหรับ API ทุกตัวยกเว้น `/auth/register` และ `/auth/login` จำเป็นต้องส่ง HTTP Header ดังนี้:

```http
Authorization: Bearer <jwt_token_here>
```

---

## 1. Auth API (`/api/v1/auth`)

### 1.1 POST `/api/v1/auth/register`
- **Description**: ลงทะเบียนผู้ใช้งานใหม่ (Public - ไม่ต้องส่ง Token)
- **Request Body**:
```json
{
  "username": "admin_pos",
  "email": "admin@pos.com",
  "password": "Password123!",
  "first_name": "Somchai",
  "last_name": "Dev",
  "phone": "0812345678",
  "tenant_id": 1,
  "store_id": 1,
  "role_id": 1
}
```
- **Response 200 OK**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expires_at": "2026-08-02T18:00:00Z",
  "user": {
    "id": 1,
    "tenant_id": 1,
    "store_id": 1,
    "role_id": 1,
    "username": "admin_pos",
    "email": "admin@pos.com",
    "first_name": "Somchai",
    "last_name": "Dev",
    "phone": "0812345678",
    "is_active": true,
    "created_at": "2026-08-01T18:00:00Z"
  }
}
```
- **Response 400 Bad Request**: `{"message": "Username already exists."}` หรือ `{"message": "Email already exists."}`

---

### 1.2 POST `/api/v1/auth/login`
- **Description**: เข้าสู่ระบบเพื่อขอรับ JWT Token (Public - ไม่ต้องส่ง Token)
- **Request Body**:
```json
{
  "username_or_email": "admin_pos",
  "password": "Password123!"
}
```
- **Response 200 OK**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expires_at": "2026-08-02T18:00:00Z",
  "user": {
    "id": 1,
    "tenant_id": 1,
    "store_id": 1,
    "role_id": 1,
    "username": "admin_pos",
    "email": "admin@pos.com",
    "first_name": "Somchai",
    "last_name": "Dev",
    "phone": "0812345678",
    "is_active": true,
    "created_at": "2026-08-01T18:00:00Z"
  }
}
```
- **Response 401 Unauthorized**: `{"message": "Invalid username/email or password."}`

---

### 1.3 GET `/api/v1/auth/me`
- **Description**: ดึงข้อมูลรายละเอียดโปรไฟล์ของผู้ใช้ปัจจุบันที่เข้าสู่ระบบ
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Response 200 OK**: UserProfile Object
- **Response 401 Unauthorized**: `{"message": "Unauthorized"}`

---

## 2. Categories API (`/api/v1/categories`) 🔒

### 2.1 GET `/api/v1/categories`
- **Description**: ดึงรายการหมวดหมู่สินค้า
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Query Parameters**:
  - `tenant_id` (integer, optional)
  - `parent_id` (integer, optional)
- **Response 200 OK**:
```json
[
  {
    "id": 1,
    "tenant_id": 1,
    "parent_id": null,
    "name": "เครื่องดื่ม",
    "sort_order": 1,
    "is_active": true,
    "created_at": "2026-08-01T10:00:00Z",
    "updated_at": null
  }
]
```

---

### 2.2 GET `/api/v1/categories/{id}`
- **Description**: ดึงรายละเอียดหมวดหมู่ตาม ID
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: Category Object
- **Response 404 Not Found**: `{"message": "Category with ID {id} not found."}`

---

### 2.3 POST `/api/v1/categories`
- **Description**: สร้างหมวดหมู่สินค้าใหม่
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Request Body**:
```json
{
  "tenant_id": 1,
  "parent_id": null,
  "name": "เบเกอรี่",
  "sort_order": 2
}
```
- **Response 201 Created**: Category Object
- **Response 400 Bad Request**: `{"message": "Category name is required."}`

---

### 2.4 PUT `/api/v1/categories/{id}`
- **Description**: แก้ไขข้อมูลหมวดหมู่
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Request Body**:
```json
{
  "name": "เบเกอรี่ & ขนมหวาน",
  "parent_id": null,
  "sort_order": 2,
  "is_active": true
}
```
- **Response 200 OK**: Category Object
- **Response 404 Not Found**

---

### 2.5 DELETE `/api/v1/categories/{id}`
- **Description**: ลบหมวดหมู่
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: `{"message": "Category with ID {id} deleted successfully."}`
- **Response 404 Not Found**

---

## 3. Item Types API (`/api/v1/item-types`) 🔒

### 3.1 GET `/api/v1/item-types`
- **Description**: ดึงรายการประเภทสินค้าพร้อม Pagination
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Query Parameters**:
  - `tenant_id` (integer, optional)
  - `search` (string, optional) - ค้นหาด้วยชื่อหรือโค้ด
  - `is_active` (boolean, optional)
  - `is_service` (boolean, optional)
  - `page` (integer, default: 1)
  - `limit` (integer, default: 10)
- **Response 200 OK**:
```json
{
  "total_count": 2,
  "page": 1,
  "limit": 10,
  "total_pages": 1,
  "data": [
    {
      "id": 1,
      "tenant_id": 1,
      "code": "PHYSICAL",
      "name": "สินค้าจับต้องได้",
      "description": "Physical Goods",
      "track_stock_default": true,
      "is_service": false,
      "is_active": true,
      "created_at": "2026-08-01T10:00:00Z",
      "updated_at": null
    }
  ]
}
```

---

### 3.2 GET `/api/v1/item-types/{id}`
- **Description**: ดึงรายละเอียดประเภทสินค้าตาม ID
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: ItemType Object
- **Response 404 Not Found**

---

## 4. Products API (`/api/v1/products`) 🔒

### 4.1 GET `/api/v1/products`
- **Description**: ดึงรายการสินค้าพร้อม Variants และ Pagination
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Query Parameters**:
  - `search` (string, optional)
  - `category_id` (integer, optional)
  - `item_type` (string, optional)
  - `page` (integer, default: 1)
  - `limit` (integer, default: 10)
- **Response 200 OK**:
```json
{
  "total_count": 1,
  "page": 1,
  "limit": 10,
  "total_pages": 1,
  "data": [
    {
      "id": 1,
      "tenant_id": 1,
      "category_id": 1,
      "category_name": "เครื่องดื่ม",
      "name": "กาแฟอเมริกาโน่",
      "description": "Americano Coffee",
      "item_type": "PHYSICAL",
      "track_stock": true,
      "is_purchaseable": true,
      "duration_minutes": null,
      "is_active": true,
      "created_at": "2026-08-01T10:00:00Z",
      "updated_at": null,
      "variants": [
        {
          "id": 101,
          "product_id": 1,
          "sku": "AMR-HOT",
          "barcode": "885000000001",
          "cost_price": 25.00,
          "sell_price": 55.00,
          "attributes": "ร้อน",
          "is_active": true,
          "created_at": "2026-08-01T10:00:00Z"
        }
      ]
    }
  ]
}
```

---

### 4.2 GET `/api/v1/products/{id}`
- **Description**: ดึงรายละเอียดสินค้าตาม ID
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: Product Object
- **Response 404 Not Found**

---

### 4.3 POST `/api/v1/products`
- **Description**: สร้างสินค้าใหม่พร้อม Variants
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Request Body**:
```json
{
  "tenant_id": 1,
  "name": "ลาเต้เย็น",
  "category_id": 1,
  "item_type": "PHYSICAL",
  "track_stock": true,
  "description": "Iced Latte",
  "is_purchaseable": true,
  "duration_minutes": null,
  "variants": [
    {
      "sku": "LAT-ICE-M",
      "barcode": "885000000002",
      "cost_price": 30.00,
      "sell_price": 65.00,
      "attributes": "เย็น / แก้วกลาง"
    }
  ]
}
```
- **Response 201 Created**: Product Object
- **Response 400 Bad Request**: `{"message": "Product name is required."}`

---

### 4.4 PUT `/api/v1/products/{id}`
- **Description**: แก้ไขข้อมูลสินค้าหลัก (ไม่รวม Variants)
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Request Body**:
```json
{
  "name": "ลาเต้เย็น (สูตรพิเศษ)",
  "category_id": 1,
  "item_type": "PHYSICAL",
  "track_stock": true,
  "is_active": true,
  "description": "Special Iced Latte",
  "is_purchaseable": true,
  "duration_minutes": null
}
```
- **Response 200 OK**: Product Object
- **Response 404 Not Found**

---

### 4.5 DELETE `/api/v1/products/{id}`
- **Description**: ลบสินค้า (รวมถึง Variants ทั้งหมดของสินค้านั้น)
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: `{"message": "Product with ID {id} deleted successfully."}`
- **Response 404 Not Found**

---

## 5. Product Variants API (`/api/v1/product-variants`) 🔒

### 5.1 PUT `/api/v1/product-variants/{id}`
- **Description**: แก้ไขข้อมูล Product Variant ตาม ID
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Request Body**:
```json
{
  "sku": "LAT-ICE-M-UPDATED",
  "barcode": "885000000009",
  "cost_price": 32.00,
  "sell_price": 70.00,
  "attributes": "เย็น / หวานน้อย",
  "is_active": true
}
```
- **Response 200 OK**: ProductVariant Object
- **Response 404 Not Found**

---

## 6. Stock Locations API (`/api/v1/stock-locations`) 🔒

### 6.1 GET `/api/v1/stock-locations`
- **Description**: ดึงรายการคลังสินค้า
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Query Parameters**:
  - `tenant_id` (integer, optional)
  - `store_id` (integer, optional)
- **Response 200 OK**:
```json
[
  {
    "id": 1,
    "tenant_id": 1,
    "store_id": 10,
    "name": "คลังหน้าร้าน (Main Store)",
    "is_default": true,
    "is_active": true,
    "created_at": "2026-08-01T10:00:00Z"
  }
]
```

---

### 6.2 GET `/api/v1/stock-locations/{id}`
- **Description**: ดึงรายละเอียดคลังสินค้าตาม ID
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: StockLocation Object
- **Response 404 Not Found**

---

### 6.3 POST `/api/v1/stock-locations`
- **Description**: สร้างคลังสินค้าใหม่
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Request Body**:
```json
{
  "tenant_id": 1,
  "store_id": 10,
  "name": "คลังหลังร้าน (Backroom)",
  "is_default": false
}
```
- **Response 201 Created**: StockLocation Object
- **Response 400 Bad Request**: `{"message": "Stock location name is required."}`

---

### 6.4 PUT `/api/v1/stock-locations/{id}`
- **Description**: แก้ไขคลังสินค้า
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Request Body**:
```json
{
  "name": "คลังหลังร้าน (ปรับปรุง)",
  "is_default": false,
  "is_active": true
}
```
- **Response 200 OK**: StockLocation Object
- **Response 404 Not Found**

---

### 6.5 DELETE `/api/v1/stock-locations/{id}`
- **Description**: ลบคลังสินค้า
- **Headers**: `Authorization: Bearer <jwt_token>`
- **Path Parameters**: `id` (integer, required)
- **Response 200 OK**: `{"message": "Stock location with ID {id} deleted successfully."}`
- **Response 404 Not Found**
