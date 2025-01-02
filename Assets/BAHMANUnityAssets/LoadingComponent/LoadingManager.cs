/*
 * 
 * Loading Manager Version 1.0
 * 
 */


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    
    
    // instance to call Load manager
    public static LoadingManager _INSTANCE;

    

    // public procedures which shoud be replaced by unity load scene
    public void _LoadScene(int iSceneIndex)
    {
        StartCoroutine(_loadSceneRoutin(iSceneIndex));

    }
    public void _LoadScene(string iSceneName)
    {

        StartCoroutine(_loadSceneRoutin(iSceneName));

    }
    // manually hides load panel
    public void _LoadCompeleted()
    {
        _HideLoadPanel();
    }

    #region private

    [SerializeField] GameObject _LoadingSlider;

    // panel to show or hide entire loading 
    [SerializeField] GameObject _LoadPanel;

    // show on start of scene
    [SerializeField] bool _ShowOnStartup = true;

    // autohide after load
    [SerializeField] bool _AutoHideOnLoad = true;




    void Awake()
    {
        if (_INSTANCE == null)
            _INSTANCE = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {

        SceneManager.sceneLoaded += _sceneLoadedComplete;
        SceneManager.activeSceneChanged += _sceneChanged;
        SceneManager.sceneUnloaded += _sceneUnload;

    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= _sceneLoadedComplete;
        SceneManager.activeSceneChanged -= _sceneChanged;
        SceneManager.sceneUnloaded -= _sceneUnload;

    }
    void _sceneChanged(Scene iCurrent, Scene iNext)
    {

    }
    void _sceneUnload(Scene iCurrentScene)
    {

    }
    void _sceneLoadedComplete(Scene iScene, LoadSceneMode iMode)
    {
        _LoadPanel.SetActive(_ShowOnStartup);
        if (_AutoHideOnLoad)
        {
            _HideLoadPanel();
        }
    }
    IEnumerator _loadSceneRoutin(int iSceneIndex)
    {


        yield return 0;
        _ShowLoadPanel();
        yield return 0;
        AsyncOperation asyncLoad =  SceneManager.LoadSceneAsync(iSceneIndex, LoadSceneMode.Single);
        _LoadingSlider.SetActive(true);
        while (!asyncLoad.isDone)
        {
            _LoadingSlider.GetComponent<Slider>().value = asyncLoad.progress;
            yield return null;

        }
        _LoadingSlider.SetActive(false);

    }
    IEnumerator _loadSceneRoutin(string iSceneName)
    {

        yield return 0;
        _ShowLoadPanel();
        yield return 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(iSceneName, LoadSceneMode.Single);
        _LoadingSlider.SetActive(true);
        while (!asyncLoad.isDone)
        {
            _LoadingSlider.GetComponent<Slider>().value = asyncLoad.progress;
            yield return null;
        }
        _LoadingSlider.SetActive(false);

    }
    void _ShowLoadPanel()
    {
        _LoadPanel.SetActive(true);
    }
    void _HideLoadPanel()
    {
        _LoadPanel.SetActive(false);
    }


    #endregion


}
