using pocker_backend_core.helpers;
using pocker_backend_core.messages;
using pocker_backend_core.messages.impl;
using pocker_backend_core.messages.interaction;
using pocker_backend_core.messages.interaction.request;
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