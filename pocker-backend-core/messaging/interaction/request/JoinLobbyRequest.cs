using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using pocker_backend_core.lobby;
using pocker_backend_core.messaging.interaction.request.@base;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request
{
    [Description("Подключение к уже существующей комнате.")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class JoinLobbyRequest : LobbyRequestBase
    {
        public JoinLobbyRequest(string lobbyName, string username) : base(username, lobbyName)
        {
        }

        public override void Run(LobbyService actor)
        {
            var lobby = actor[LobbyName];
            if (lobby == null)
            {
                SendResponse<FailureLobbyNotExistsResponse>();
                return;
            }

            if (TryEnterLobby(actor, lobby)) SendResponse<SuccessLobbyJoiningResponse>();
        }
    }
}