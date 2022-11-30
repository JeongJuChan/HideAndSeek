using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField] string _userName;
    
    PhotonConnector _photonConnector;

    const string TITLE_ID = "E35EF";
    #region Unity Methods

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = TITLE_ID;
        }
        _photonConnector = FindObjectOfType<PhotonConnector>();
    }

    #endregion

    #region Public Methods

    public void SetPlayerName(string userName)
    {
        _userName = userName;
        PlayerPrefs.SetString(Define.UserAuth.UserName.ToString(), _userName);
    }

    public void Login()
    {
        // 이름이 유효한지 체크
        if (!IsValidUserName())
            return;
        LoginUsingCustomId();
    }

    #endregion

    #region Private Methods

    bool IsValidUserName()
    {
        bool isValid = _userName.Length is >= 3 and < 24;
        return isValid;
    }
    
    void LoginUsingCustomId()
    {
        Debug.Log($"Login to Playfab as {_userName}");
        var request = new LoginWithCustomIDRequest { CustomId = _userName, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnPlayfabError);
    }

    void UpdateDisplayName()
    {
        Debug.Log("Updating DisplayName");
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = _userName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserTitleNameSuccess, OnPlayfabError);
    }
    
    // TODO : Destroy
    void LoginRewardRequest(string currency)
    {
        var request = new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddCurrency",
            FunctionParameter = new { text = currency },
            GeneratePlayStreamEvent = true
        };
        
        PlayFabClientAPI.ExecuteCloudScript(request, result => { Debug.Log("ExecuteCloudScriptSuccessed");}, error => { Debug.LogError("ExecuteCloudScriptError");});
    }

    #endregion

    #region PlayfabCallbacks

    void OnLoginCustomIdSuccess(LoginResult result)
    {
        Debug.Log($"Login with CustomId Succeed {result}" );
        UpdateDisplayName();
        LoginRewardRequest("KR");
    }
    
    void OnUpdateUserTitleNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"OnUpdateUserTitleNameSuccess : {result}");
        _photonConnector.ConnectToPhoton();
    }

    void OnPlayfabError(PlayFabError error)
    {
        Debug.LogError($"OnPlayfabError : {error}");
    }

    #endregion
}
