using pocker_backend_core.lobby;
using pocker_backend_core.messaging;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.frontEnd
{
    public sealed class User
    {
        public static readonly User Invalid = new User();

        private User()
        {
        }

        public static User Create()
        {
            return new User();
        }

        public void UpdateLobby(Lobby lobby)
        {
            Directory.Send(new UpdateLobbyResponse(this, lobby));
        }
    }
}