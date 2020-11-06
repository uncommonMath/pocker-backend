using pocker_backend_core.helper;
using pocker_backend_core.messaging;
using pocker_backend_core.messaging.impl;
using pocker_backend_core.messaging.interaction.request;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace pocker_backend_core.frontEnd
{
    public class Connection : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            base.OnOpen();
            Directory.Send(new NewConnectionRequest(this));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data;
            var msgObj = JsonHelper.Deserialize<IRequest>(msg);
            Directory.Send(new ReceivedRequestMessage(this, msgObj));
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            Directory.Send(new ConnectionLostMessage(this));
        }

        public void SendResponse(AbstractResponse response)
        {
            var jsonResponse = JsonHelper.Serialize(response);
            Send(jsonResponse);
        }
    }
}