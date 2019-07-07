#!/bin/bash
set -e
source ./.ci/utils.sh

# --------------------------------------------------------------------------------------------------
# Utility to download the latest NuGet dependencies.
# --------------------------------------------------------------------------------------------------

NUGET_PACKAGE="EnumGenerator.Core"
NUGET_DIR=".nuget"
LIBRARY_DIR=".lib"

# Verify that commands we depend on are present.
verifyCommand nuget
verifyCommand cp
verifyCommand mkdir
verifyCommand rm
verifyCommand basename

# Clear output.
rm -rf "$NUGET_DIR"
rm -rf "$LIBRARY_DIR"
ensureDir "$LIBRARY_DIR"

info "Fetching nuget packages"
withRetry nuget install "$NUGET_PACKAGE" -OutputDirectory "$NUGET_DIR"

saveDll ()
{
    local dllPath="$1"
    local packageName="$2"
    info "Processing dll: $dllPath for package: $packageName"
    ensureDir "$LIBRARY_DIR"
    cp -f "$dllPath" "$LIBRARY_DIR"
}

processPackage ()
{
    local packageName="$(basename $1)"
    info "Processing package: $packageName"

    local netstandard2Dir="$1/lib/netstandard2.0/"
    if [ ! -d "$netstandard2Dir" ]
    then
        fail "No 'netstandard2.0' library found for package: $packageName"
    fi

    echo "$packageName" >> "$LIBRARY_DIR/manifest.txt"
    for dllPath in "$netstandard2Dir"*.dll
    do
        saveDll "$dllPath" "$packageName"
    done
}

# Extract all netstandard2.0 dll's from the packages.
for packageDir in "$NUGET_DIR"/*
do
    processPackage $packageDir
done

info "Finished fetching dependencies"
exit 0
