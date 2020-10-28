#!/bin/bash

rm -rf Debug Release && mkdir Debug Release

git clone --no-checkout  https://github.com/sta/websocket-sharp && \
cd websocket-sharp && \
git checkout master websocket-sharp && \
cd .. && \
dotnet restore websocket-sharp-core.csproj && \
dotnet publish websocket-sharp-core.csproj -c Debug && \
cp bin/Debug/netcoreapp3.1/publish/websocket-sharp.dll Debug/websocket-sharp.dll && \
dotnet publish websocket-sharp-core.csproj -c Release && \
cp bin/Release/netcoreapp3.1/publish/websocket-sharp.dll Release/websocket-sharp.dll && \
rm -rf bin obj websocket-sharp
