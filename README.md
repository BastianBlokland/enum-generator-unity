# EnumGenerator-Unity

Unity package tool for generating c# enums based on json input files.

## Description
If you have config in json files it can be nice to have a enum to reference in the code instead of
having to hard code values, this tool allows you to generate that enum.

## Installation
1. Add a reference to this repository to your package dependencies (`Packages/manifest.json`)

    ```
    "dependencies": {
        "com.enumgenerator": "https://github.com/BastianBlokland/enum-generator-unity.git",
        ...
    }
    ```
2. Add the NuGet dependency to your project.

    If your project uses a NuGet package manager you can simply add a dependency to [**EnumGenerator.Core**](https://www.nuget.org/packages/EnumGenerator.Core/).

    If you are not using a NuGet package manager you can simply copy the dll's from the [`.lib`](https://github.com/BastianBlokland/enum-generator-unity/tree/master/.lib) directory to your project.

## Usage
1. Create a `Generator` scriptable-object. (Right mouse the project window -> `Create/EnumGenerator/Generator`)
2. In the `Json File` field of the generator configure the file to base your enum on.
3. Configure the jPaths to point to the right fields in your json. (More info: [**readme**](https://github.com/BastianBlokland/enum-generator-dotnet/blob/master/README.md#json-file-structure))
4. In the `Output Path` field of the generator configure where to output the enum to. (Relative to the `Assets` directory)

## Example
An example of how to integrate this package with a unity project can be found in the [`.example`](https://github.com/BastianBlokland/enum-generator-unity/tree/master/.example) directory.

## Help
More information: [**enum-generator-dotnet**](https://github.com/BastianBlokland/enum-generator-dotnet)
