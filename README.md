# B2Emulator

| Branch | CircleCI | FOSSA Status |
|--------|----------|--------------|
| Master | [![CircleCI](https://circleci.com/gh/welkie/B2Emulator.svg?style=svg)](https://circleci.com/gh/welkie/B2Emulator) | [![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fwelkie%2FB2Emulator.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2Fwelkie%2FB2Emulator?ref=badge_shield) |

An emulator for the Backblaze B2 Cloud Storage service. The goal of the project is to mimic the B2 API to provide a tool that can be used to test applications built to use B2.

**Note:** This project is currently in an early state. There is little coverage of the B2 API.

## Current **partially** implemented endpoints

* b2_upload_file - "fileInfo" ignored, error codes don't match B2 API
* b2_download_file_by_id - "fileInfo" ignored, error codes don't match B2 API

## Running with Docker

The latest version is published at the [mwelke/b2-emulator](https://hub.docker.com/r/mwelke/b2-emulator/) Docker repository.

To run the image locally:

```yaml
docker run --name my-b2-emulator -p 5000:5000 mwelke/b2-emulator:latest
```

## Choosing a file storage provider

Set the `PROVIDER` environment variable to choose which provider is loaded at runtime. Options are:

* MEMORY - An in memory provider. Stores the files in a concurrent dictionary data structure in memory until the process exits. **Warning: Files will not be persisted once process exits**

## Dev Machine Prerequisites

* The .NET SDK must be installed.

## Build

1. Run `dotnet build` from the project/solution directory.

## Test

1. Run `dotnet test` from the `test/B2Emulator.Tests` subdirectory.

## Run in development

From the `src/B2Emulator` subdirectory, run `dotnet run` with the following environment variables set:

* `ASPNETCORE_ENVIRONMENT` - Development
* `ASPNETCORE_URLS` - http://0.0.0.0:DESIRED_PORT
* `B2_CLOUD_STORAGE_ACCOUNT_ID` - ACCOUNT_ID
* `B2_CLOUD_STORAGE_BUCKET_ID` - BUCKET_ID

Note that the B2 credentials are used to fake authorization endpoints and bucket behavior. They are not transmitted.


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fwelkie%2FB2Emulator.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fwelkie%2FB2Emulator?ref=badge_large)
