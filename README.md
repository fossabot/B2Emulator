# B2Emulator

This is an emulator for the Backblaze B2 Cloud Storage service. The goal of the project is to mimic the B2 API to provide a tool that can be used to test applications built to use B2.

## Currently implemented endpoints

* b2_upload_file

## Architecture

An ASP.NET Core Web API project.

## Dev Machine Prerequisites

* Any OS supporting the .NET Core SDK is supported.
* The .NET SDK must be installed.
* Heroku CLI must be installed.

## Build

1. Ensure dependencies are downloaded with `dotnet restore`.
1. Run `dotnet build`.

## Run in development

1. Ensure the backing services are running with `docker-compose up` (or, to run in background, `docker-compose up -d`).
1. From the project directory, run `dotnet run` with the following environment variables set:
* `ASPNETCORE_ENVIRONMENT`: Development
* `ASPNETCORE_URLS`: http://0.0.0.0:DESIRED_PORT
* `B2_CLOUD_STORAGE_ACCOUNT_ID`
* `B2_CLOUD_STORAGE_BUCKET_ID`