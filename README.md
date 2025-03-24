# VAT Calculation API
<em>by Carlos Filho</em> <a href="https://www.linkedin.com/in/carlosvamberto/" target="_blank">https://www.linkedin.com/in/carlosvamberto </a><br>
<carlosvamberto@hotmail.com>

## Overview
This API allows users to calculate Net, Gross, and VAT amounts for purchases in Austria. By providing one of these values along with a valid Austrian VAT rate (10%, 13%, or 20%), the API calculates the missing values and returns them in a structured response.

## Endpoints

### Calculate VAT
**URL:** `/api/vat/calculate`

**Method:** `POST`

**Request Body:**
```json
{
  "Net": 100.00,
  "Gross": null,
  "Vat": null,
  "VatRate": 0.2
}
```

**Response:**
```json
{
  "Net": 100.00,
  "Gross": 120.00,
  "Vat": 20.00
}
```

## Request Parameters
- **Net** (decimal, optional): The net price before VAT.
- **Gross** (decimal, optional): The total price including VAT.
- **Vat** (decimal, optional): The VAT amount.
- **VatRate** (decimal, required): The VAT rate (10%, 13%, or 20%).

## Validation Rules
- Only **one** of `Net`, `Gross`, or `Vat` should be provided.
- `VatRate` must be either `0.1`, `0.13`, or `0.2`.
- If no amount (`Net`, `Gross`, `Vat`) is provided, an error is returned.
- If more than one amount is provided, an error is returned.

## Error Handling
The API returns meaningful error messages in case of invalid input:
- **Invalid VAT rate input:**
```json
{
  "message": "Invalid VAT rate input. The rate must be 10%, 13%, or 20%."
}
```
- **More than one value provided:**
```json
{
  "message": "Only one amount (Net, Gross, VAT) should be provided."
}
```
- **No value provided:**
```json
{
  "message": "At least one value (Net, Gross, VAT) must be provided."
}
```

## Technical Requirements
- Implemented using **.NET Core 8 (LTS)**.
- Uses **Dependency Injection (DI)** for service management.
- Follows **REST API** standards.
- Uses **NuGet package manager**.

## Testing
You can validate the calculation logic using this [Docker Container](https://hub.docker.com/repository/docker/carlosvamberto/taxcalculatorapi).
