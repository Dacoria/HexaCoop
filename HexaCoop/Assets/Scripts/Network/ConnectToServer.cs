using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField NameInputField;
    public Button StartOnlineFastButton;
    public Button OfflineButton;

    public bool AutoStartOnline;
    public bool AutoStartOffline;

    private IEnumerator Start()
    {
        yield return Wait4Seconds.Get(0.1f);

        if(AutoStartOnline)
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

    private bool HasStartedGame;

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
        //Debug.Log("StartGame " + connectMethod);
        PhotonNetwork.ConnectUsingSettings(PhotonNetwork.PhotonServerSettings.AppSettings, startInOfflineMode: connectMethod == ConnectMethod.Offline);
    }


    public override void OnConnectedToMaster()
    {
        //Debug.Log("OnConnectedToMaster");
        if (ConnectMethod == ConnectMethod.Offline)
        {
            PhotonNetwork.JoinRandomRoom();
            LevelLoader.instance.LoadScene(Settings.LevelName);
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        //Debug.Log("OnJoinedLobby");

        if (ConnectMethod == ConnectMethod.Online_Fast)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }        
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("OnJoinedRoom");
        if (ConnectMethod == ConnectMethod.Online_Fast)
        {
            PhotonNetwork.NickName = NameInputField.text;
        }

        PhotonNetwork.LoadLevel(Settings.LevelName);
    }
}

[SerializeField]
public enum ConnectMethod
{
    Online_Fast,
    Offline
}