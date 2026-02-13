# API Documentation (For Future Extensions)

## Overview
This document outlines potential API endpoints that could be implemented for mobile or third-party integrations.

## Authentication Endpoints

### POST /api/auth/login
Authenticate user and return session token

**Request:**
```json
{
  "username": "string",
  "password": "string"
}
```

**Response:**
```json
{
  "success": true,
  "token": "string",
  "userId": 1,
  "username": "string"
}
```

### POST /api/auth/register
Register new user account

**Request:**
```json
{
  "username": "string",
  "email": "string",
  "password": "string"
}
```

## Transaction Endpoints

### GET /api/transactions
Get all transactions for authenticated user

**Query Parameters:**
- startDate (optional): ISO date string
- endDate (optional): ISO date string
- categoryId (optional): integer
- type (optional): "income" | "expense"

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "transactionId": 1,
      "description": "Grocery shopping",
      "amount": 50.00,
      "category": {
        "id": 1,
        "name": "Food & Dining",
        "color": "#fd7e14"
      },
      "type": "expense",
      "date": "2026-02-13"
    }
  ]
}
```

### POST /api/transactions
Create new transaction

**Request:**
```json
{
  "description": "string",
  "amount": 0.00,
  "categoryId": 1,
  "type": "income|expense",
  "date": "2026-02-13"
}
```

### PUT /api/transactions/{id}
Update existing transaction

### DELETE /api/transactions/{id}
Delete transaction

## Budget Endpoints

### GET /api/budgets
Get all budgets for month/year

**Query Parameters:**
- month: 1-12
- year: integer

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "budgetId": 1,
      "category": {
        "id": 1,
        "name": "Food & Dining"
      },
      "limitAmount": 300.00,
      "amountSpent": 150.00,
      "remaining": 150.00,
      "percentageUsed": 50.0,
      "isOverBudget": false
    }
  ]
}
```

### POST /api/budgets
Create new budget

### PUT /api/budgets/{id}
Update budget

### DELETE /api/budgets/{id}
Delete budget

## Dashboard Endpoint

### GET /api/dashboard
Get dashboard data

**Response:**
```json
{
  "success": true,
  "data": {
    "totalBalance": 5000.00,
    "monthlyIncome": 3000.00,
    "monthlyExpenses": 1500.00,
    "monthlySavings": 1500.00,
    "expensesByCategory": [
      {
        "categoryName": "Food & Dining",
        "amount": 500.00,
        "percentage": 33.3,
        "color": "#fd7e14"
      }
    ],
    "recentTransactions": []
  }
}
```

## Category Endpoints

### GET /api/categories
Get all available categories

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "categoryId": 1,
      "name": "Food & Dining",
      "type": "expense",
      "color": "#fd7e14",
      "icon": "fa-utensils"
    }
  ]
}
```

## Error Responses

All endpoints may return error responses:

```json
{
  "success": false,
  "error": "Error message",
  "code": "ERROR_CODE"
}
```

Common HTTP Status Codes:
- 200: Success
- 201: Created
- 400: Bad Request
- 401: Unauthorized
- 404: Not Found
- 500: Internal Server Error

## Implementation Notes

To implement these APIs:

1. Create `ApiController` base class
2. Implement JWT token authentication
3. Add API-specific error handling
4. Implement rate limiting
5. Add API documentation with Swagger
6. Enable CORS for allowed origins

---

**Note**: This API is not currently implemented but serves as a roadmap for future development.
