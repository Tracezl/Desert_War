using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
public class NetworkHook : Prototype.NetworkLobby.LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        PlayerState player = gamePlayer.GetComponent<PlayerState>();

        //spaceship.name = lobby.name;
        //spaceship.color = lobby.playerColor;
        player.player = lobby.playerName;
        player.playerColor = lobby.playerColor;

    }
}