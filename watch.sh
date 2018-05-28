#!/bin/bash
export ASPNETCORE_ENVIRONMENT=Development

pushd ./DotNetCoreBlog
dotnet watch run
cd
exit /b
