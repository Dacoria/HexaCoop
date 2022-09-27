using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator Transition;

    private float TransitionTime = 0.5f;
    private Canvas canvas;

    public static LevelLoader instance;

    private void Awake()
    {
        instance = this;
        Transition = GetComponentInChildren<Animator>(true);
        Transition.gameObject.SetActive(true);
        canvas = FindObjectOfType<Canvas>();
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        var animStateInfo = Transition.GetCurrentAnimatorStateInfo(0);
        var NTime = animStateInfo.normalizedTime;

        if (NTime > 1.0f)
        {
            canvas.gameObject.SetActive(true);
        }
    }



    private string sceneName;
    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;
        StartCoroutine(CR_LoadAnimation(LoadScene));
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }


    public void LoadSceneAnimation(Action callback)
    {
        StartCoroutine(CR_LoadAnimation(callback));
    }

    private IEnumerator CR_LoadAnimation(Action Callback)
    {
        Transition.SetTrigger(Statics.ANIMATION_TRIGGER_START_ANIMATION_SCENE);
        yield return Wait4Seconds.Get(TransitionTime);
        Callback();
    }
}

public static class LevelExt
{
    public static int GetLevelNr(this string levelName)
    {
        var numberValue = levelName.ToUpper().Replace("LEVEL", "");
        if (int.TryParse(numberValue, out var numberOfLevel))
        {
            return numberOfLevel;
        }

        return -1;
    }
}
