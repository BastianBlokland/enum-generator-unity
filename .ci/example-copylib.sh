#!/bin/bash
set -e
source ./.ci/utils.sh

# --------------------------------------------------------------------------------------------------
# Utility to copy the latest libraries to the example project.
# --------------------------------------------------------------------------------------------------

LIBRARY_DIR=".lib"
EXAMPLE_DIR=".example"

# Verify that commands we depend on are present.
verifyCommand cp
verifyCommand rm

# Verify that both the library and the example directories exist.
if [ ! -d "$LIBRARY_DIR" ]
then
    fail "No library directory found at: '$LIBRARY_DIR'"
fi
if [ ! -d "$EXAMPLE_DIR" ]
then
    fail "No example directory found at: '$EXAMPLE_DIR'"
fi

# Remove existing libraries.
rm -rf "$EXAMPLE_DIR/Assets/lib"

# Copy the libraries over.
ensureDir "$EXAMPLE_DIR/Assets/lib"
cp -f "$LIBRARY_DIR"/*.dll "$EXAMPLE_DIR/Assets/lib"

info "Copied libraries to example"
exit 0
