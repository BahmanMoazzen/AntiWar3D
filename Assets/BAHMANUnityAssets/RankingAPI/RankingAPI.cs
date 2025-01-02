using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RankingAPI : MonoBehaviour
{
    // accessable instance for RankingAPI
    public static RankingAPI _INSTANCE;
    // version number
    public static string _VersionName = "2.0";



    // percistent criteria for API
    public static byte _RANKINGAPIMINNAMELENGTH = 3;
    public static byte _RANKINGAPIMAXNAMELENGTH = 25;




    [Header("Record Display Setting:")]
    [Range(5, 15)]
    [SerializeField] int _MaxRecord;
    [SerializeField] bool _DontDestroyOnLoad = false;

    [Header("Automatic Setting:")]
    [SerializeField] string _UniqueIdentifier;
    [SerializeField] string _UserName;
    
    private string[] _resultText = { "ثبت امتیاز با موفقیت انجام شد!", "به روز رسانی کاربر با موفقیت انجام شد", "ارتباط با پایگاه داده بر قرار نشد", "داده دریافت شده معتبر نیست", "تنظیمات پایه انجام نشده است", "ارتباط با سرور برقرار نشد" };
    private const string _setRankURL = "https://spssland.ir/unity/set_rank.php";
    private const string _getRankURL = "https://spssland.ir/unity/get_rank.php";
    private const string _getMyRankURL = "https://spssland.ir/unity/my_rank.php";
    private const string _ipLocatorURL = "http://ip-api.com/json/";
    private const string _TagUsername = "RankingAPIUsername";
    private const string _TagUserPrefix = "USER";
    
    bool _canPerform = false;
    
    public delegate void InitSuccessful();
    public delegate void InitFailed(string iMessage);

    public delegate void FlagParseError(string iMessage);
    public delegate void FlagParseSuccessful();

    public delegate void GetRankFailed(string iMessage);
    public delegate void GetRankSuccessful(RankingList iList);

    public delegate void GetMyRankFailed(string iMessage);
    public delegate void GetMyRankSuccessful(int iRank);

    public delegate void SetMyScoreFailed(string iMessage);
    public delegate void SetMyScoreSuccessful();

    public delegate void ListPopulateSuccessful();


    public static event InitSuccessful OnInitSuccessful;
    public static event InitFailed OnInitFailed;

    public static event FlagParseError OnFlagParseError;
    public static event FlagParseSuccessful OnFlagParseSuccessful;

    public static event GetRankFailed OnGetRankFailed;
    public static event GetRankSuccessful OnGetRankSuccessful;

    public static event GetMyRankFailed OnGetMyRankFailed;
    public static event GetMyRankSuccessful OnGetMyRankSuccessful;

    public static event SetMyScoreFailed OnSetMyScoreFailed;
    public static event SetMyScoreSuccessful OnSetMyScoreSuccessful;

    public static event ListPopulateSuccessful OnListPopulateSuccessful;


    public void _INIT(string iKeyCode)
    {
        _canPerform = true;
        OnInitSuccessful?.Invoke();
    }

    private void Awake()
    {

        StartCoroutine(_startupRoutine());
        if (_DontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
    IEnumerator _startupRoutine()
    {
        if (_INSTANCE == null)
        {
            _INSTANCE = this;
        }
        yield return 0;
        // setting app identifier if not given by user
        if (_UniqueIdentifier.Equals(string.Empty))
            _UniqueIdentifier = Application.identifier;
        // check if the username set
        if (_UserName.Equals(string.Empty))
        {
            // check if the username Exists
            _UserName = PlayerPrefs.GetString(_TagUsername, "");

            // if user dosn't exists create new one
            if (_UserName.Equals(string.Empty))
            {
                _UserName = string.Format("{0}{1}", _TagUserPrefix, UnityEngine.Random.Range(0, int.MaxValue).ToString());
                PlayerPrefs.SetString(_TagUsername, _UserName);
            }
        }
        _canPerform = true;
        yield return 0;
    }


    public void _SetRank(string iDisplayName, int iScore)
    {
        if (_canPerform)
        {
            //defining data
            DataStructure ds = new DataStructure();
            if (iDisplayName.Length > RankingAPI._RANKINGAPIMAXNAMELENGTH)
            {
                iDisplayName = iDisplayName.Substring(0, _RANKINGAPIMAXNAMELENGTH);
            }
            else if (iDisplayName.Length < RankingAPI._RANKINGAPIMINNAMELENGTH)
            {
                iDisplayName = string.Format("{0}_{1}", _TagUserPrefix, iDisplayName);
            }
            ds.display_name = iDisplayName;
            ds.pkg_id = _UniqueIdentifier;
            ds.user_name = _UserName;
            ds.score = iScore;

            // starting set rank coroutine
            StartCoroutine(_SetRankRoutine(_setRankURL, ds));
        }
        else
        {
            OnInitFailed?.Invoke(_resultText[4]);
        }

    }

    IEnumerator _SetRankRoutine(string iURL, DataStructure iDataStructure)
    {
        // setting content of request
        UnityWebRequest www = UnityWebRequest.Put(iURL, JsonUtility.ToJson(iDataStructure));
        www.method = UnityWebRequest.kHttpVerbPOST;
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");

        // sending request
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            // data request error
            //Debug.Log(_resultText[_resultText.Length - 1]);
            OnSetMyScoreFailed?.Invoke(_resultText[_resultText.Length - 1]);
        }
        else
        {
            // setting data successful
            OnSetMyScoreSuccessful?.Invoke();

        }

    }

    IEnumerator _checkIpLocation(string iIp, Image iFlagImage)
    {
        // getting ip location request
        UnityWebRequest www = UnityWebRequest.Get(string.Format("{0}{1}", _ipLocatorURL, iIp));

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            // ip location error
            // Debug.Log(_resultText[_resultText.Length - 1]);
            OnFlagParseError(_resultText[_resultText.Length - 1]);
        }
        else
        {
            try
            {
                // converting data information
                IPInformation ipInfo = JsonUtility.FromJson<IPInformation>(www.downloadHandler.text);
                iFlagImage.gameObject.SetActive(true);
                iFlagImage.sprite = Resources.Load<Sprite>("Flags/" + ipInfo.countryCode.ToLower());
                OnFlagParseSuccessful();
            }
            catch
            {
                // data recived is invalid
                iFlagImage.gameObject.SetActive(false);
                OnFlagParseError(_resultText[4]);
            }

        }

    }
    public void _GetRank()
    {
        if (_canPerform)
        {
            //defining data
            RequestDataStructure ds = new RequestDataStructure();

            ds.pkg_id = _UniqueIdentifier;
            ds.limit = _MaxRecord;
            // starting coroutine
            StartCoroutine(_GetRanksRoutine(_getRankURL, ds));
        }
        else
        {
            OnInitFailed?.Invoke(_resultText[4]);
        }
    }
    public void _PopulateList(RankingList iList, Text[] iRankTexts, Text[] iNameTexts, Text[] iScoreTexts, Image[] iFlagImages)
    {
        if (_canPerform)
        {
            // showing data on screen
            int i = 0;
            for (i = 0; i < iList.RankingItem.Length; i++)
            {
                //setting rank on places
                if (i < iRankTexts.Length)
                {
                    iRankTexts[i].enabled = true;
                    iRankTexts[i].text = (i + 1).ToString();

                }
                // setting name on places
                if (i < iNameTexts.Length)
                {
                    iNameTexts[i].enabled = true;
                    iNameTexts[i].text = iList.RankingItem[i].display_name;

                }
                // setting score on places
                if (i < iScoreTexts.Length)
                {
                    iScoreTexts[i].enabled = true;
                    iScoreTexts[i].text = iList.RankingItem[i].player_score.ToString();
                }
                // setting flag on places
                if (i < iFlagImages.Length)
                {
                    iFlagImages[i].enabled = true;
                    StartCoroutine(_checkIpLocation(iList.RankingItem[i].user_ip, iFlagImages[i]));

                }
            }
            // disabling other ranks
            for (int j = i; j < iRankTexts.Length; j++)
            {

                iRankTexts[j].enabled = false;
            }
            // disabling other names
            for (int j = i; j < iNameTexts.Length; j++)
            {

                iNameTexts[j].enabled = false;
            }
            // disabling other scors
            for (int j = i; j < iScoreTexts.Length; j++)
            {

                iScoreTexts[j].enabled = false;
            }
            // disabling other flags
            for (int j = i; j < iFlagImages.Length; j++)
            {

                iFlagImages[j].enabled = false;
            }
            OnListPopulateSuccessful?.Invoke();

        }
        else
        {
            OnInitFailed?.Invoke(_resultText[4]);
        }
    }
    IEnumerator _GetRanksRoutine(string iURL, RequestDataStructure iDataStructure)
    {
        // setting content data
        UnityWebRequest www = UnityWebRequest.Put(iURL, JsonUtility.ToJson(iDataStructure));
        www.method = UnityWebRequest.kHttpVerbPOST;
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        // sending data request
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            // data connection error

            OnGetRankFailed?.Invoke(_resultText[_resultText.Length - 1]);
        }
        else
        {
            try
            {
                // data accuired
                RankingList myDeserializedClass = JsonUtility.FromJson<RankingList>(www.downloadHandler.text);
                OnGetRankSuccessful?.Invoke(myDeserializedClass);

            }
            catch 
            {
                if (OnGetRankFailed != null)
                    OnGetRankFailed(_resultText[3]);
                // recieved data is invalid
            }
        }
    }
    public void _GetMyRank()
    {
        if (_canPerform)
        {
            var ds = new RequestRankStructure();
            ds.pkg_id = _UniqueIdentifier;
            ds.user_name = _UserName;
            // starting coroutine
            StartCoroutine(_GetMyRanksRoutine(_getMyRankURL, ds));
        }
        else
        {
            OnInitFailed(_resultText[4]);
        }
    }
    IEnumerator _GetMyRanksRoutine(string iURL, RequestRankStructure iDataStructure)
    {
        // setting content data

        UnityWebRequest www = UnityWebRequest.Put(iURL, JsonUtility.ToJson(iDataStructure));
        www.method = UnityWebRequest.kHttpVerbPOST;
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");

        // sending data request
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            // data connection error
            OnGetMyRankFailed?.Invoke(_resultText[_resultText.Length - 1]);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            try
            {
                // data accuired
                OnGetMyRankSuccessful?.Invoke(int.Parse(www.downloadHandler.text));
            }
            catch
            {
                OnGetMyRankFailed?.Invoke(_resultText[3]);
                // recieved data is invalid
            }
        }
    }
    [System.Serializable]
    public class DataStructure
    {
        public string pkg_id;
        public string user_name;
        public string display_name;
        public int score;
    }
    [System.Serializable]
    public class RankingItem
    {
        public string display_name;
        public int player_score;
        public string user_ip;
    }
    [System.Serializable]
    public class RankingList
    {
        public RankingItem[] RankingItem;
    }
    [System.Serializable]
    public class RequestRankStructure
    {
        public string pkg_id;
        public string user_name;

    }
    [System.Serializable]
    public class RequestDataStructure
    {
        public string pkg_id;
        public int limit;
    }
    [System.Serializable]
    public class IPInformation
    {
        public string status;
        public string country;
        public string countryCode;
        public string region;
        public string regionName;
        public string city;
        public string zip;
        public double lat;
        public double lon;
        public string timezone;
        public string isp;
        public string org;
        public string @as;
        public string query;
    }
}

