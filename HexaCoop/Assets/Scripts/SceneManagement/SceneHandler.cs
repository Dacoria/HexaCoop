using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        instance = this;
        if (!PhotonNetwork.IsConnected)
        {
            GoToLoadingScene();
        }
    }

    public void GoToLoadingScene()
    {
        Settings.DefaultLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Statics.SCENE_LOADING);
    }

    public void LoadNextScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == Statics.SCENE_LEVEL1)
        {
            PhotonNetwork.LoadLevel(Statics.SCENE_LEVEL2);
        }
        else if (currentSceneName == Statics.SCENE_LEVEL2)
        {
            PhotonNetwork.LoadLevel(Statics.SCENE_LEVEL3);
        }
    }

    public void LoadSameScene()
    {
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }
}