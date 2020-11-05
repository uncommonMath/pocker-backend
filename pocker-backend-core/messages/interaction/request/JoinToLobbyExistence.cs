using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using pocker_backend_core.lobby;
using pocker_backend_core.messages.interaction.response;

namespace pocker_backend_core.messages.interaction.request
{
    [Description("Подключение к уже существующей комнате.")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class JoinToLobbyExistence : JoinToLobbyBase
    {
        public JoinToLobbyExistence(string lobbyName, string username) : base(lobbyName, username)
        {
        }

        public override void Run(LobbyService actor)
        {
            var lobby = actor[LobbyName];
            if (lobby == null)
            {
                Directory.Send(new LobbyNotExists(Requester));
                return;
            }

            if (!actor.UserJoin(Requester, Username, lobby)) throw new ApplicationException("unexpected");

            Directory.Send(new SuccessLobbyJoiningResponse(Requester));
        }
    }
}