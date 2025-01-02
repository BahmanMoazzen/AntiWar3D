using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingAPISceneManager : MonoBehaviour
{
    public static RankingAPISceneManager _INSTANCE;

    public Image[] _Flags;
    public Text[] _Ranks, _Scores, _Names;
    public Text[] _MyRank;
    [SerializeField] GameObject _RankPanel,_SummeryPanel,_AddRankButton;
    [SerializeField] Button _GetRankBotton;
    [SerializeField] SaveableItem _AddScorePower;
    public void _SetScoreNow()
    {
        
        
    }

    public void _GetRanks()
    {
        
    }
    public void _GetMyRanks()
    {
       
    }
    private void Start()
    {
        RankingAPI._INSTANCE._INIT("");
    }
    private void Awake()
    {
        if(_INSTANCE == null)
        {
            _INSTANCE = this;
        }
    }
    private void OnEnable()
    {
        RankingAPI.OnInitSuccessful += _OnInitSuccessful;
        RankingAPI.OnInitFailed += _OnInitFailed;

        RankingAPI.OnFlagParseError += _FlagParseError;
        RankingAPI.OnFlagParseSuccessful += _FlagParseSuccessful;

        RankingAPI.OnGetRankFailed += _GetRankFailed;
        RankingAPI.OnGetRankSuccessful += _GetRankSuccessful;

        RankingAPI.OnGetMyRankFailed += _GetMyRankFailed;
        RankingAPI.OnGetMyRankSuccessful += _GetMyRankSuccessful;

        RankingAPI.OnSetMyScoreFailed += _SetMyScoreFailed;
        RankingAPI.OnSetMyScoreSuccessful += _SetMyScoreSuccessful;

        RankingAPI.OnListPopulateSuccessful += _PopulateListSuccessful;
    }
    private void OnDisable()
    {
        RankingAPI.OnInitSuccessful -= _OnInitSuccessful;
        RankingAPI.OnInitFailed -= _OnInitFailed;

        RankingAPI.OnFlagParseError -= _FlagParseError;
        RankingAPI.OnFlagParseSuccessful -= _FlagParseSuccessful;

        RankingAPI.OnGetRankFailed -= _GetRankFailed;
        RankingAPI.OnGetRankSuccessful -= _GetRankSuccessful;

        RankingAPI.OnGetMyRankFailed -= _GetMyRankFailed;
        RankingAPI.OnGetMyRankSuccessful -= _GetMyRankSuccessful;

        RankingAPI.OnSetMyScoreFailed -= _SetMyScoreFailed;
        RankingAPI.OnSetMyScoreSuccessful -= _SetMyScoreSuccessful;

        RankingAPI.OnListPopulateSuccessful -= _PopulateListSuccessful;
    }
    
    void _OnInitSuccessful()
    {
        
    }
    void _OnInitFailed(string iMessage)
    {

    }

    void _FlagParseError(string iMessage)
    {

    }
    void _FlagParseSuccessful()
    {

    }

    void _GetRankFailed(string iMessage)
    {
        
    }
    void _GetRankSuccessful(RankingAPI.RankingList iList)
    {

    }

    void _GetMyRankFailed(string iMessage)
    {
        
    }
    void _GetMyRankSuccessful(int iRank)
    {
       
    }

    void _SetMyScoreFailed(string iMessage)
    {
        
    }
    void _SetMyScoreSuccessful()
    {
       
    }

    void _PopulateListSuccessful()
    {
        
    }
    //private void Awake()
    //{
    //    RankingAPI.onConnectionError += test;
    //}

    //private void test(string error)
    //{
    //    Debug.Log(error);
    //}
}
