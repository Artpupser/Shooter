using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string nextSceneName;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }
    public void OnNextScene(string name)
    {
        nextSceneName = name;
        animator.Play("In");
    }
    public void InEnded()
    {
        if (nextSceneName == "") return;
        ChangeScene();
    }
    private void ChangeScene()
    {
        if (SceneManager.GetSceneByName(nextSceneName) != null)
            SceneManager.LoadScene(nextSceneName);
        nextSceneName = "";
    }
    private void ActiveSceneChanged(Scene cur, Scene next)
    {
        SetSettings();
    }
    private void SetSettings()
    {
        Application.targetFrameRate = 60;
    }
}
