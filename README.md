# B2Emulator

An emulator for the Backblaze B2 Cloud Storage service. The goal of the project is to mimic the B2 API to provide a tool that can be used to test applications built to use B2.

**Note:** This project is currently in a pre-alpha state. There is little coverage of the B2 API and no test coverage.

## Current **partially** implemented endpoints

* b2_upload_file ("fileInfo" ignored, error codes don't match B2 API)

## Current **completely** implemented endpoints

* All except b2_upload_file

## Architecture

An ASP.NET Core Web API project.

## Dev Machine Prerequisites

* Any OS supporting the .NET Core SDK is supported.
* The .NET SDK must be installed.

## Build

1. Ensure dependencies are downloaded with `dotnet restore`.
1. Run `dotnet build`.

## Test

TBD

## Run in development

1. From the project directory, run `dotnet run` with the following environment variables set:
* `ASPNETCORE_ENVIRONMENT`: Development
* `ASPNETCORE_URLS`: http://0.0.0.0:DESIRED_PORT
* `B2_CLOUD_STORAGE_ACCOUNT_ID`
* `B2_CLOUD_STORAGE_BUCKET_ID`
