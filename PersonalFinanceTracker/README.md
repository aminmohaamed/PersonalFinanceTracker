# Personal Finance Tracker - .NET MVC Web Application

## 📋 Project Overview

A comprehensive Personal Finance Tracker web application built with ASP.NET MVC that helps users manage their income, expenses, budgets, and financial goals. The application features user authentication, interactive dashboards, transaction management, budget tracking, and data visualizations.

## ✨ Features

### User Authentication
- User registration and login
- Session-based authentication
- Secure password hashing (SHA256)
- Protected routes and authorization

### Dashboard
- Financial overview with key metrics
  - Total Balance
  - Monthly Income
  - Monthly Expenses
  - Monthly Savings
- Interactive pie chart showing spending by category
- Budget progress visualization
- Recent transactions list

### Transaction Management
- Add, edit, and delete income/expense transactions
- Categorize transactions (Food, Transport, Entertainment, etc.)
- Filter transactions by:
  - Date range
  - Category
  - Transaction type (Income/Expense)
- Real-time transaction deletion with AJAX
- Visual category badges with color coding

### Budget Management
- Create monthly budgets for expense categories
- Track budget vs. actual spending
- Progress bars showing budget utilization
- Visual warnings for over-budget categories
- Edit and delete budgets

### Data Visualization
- Pie charts for expense breakdown by category
- Budget progress bars
- Color-coded transaction types
- Responsive charts using Chart.js

## 🏗️ Architecture & Design Patterns

### 1. **MVC Pattern (Model-View-Controller)**
- **Models**: Entity classes representing database tables
- **Views**: Razor views for UI rendering
- **Controllers**: Handle HTTP requests and business logic coordination

### 2. **Repository Pattern**
- Generic `IRepository<T>` interface for data access abstraction
- Concrete `Repository<T>` implementation using Entity Framework
- Decouples business logic from data access

### 3. **Unit of Work Pattern**
- `IUnitOfWork` manages transactions across multiple repositories
- Centralizes database context management
- Ensures data consistency

### 4. **Service Layer Pattern**
- Business logic separated into service classes:
  - `AuthService`: User authentication
  - `TransactionService`: Transaction operations
  - `BudgetService`: Budget management
  - `DashboardService`: Dashboard data aggregation
- Promotes single responsibility principle

### 5. **Dependency Injection**
- Custom dependency resolver for controller instantiation
- Services injected into controllers
- Promotes loose coupling and testability

### 6. **ViewModel Pattern**
- Separate ViewModels for each view
- Data transfer between controllers and views
- Validation attributes on ViewModels

### 7. **Strategy Pattern**
- Database initializer strategy for seeding data
- Configurable database initialization

## 📁 Project Structure

```
PersonalFinanceTracker/
│
├── App_Start/
│   ├── BundleConfig.cs          # CSS/JS bundling configuration
│   ├── DependencyConfig.cs      # Dependency injection setup
│   ├── FilterConfig.cs          # Global filters
│   └── RouteConfig.cs           # URL routing configuration
│
├── Controllers/
│   ├── AccountController.cs     # Authentication (Login/Register)
│   ├── BaseController.cs        # Common controller functionality
│   ├── BudgetController.cs      # Budget management
│   ├── DashboardController.cs   # Dashboard display
│   └── TransactionController.cs # Transaction CRUD operations
│
├── Data/
│   ├── ApplicationDbContext.cs  # Entity Framework context
│   └── DbInitializer.cs         # Database seeding
│
├── Filters/
│   └── AuthorizeUserAttribute.cs # Custom authorization filter
│
├── Models/
│   ├── Budget.cs                # Budget entity
│   ├── Category.cs              # Category entity
│   ├── Transaction.cs           # Transaction entity
│   └── User.cs                  # User entity
│
├── Repositories/
│   ├── IRepository.cs           # Generic repository interface
│   ├── Repository.cs            # Generic repository implementation
│   └── UnitOfWork.cs            # Unit of Work pattern
│
├── Services/
│   ├── AuthService.cs           # Authentication service
│   ├── BudgetService.cs         # Budget business logic
│   ├── DashboardService.cs      # Dashboard data service
│   └── TransactionService.cs    # Transaction business logic
│
├── ViewModels/
│   ├── AccountViewModels.cs     # Login/Register ViewModels
│   ├── BudgetViewModels.cs      # Budget ViewModels
│   ├── DashboardViewModel.cs    # Dashboard ViewModel
│   └── TransactionViewModels.cs # Transaction ViewModels
│
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml         # Login page
│   │   └── Register.cshtml      # Registration page
│   ├── Budget/
│   │   ├── Create.cshtml        # Create budget
│   │   ├── Edit.cshtml          # Edit budget
│   │   └── Index.cshtml         # Budget list
│   ├── Dashboard/
│   │   └── Index.cshtml         # Dashboard view
│   ├── Transaction/
│   │   ├── Create.cshtml        # Add transaction
│   │   ├── Edit.cshtml          # Edit transaction
│   │   └── Index.cshtml         # Transaction history
│   ├── Shared/
│   │   └── _Layout.cshtml       # Main layout template
│   ├── _ViewStart.cshtml        # View initialization
│   └── Web.config               # Views configuration
│
├── Global.asax                  # Application entry point
├── Global.asax.cs               # Application startup
└── Web.config                   # Main configuration file
```

## 🛠️ Technologies Used

- **Framework**: ASP.NET MVC 5
- **Language**: C# (.NET Framework 4.7.2)
- **Database**: SQL Server / LocalDB
- **ORM**: Entity Framework 6
- **Frontend**: 
  - Bootstrap 5.1.3
  - jQuery 3.6.0
  - Chart.js 3.7.0
  - Font Awesome 6.0
- **Authentication**: Forms Authentication with Session

## 📦 NuGet Packages Required

```xml
<packages>
  <package id="EntityFramework" version="6.4.4" />
  <package id="Microsoft.AspNet.Mvc" version="5.2.7" />
  <package id="Microsoft.AspNet.Razor" version="3.2.7" />
  <package id="Microsoft.AspNet.WebPages" version="3.2.7" />
  <package id="Microsoft.Web.Infrastructure" version="1.0.0" />
  <package id="Newtonsoft.Json" version="12.0.3" />
  <package id="WebGrease" version="1.6.0" />
  <package id="Antlr" version="3.5.0.2" />
  <package id="Microsoft.AspNet.Web.Optimization" version="1.1.3" />
</packages>
```

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- SQL Server or SQL Server LocalDB

### Installation Steps

1. **Clone or Download the Project**
   ```
   Extract the project to your desired location
   ```

2. **Open in Visual Studio**
   ```
   Open PersonalFinanceTracker.sln in Visual Studio
   ```

3. **Restore NuGet Packages**
   ```
   Right-click on Solution > Restore NuGet Packages
   ```

4. **Update Database Connection String**
   - Open `Web.config`
   - Update the connection string if needed (default uses LocalDB)

5. **Build the Solution**
   ```
   Build > Build Solution (Ctrl + Shift + B)
   ```

6. **Run Database Migrations**
   - The database will be created automatically on first run
   - Initial categories will be seeded

7. **Run the Application**
   ```
   Press F5 or click Start
   ```

8. **Create Your Account**
   - Navigate to the registration page
   - Create a new user account
   - Start tracking your finances!

## 📊 Database Schema

### Users Table
- UserId (PK)
- Username (Unique)
- Email (Unique)
- PasswordHash
- CreatedDate

### Transactions Table
- TransactionId (PK)
- UserId (FK)
- CategoryId (FK)
- Description
- Amount
- Type (Income/Expense)
- Date
- CreatedDate

### Categories Table
- CategoryId (PK)
- Name
- Description
- Type (Income/Expense/Both)
- ColorCode
- Icon

### Budgets Table
- BudgetId (PK)
- UserId (FK)
- CategoryId (FK)
- LimitAmount
- Month
- Year
- CreatedDate

## 🎨 Design Features

### Responsive Design
- Mobile-first approach
- Bootstrap grid system
- Flexible layouts for all screen sizes
- Sidebar navigation with mobile menu

### Color Coding
- Income: Green (#1cc88a)
- Expense: Red (#e74a3b)
- Balance: Blue (#36b9cc)
- Savings: Yellow (#f6c23e)
- Category-specific colors for charts

### User Experience
- Intuitive navigation
- Real-time feedback
- AJAX for smooth interactions
- Form validation
- Success/Error alerts
- Confirmation dialogs for deletions

## 🔒 Security Features

- Password hashing (SHA256)
- Session-based authentication
- Anti-forgery tokens
- SQL injection prevention (Entity Framework parameterized queries)
- XSS protection headers
- Secure cookie handling

## 📈 Future Enhancements

1. **Advanced Features**
   - Recurring transactions
   - Financial goals tracking
   - Export to PDF/Excel
   - Email notifications
   - Multi-currency support

2. **Analytics**
   - Monthly/Yearly comparison charts
   - Spending trends analysis
   - Category-wise analytics
   - Custom date range reports

3. **Security**
   - Two-factor authentication
   - Password recovery
   - BCrypt password hashing
   - Role-based authorization

4. **UI/UX**
   - Dark mode
   - Customizable themes
   - Dashboard widgets
   - Drag-and-drop budget planning

## 🧪 Testing

### Manual Testing Checklist
- [ ] User registration
- [ ] User login/logout
- [ ] Add income transaction
- [ ] Add expense transaction
- [ ] Edit transaction
- [ ] Delete transaction
- [ ] Filter transactions
- [ ] Create budget
- [ ] Edit budget
- [ ] Delete budget
- [ ] Dashboard displays correctly
- [ ] Charts render properly
- [ ] Responsive design on mobile
- [ ] Session timeout handling

## 📝 Code Quality

### Best Practices Implemented
- ✅ SOLID principles
- ✅ Separation of concerns
- ✅ DRY (Don't Repeat Yourself)
- ✅ Meaningful naming conventions
- ✅ Comprehensive comments
- ✅ Error handling
- ✅ Input validation
- ✅ Consistent code formatting

## 👥 Contributing

This project is a demonstration of .NET MVC best practices. Feel free to:
- Fork the repository
- Submit pull requests
- Report issues
- Suggest improvements

## 📄 License

This project is created for educational purposes and portfolio demonstration.

## 👤 Author

Created as part of a technical assessment for building a comprehensive Personal Finance Tracker application.

## 🙏 Acknowledgments

- Bootstrap for responsive UI framework
- Chart.js for data visualizations
- Font Awesome for icons
- Microsoft for .NET Framework and Entity Framework

---

**Note**: This application is built with scalability, maintainability, and code readability in mind, following industry best practices and design patterns.
