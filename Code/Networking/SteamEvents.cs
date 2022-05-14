using Steamworks;
using Steamworks.Data;

namespace Dungeons.Networking
{
    public class SteamEvents
    {
        public SteamEvents()
        {
            SteamFriends.OnGameLobbyJoinRequested += SteamFriends_OnGameLobbyJoinRequested;
        }

        public void SteamFriends_OnGameLobbyJoinRequested(Lobby lobby, SteamId id)
        {
        }
    }
}