# iprep 
### ![.NET Core](https://github.com/jbies121/iprep/workflows/.NET%20Core/badge.svg) master
### ![.NET Core](https://github.com/jbies121/iprep/workflows/.NET%20Core/badge.svg?branch=key-management) key-management
## IP Reputation tool for .NET Core 3.1
This tool was developed with the blue team in mind to quickly determine IP origin and reputation.
Using user supplied API keys, IP addresses and ranges can be checked against reputation services quickly and consumed in a usable way.
Supporting multiple APIs allows users to access fresh and diverse reputation sources, and provides some fault tolerance when services are interrupted.

This project aims to stay lightweight and platform independent.

### Supported APIs
- AbuseIPDB
  - CHECK endpoint

### Usage
From a built executable:
```powershell
.\iprep.exe [ip] [info]
```

From within the project directory:
```powershell 
dotnet run [ip] [info]
```

### 'info' options currently available
-isPublic
-ipVersion
-isWhitelisted
-countryCode
-usageType
-isp
-domain
-hostnames
-countryName
-totalReports
-numDistinctUsers
-lastReportedAt
