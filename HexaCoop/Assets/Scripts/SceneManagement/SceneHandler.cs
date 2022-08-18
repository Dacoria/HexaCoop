using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public bool DebugOffline = true;

    void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (DebugOffline)
            {
                var myAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
                PhotonNetwork.ConnectUsingSettings(myAppSettings, true);
                PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: new RoomOptions
                {
                    MaxPlayers = 4,
                });
            }
            else
            {
                GoToLoadingScene();
            }
        }
    }

    public void GoToLoadingScene()
    {
        Settings.LevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Statics.SCENE_LOADING);
    }
}