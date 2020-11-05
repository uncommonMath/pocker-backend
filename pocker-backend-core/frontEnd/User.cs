using pocker_backend_core.lobby;
using pocker_backend_core.messages;
using pocker_backend_core.messages.interaction.response;

namespace pocker_backend_core.frontEnd
{
    public sealed class User
    {
        private User()
        {
        }

        public static User Create()
        {
            return new User();
        }

        public void UpdateLobby(Lobby lobby)
        {
            Directory.Send(new LobbyUpdateResponse(this, lobby));
        }
    }
}