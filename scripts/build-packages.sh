#!/bin/bash

# Bash script to build and package CibPay .NET SDK
set -e

# Default values
CONFIGURATION="Release"
OUTPUT_PATH="./packages"
SKIP_TESTS=false
INCLUDE_SYMBOLS=true
PUSH=false
API_KEY=""

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -c|--configuration)
            CONFIGURATION="$2"
            shift 2
            ;;
        -o|--output)
            OUTPUT_PATH="$2"
            shift 2
            ;;
        --skip-tests)
            SKIP_TESTS=true
            shift
            ;;
        --no-symbols)
            INCLUDE_SYMBOLS=false
            shift
            ;;
        --push)
            PUSH=true
            shift
            ;;
        --api-key)
            API_KEY="$2"
            shift 2
            ;;
        -h|--help)
            echo "Usage: $0 [OPTIONS]"
            echo "Options:"
            echo "  -c, --configuration CONFIG    Build configuration (default: Release)"
            echo "  -o, --output PATH             Output path for packages (default: ./artifacts/packages)"
            echo "  --skip-tests                  Skip running tests"
            echo "  --no-symbols                  Don't include symbol packages"
            echo "  --push                        Push packages to NuGet"
            echo "  --api-key KEY                 NuGet API key for pushing"
            echo "  -h, --help                    Show this help message"
            exit 0
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

# Change to project root directory (one level up from scripts)
cd "$(dirname "$0")/.."

echo "🏗️  Building CibPay .NET SDK Packages"
echo "Configuration: $CONFIGURATION"
echo "Output Path: $OUTPUT_PATH"

# Clean previous builds
echo "🧹 Cleaning previous builds..."
dotnet clean --configuration "$CONFIGURATION"

# Create output directory
mkdir -p "$OUTPUT_PATH"

# Restore packages
echo "📦 Restoring packages..."
dotnet restore

# Build solution
echo "🔨 Building solution..."
dotnet build --configuration "$CONFIGURATION" --no-restore

# Run tests (if not skipped)
if [ "$SKIP_TESTS" = false ]; then
    echo "🧪 Running tests..."
    dotnet test --configuration "$CONFIGURATION" --no-build
fi

# Package projects - only the main SDK package (includes Core and Http as dependencies)
projects=(
    "src/CibPay.Sdk/CibPay.Sdk.csproj"
)

echo "📦 Creating NuGet packages..."
for project in "${projects[@]}"; do
    echo "   Packaging $project..."
    
    pack_args=(
        "pack" "$project"
        "--configuration" "$CONFIGURATION"
        "--output" "$OUTPUT_PATH"
        "--no-build"
    )
    
    if [ "$INCLUDE_SYMBOLS" = true ]; then
        pack_args+=("--include-symbols")
    fi
    
    dotnet "${pack_args[@]}"
done

# List created packages
echo "✅ Packages created successfully!"
for package in "$OUTPUT_PATH"/*.nupkg; do
    if [ -f "$package" ]; then
        echo "   📦 $(basename "$package")"
    fi
done

# Push packages (if requested)
if [ "$PUSH" = true ]; then
    if [ -z "$API_KEY" ]; then
        echo "❌ API key is required for pushing packages!"
        exit 1
    fi
    
    echo "🚀 Pushing packages to NuGet..."
    for package in "$OUTPUT_PATH"/*.nupkg; do
        if [[ -f "$package" && ! "$package" =~ symbols ]]; then
            echo "   Pushing $(basename "$package")..."
            dotnet nuget push "$package" --api-key "$API_KEY" --source https://api.nuget.org/v3/index.json
        fi
    done
    
    echo "✅ All packages pushed successfully!"
fi

echo "🎉 Build and packaging completed successfully!"
