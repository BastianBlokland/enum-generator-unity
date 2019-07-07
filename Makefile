.PHONY: update-dependencies example-edit
default: update-dependencies

# --------------------------------------------------------------------------------------------------
# MakeFile used as a convient way for executing development utlitities.
# --------------------------------------------------------------------------------------------------

update-dependencies:
	./.ci/update-dependencies.sh
	./.ci/example-copylib.sh

example-edit:
	./.ci/example-edit.sh
