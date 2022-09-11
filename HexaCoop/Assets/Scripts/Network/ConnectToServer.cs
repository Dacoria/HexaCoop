using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField NameInputField;
    public Button StartOnlineFastButton;
    public Button OfflineButton;

    public bool AutoStartOnline;
    public bool AutoStartOffline;

    public bool HasStartedGame;


    private IEnumerator Start()
    {
        yield return Wait4Seconds.Get(0.1f);
        levelName = null;
        NameInputField.text = NameGen.Get();

        if (AutoStartOnline)
        {
            NameInputField.text = NameGen.Get();
            StartGameOnlineFast();
        }
        else if (AutoStartOffline)
        {
            NameInputField.text = NameGen.Get();
            StartGameOffline();
        }

        
    }

    private string levelName;

    public void StartGameOnClick(string level)
    {
        levelName = level;
        StartGameOnlineFast();
    }


    public ConnectMethod ConnectMethod;

    private int prevLengthName;

    private void Update()
    {        
        if (StartOnlineFastButton.interactable && NameInputField.text.Length == 0)
        {
            StartOnlineFastButton.interactable = false;
        }
        else if (!HasStartedGame && !StartOnlineFastButton.interactable && NameInputField.text.Length != prevLengthName && NameInputField.text.Length > 0)
        {
            StartOnlineFastButton.interactable = true;
            prevLengthName = NameInputField.text.Length;
        }
    }

    public void StartGameOnlineFast()
    {
        if (string.IsNullOrEmpty(NameInputField.text))
        {
            return;
        }
        StartOnlineFastButton.interactable = false;
        StartGame(ConnectMethod.Online_Fast);
    }

    public void StartGameOffline()
    {
        OfflineButton.interactable = false;
        StartGame(ConnectMethod.Offline);
    }

    public void StartGame(ConnectMethod connectMethod)
    {
        HasStartedGame = true;
        ConnectMethod = connectMethod;
        //Textt.GameLocal("StartGame");
        PhotonNetwork.ConnectUsingSettings(PhotonNetwork.PhotonServerSettings.AppSettings, startInOfflineMode: connectMethod == ConnectMethod.Offline);
    }


    public override void OnConnectedToMaster()
    {
        //Textt.GameLocal("OnConnectedToMaster");
        if (ConnectMethod == ConnectMethod.Offline)
        {
            PhotonNetwork.JoinRandomRoom();
            LevelLoader.instance.LoadScene(Settings.DefaultLevelName);
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        //Textt.GameLocal("OnJoinedLobby");

        if (ConnectMethod == ConnectMethod.Online_Fast)
        {
            //PhotonNetwork.JoinRandomOrCreateRoom();
            var roomOptions = new RoomOptions { };
            var punRoomName = string.IsNullOrEmpty(levelName) ? "Randomm" : levelName;
            PhotonNetwork.JoinOrCreateRoom(punRoomName, roomOptions, TypedLobby.Default);
        }        
    }

    public override void OnJoinedRoom()
    {
        //Textt.GameLocal("OnJoinedRoom");
        //Debug.Log("OnJoinedRoom");
        if (ConnectMethod == ConnectMethod.Online_Fast)
        {
            PhotonNetwork.NickName = NameInputField.text;
        }

        var sceneName = GetSceneName();
        PhotonNetwork.LoadLevel(sceneName);
    }

    private string GetSceneName()
    {
        if(string.IsNullOrEmpty(levelName))
        {
            return Settings.DefaultLevelName;
        }

        var buildIndex = SceneUtility.GetBuildIndexByScenePath(levelName);
        if (buildIndex < 0)
        {
            Debug.Log("GEEN SCENE GEVONDEN VOOR " + levelName + ", PAK DE DEFAULT");
            return Settings.DefaultLevelName;
        }

        return levelName;

    }
}

[SerializeField]
public enum ConnectMethod
{
    Online_Fast,
    Offline
}