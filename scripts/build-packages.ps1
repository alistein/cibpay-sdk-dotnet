# PowerShell script to build and package CibPay .NET SDK
param(
    [string]$Configuration = "Release",
    [string]$OutputPath = "./packages",
    [switch]$SkipTests = $false,
    [switch]$IncludeSymbols = $true,
    [switch]$Push = $false,
    [string]$ApiKey = ""
)

# Change to project root directory (one level up from scripts)
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location (Join-Path $scriptPath "..")

Write-Host "🏗️  Building CibPay .NET SDK Packages" -ForegroundColor Green
Write-Host "Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "Output Path: $OutputPath" -ForegroundColor Cyan

# Clean previous builds
Write-Host "🧹 Cleaning previous builds..." -ForegroundColor Yellow
dotnet clean --configuration $Configuration

# Create output directory
New-Item -ItemType Directory -Force -Path $OutputPath | Out-Null

# Restore packages
Write-Host "📦 Restoring packages..." -ForegroundColor Yellow
dotnet restore

# Build solution
Write-Host "🔨 Building solution..." -ForegroundColor Yellow
dotnet build --configuration $Configuration --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    exit 1
}

# Run tests (if not skipped)
if (-not $SkipTests) {
    Write-Host "🧪 Running tests..." -ForegroundColor Yellow
    dotnet test --configuration $Configuration --no-build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Tests failed!" -ForegroundColor Red
        exit 1
    }
}

# Package projects - only the main SDK package (includes Core and Http as dependencies)
$projects = @(
    "src/CibPay.Sdk/CibPay.Sdk.csproj"
)

Write-Host "📦 Creating NuGet packages..." -ForegroundColor Yellow
foreach ($project in $projects) {
    $packageArgs = @(
        "pack", $project,
        "--configuration", $Configuration,
        "--output", $OutputPath,
        "--no-build"
    )
    
    if ($IncludeSymbols) {
        $packageArgs += "--include-symbols"
    }
    
    Write-Host "   Packaging $project..." -ForegroundColor Cyan
    & dotnet $packageArgs
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Failed to package $project!" -ForegroundColor Red
        exit 1
    }
}

# List created packages
Write-Host "✅ Packages created successfully!" -ForegroundColor Green
Get-ChildItem -Path $OutputPath -Filter "*.nupkg" | ForEach-Object {
    Write-Host "   📦 $($_.Name)" -ForegroundColor Cyan
}

# Push packages (if requested)
if ($Push) {
    if ([string]::IsNullOrEmpty($ApiKey)) {
        Write-Host "❌ API key is required for pushing packages!" -ForegroundColor Red
        exit 1
    }
    
    Write-Host "🚀 Pushing packages to NuGet..." -ForegroundColor Yellow
    Get-ChildItem -Path $OutputPath -Filter "*.nupkg" | Where-Object { $_.Name -notlike "*symbols*" } | ForEach-Object {
        Write-Host "   Pushing $($_.Name)..." -ForegroundColor Cyan
        dotnet nuget push $_.FullName --api-key $ApiKey --source https://api.nuget.org/v3/index.json
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Failed to push $($_.Name)!" -ForegroundColor Red
            exit 1
        }
    }
    
    Write-Host "✅ All packages pushed successfully!" -ForegroundColor Green
}

Write-Host "🎉 Build and packaging completed successfully!" -ForegroundColor Green
