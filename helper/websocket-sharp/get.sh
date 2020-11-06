#!/bin/bash

rm -rf Debug Release && mkdir Debug Release

git clone --no-checkout  https://github.com/sta/websocket-sharp && \
cd websocket-sharp && \
git checkout master websocket-sharp && \
cd websocket-sharp && \
sed 's|_callback.BeginInvoke (this, ar => _callback.EndInvoke (ar), null);|System.Threading.Tasks.Task.Run(() => _callback.Invoke(this));|g' Net/HttpStreamAsyncResult.cs -i && \
(cat WebSocket.cs | tr '\n' '\r' | sed -e 's|closer.BeginInvoke (\r        payloadData, send, receive, received, ar => closer.EndInvoke (ar), null\r      );|System.Threading.Tasks.Task.Run(() => closer.Invoke(payloadData, send, receive, received));|g' -e 's|_message.BeginInvoke (e, ar => _message.EndInvoke (ar), null);|System.Threading.Tasks.Task.Run(() => _message.Invoke(e));|g' -e 's|sender.BeginInvoke (\r        opcode,\r        stream,\r        ar => {\r          try {\r            var sent = sender.EndInvoke (ar);\r            if (completed != null)\r              completed (sent);\r          }\r          catch (Exception ex) {\r            _logger.Error (ex.ToString ());\r            error (\r              "An error has occurred during the callback for an async send.",\r              ex\r            );\r          }\r        },\r        null\r      );|System.Threading.Tasks.Task.Run(() => sender.Invoke(opcode, stream)).ContinueWith(x => {if (x.IsCompletedSuccessfully) completed(x.Result);});|g' -e 's|acceptor.BeginInvoke (\r        ar => {\r          if (acceptor.EndInvoke (ar))\r            open ();\r        },\r        null\r      );|System.Threading.Tasks.Task.Run(acceptor.Invoke).ContinueWith(x => {if (x.IsCompletedSuccessfully) open();});|g' -e 's|connector.BeginInvoke (\r        ar => {\r          if (connector.EndInvoke (ar))\r            open ();\r        },\r        null\r      );|System.Threading.Tasks.Task.Run(connector.Invoke).ContinueWith(x => {if (x.IsCompletedSuccessfully) open();});|g' | tr '\r' '\n') > WebSocket_.cs && mv WebSocket_.cs WebSocket.cs && \
cd ../.. && \
dotnet restore websocket-sharp-core.csproj && \
dotnet publish websocket-sharp-core.csproj -c Debug && \
cp bin/Debug/netcoreapp3.1/publish/websocket-sharp.dll Debug/websocket-sharp.dll && \
dotnet publish websocket-sharp-core.csproj -c Release && \
cp bin/Release/netcoreapp3.1/publish/websocket-sharp.dll Release/websocket-sharp.dll && \
rm -rf bin obj websocket-sharp
