# SingularSystems Project Readme

## Introduction

Welcome to the SingularSystems project! This solution consists of two main components:
1. **Backend:** An ASP.NET Core project named `SingularSystems.Core`
2. **Frontend:** An Angular project named `SingularSystems.Client`

The project aims to display a list of products, their details, and a summary of their sales. Users can paginate through the products, view detailed summaries in a modal, and interact with product images.

## Getting Started

### Prerequisites

Ensure you have the following installed on your development machine:
- .NET Core SDK
- Node.js and npm
- Angular CLI

### Setting Up the Backend

1. **Navigate to the backend directory:**
   ```bash
   cd SingularSystems.Core
   ```

2. **Restore the .NET dependencies:**
   ```bash
   dotnet restore
   ```

3. **Update the database:**
   ```bash
   dotnet ef database update
   ```

4. **Run the backend server:**
   ```bash
   dotnet run
   ```
   The backend server will start running on `https://localhost:5001`.

### Setting Up the Frontend

1. **Navigate to the frontend directory:**
   ```bash
   cd SingularSystems.Client
   ```

2. **Install the npm dependencies:**
   ```bash
   npm install
   ```

3. **Run the Angular development server:**
   ```bash
   ng serve
   ```
   The Angular application will start running on `http://localhost:4200`.

### How to Use

1. **Start both the backend and frontend servers as mentioned above.**
2. **Navigate to `http://localhost:4200` in your web browser to view the Angular application.**

## Functionality Overview

### Product Listing

The main table in the application displays the following columns for each product:
- ID
- Description
- Category
- Price (displayed in Rands)
- Image (clickable to view a larger version in a modal)
- View Summary (a button to fetch and display the product's sales summary in a modal)

### Product Summary

Clicking the "View Summary" button will:
1. Make an API call to the backend to fetch the product's sales summary.
2. Display the summary data in a modal dialog.

### Pagination

The product list supports pagination, allowing users to navigate through the products in batches. Users can select the number of items to display per page.

## API Endpoints

The backend provides the following key endpoints:

- `GET /api/products`: Fetches the list of products.
- `GET /api/product-sales-summary?productId={productId}`: Fetches the sales summary for a specific product.

## File Structure

### SingularSystems.Core
- **Controllers**
  - `ProductsController.cs`: Handles API requests for product data.
- **Models**
  - `Product.cs`: Defines the product model.
  - `ProductSummary.cs`: Defines the product summary model.
- **Services**
  - `ProductService.cs`: Contains business logic for fetching product data and summaries.

### SingularSystems.Client
- **app**
  - `app.component.ts`: Main application component.
  - `products.component.ts`: Component for displaying the product list and handling interactions.
- **services**
  - `product.service.ts`: Service for making API calls to the backend.

## Running Tests

### Backend

To run tests for the backend project:
```bash
cd SingularSystems.Core.Tests
dotnet test
```

### Frontend

To run tests for the frontend project:
```bash
cd SingularSystems.Client
ng test
```

## Contributing

If you wish to contribute to this project, please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature/bug fix.
3. Commit your changes.
4. Push your branch and create a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.


