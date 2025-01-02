using UnityEngine;

using System;
using System.Collections;

public class AdvertismentManager : MonoBehaviour
{
    public static AdvertismentManager _INSTANCE;

    //const string _GOOGLEADUNITID = "ca-app-pub-5781048219411200/1975031269";
    //const string _GOOGLEADAPIID = "ca-app-pub-5781048219411200~5746809214";

    const string _UnityAppID = "344c3065-a5c2-4030-8382-4ce17d93611e";

    public delegate void addShown();
    public delegate void addFailed();
    public static event addShown OnAddShowedSuccessful;
    public static event addFailed OnAddFailed;


    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;



    private void Awake()
    {
        if (_INSTANCE == null)
        {
            _INSTANCE = this;
            DontDestroyOnLoad(this);
        }
        

    }
    IEnumerator _ShowAddRoutine()
    {
        yield return 0;
        OnAddShowedSuccessful?.Invoke();



    }
    public void _ShowAdd()
    {
        StartCoroutine(_ShowAddRoutine());

    }
}
