##  DEPLOYMENT GUIDE
# Personal Finance Tracker - Deployment Instructions

## 🚀 Deployment Options

### Option 1: IIS (Internet Information Services)

#### Prerequisites
- Windows Server or Windows 10/11 Pro
- IIS installed with ASP.NET 4.7.2 support
- SQL Server (Express or Standard edition)

#### Steps

1. **Prepare the Application**
   ```powershell
   # Build in Release mode
   msbuild PersonalFinanceTracker.sln /p:Configuration=Release
   ```

2. **Publish the Application**
   - Right-click on project in Visual Studio
   - Select "Publish"
   - Choose "Folder" as publish target
   - Select output folder
   - Click "Publish"

3. **Configure IIS**
   ```
   - Open IIS Manager
   - Right-click on "Sites" > "Add Website"
   - Site name: PersonalFinanceTracker
   - Physical path: [Published folder path]
   - Port: 80 (or custom port)
   - Click OK
   ```

4. **Configure Database**
   - Update connection string in Web.config
   - Point to production SQL Server
   - Ensure database is created
   - Run initial migration if needed

5. **Set Permissions**
   ```
   - Grant IIS_IUSRS read/execute permissions to the application folder
   - Grant modify permissions to App_Data folder
   ```

6. **Test the Application**
   - Browse to http://localhost or your domain
   - Create a test account
   - Verify all features work

### Option 2: Azure App Service

#### Steps

1. **Create Azure Resources**
   ```
   - Create Azure SQL Database
   - Create App Service Plan
   - Create Web App
   ```

2. **Configure Connection String**
   - In Azure Portal, go to Web App
   - Settings > Configuration > Connection strings
   - Add "DefaultConnection" with Azure SQL connection string

3. **Deploy via Visual Studio**
   ```
   - Right-click project > Publish
   - Select Azure > Azure App Service
   - Sign in to Azure
   - Select your App Service
   - Publish
   ```

4. **Alternative: Deploy via Git**
   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   git remote add azure <your-azure-git-url>
   git push azure master
   ```

### Option 3: Local Testing (Development)

1. **Using Visual Studio**
   ```
   - Press F5 to run in debug mode
   - LocalDB will be used automatically
   - Application runs on localhost
   ```

2. **Using IIS Express**
   ```
   - Ctrl + F5 to run without debugging
   - Faster startup
   - Good for testing
   ```

## 🗄️ Database Configuration

### LocalDB (Development)
```xml
<add name="DefaultConnection" 
     connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PersonalFinanceTracker.mdf;Integrated Security=True" 
     providerName="System.Data.SqlClient" />
```

### SQL Server (Production)
```xml
<add name="DefaultConnection" 
     connectionString="Server=YOUR_SERVER;Database=PersonalFinanceTracker;User Id=YOUR_USER;Password=YOUR_PASSWORD;" 
     providerName="System.Data.SqlClient" />
```

### Azure SQL Database
```xml
<add name="DefaultConnection" 
     connectionString="Server=tcp:yourserver.database.windows.net,1433;Database=PersonalFinanceTracker;User ID=yourusername;Password=yourpassword;Encrypt=True;TrustServerCertificate=False;" 
     providerName="System.Data.SqlClient" />
```

## ⚙️ Configuration Checklist

### Before Deployment
- [ ] Update connection string
- [ ] Set debug="false" in Web.config
- [ ] Change customErrors mode to "On"
- [ ] Review security settings
- [ ] Update authentication timeout
- [ ] Configure HTTPS/SSL
- [ ] Set up database backups
- [ ] Test on staging environment

### Security Configuration
```xml
<!-- Production Web.config settings -->
<compilation debug="false" targetFramework="4.7.2" />
<customErrors mode="On" defaultRedirect="~/Error" />
<sessionState timeout="20" />
```

## 🔐 SSL/HTTPS Configuration

### IIS SSL Setup
1. Obtain SSL certificate
2. In IIS Manager:
   - Select your site
   - Bindings > Add
   - Type: https
   - Port: 443
   - SSL certificate: Select your certificate
3. Force HTTPS redirect (optional)

### web.config HTTPS Redirect
```xml
<system.webServer>
  <rewrite>
    <rules>
      <rule name="HTTP to HTTPS redirect" stopProcessing="true">
        <match url="(.*)" />
        <conditions>
          <add input="{HTTPS}" pattern="off" />
        </conditions>
        <action type="Redirect" url="https://{HTTP_HOST}/{R:1}" redirectType="Permanent" />
      </rule>
    </rules>
  </rewrite>
</system.webServer>
```

## 📊 Performance Optimization

### Enable Bundling and Minification
```csharp
// In BundleConfig.cs (already configured)
BundleTable.EnableOptimizations = true;
```

### Enable Output Caching
```csharp
// On controller actions
[OutputCache(Duration = 60, VaryByParam = "none")]
public ActionResult Index()
{
    // ...
}
```

### Database Optimization
- Enable Connection Pooling
- Use proper indexes
- Optimize queries
- Consider caching frequently accessed data

## 🔍 Troubleshooting

### Common Issues

1. **500 Internal Server Error**
   - Check event logs
   - Enable detailed errors
   - Verify permissions
   - Check connection string

2. **Database Connection Failed**
   - Verify connection string
   - Check firewall rules
   - Ensure SQL Server is running
   - Verify credentials

3. **Static Files Not Loading**
   - Check file permissions
   - Verify IIS static content module
   - Check web.config handlers

4. **Session State Issues**
   - Verify session state mode
   - Check timeout settings
   - Ensure cookies are enabled

## 📝 Maintenance

### Regular Tasks
- Monitor application logs
- Review error logs
- Backup database regularly
- Update security patches
- Monitor performance
- Check disk space

### Database Backup Script
```sql
-- SQL Server backup
BACKUP DATABASE PersonalFinanceTracker
TO DISK = 'C:\Backups\PersonalFinanceTracker.bak'
WITH FORMAT, INIT, NAME = 'Full Backup';
```

## 🌐 Environment Variables

### Development
- Debug: true
- Database: LocalDB
- Logging: Verbose

### Staging
- Debug: false
- Database: Test SQL Server
- Logging: Warnings and Errors

### Production
- Debug: false
- Database: Production SQL Server
- Logging: Errors only

## 📞 Support

For deployment issues:
1. Check application logs
2. Review IIS logs
3. Check SQL Server logs
4. Verify configuration

---

**Happy Deploying! 🎉**
