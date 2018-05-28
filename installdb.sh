#!/bin/bash
export ASPNETCORE_ENVIRONMENT=Development

pushd ./DotNetCoreBlog
dotnet ef database update
cd
exit /b
