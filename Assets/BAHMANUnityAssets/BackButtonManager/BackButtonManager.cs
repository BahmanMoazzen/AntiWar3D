using UnityEngine;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] string _HomeSceneName;
    [SerializeField] GameObject _BackPanel;
    
     void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
     void OnEnable()
    {
        _BackPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
            _BackPanel.SetActive(true);
        }
    }
    public void _Exit()
    {
        Application.Quit();
    }
    public void _Home()
    {
        _BackPanel.SetActive(false);
        LoadingManager._INSTANCE._LoadScene(_HomeSceneName);
    }
}
