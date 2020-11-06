using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using pocker_backend_core.lobby;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request
{
    [Description("Подключение к уже существующей комнате.")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class JoinToLobbyExistence : JoinToLobbyBase
    {
        public JoinToLobbyExistence(string lobbyName, string username) : base(username, lobbyName)
        {
        }

        public override void Run(LobbyService actor)
        {
            var lobby = actor[LobbyName];
            if (lobby == null)
            {
                Directory.Send(new LobbyNotExistsResponse(Requester));
                return;
            }

            if (TryEnterLobby(actor, lobby)) Directory.Send(new SuccessLobbyJoiningResponse(Requester));
        }
    }
}