using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public GameObject loginPanel;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public Button guestButton;  // 新增訪客模式按鈕
    public TextMeshProUGUI messageText;
    public GameObject manager;
    
    public UserSetting userSetting;
    private MongoClient client;
    private IMongoDatabase userDatabase;
    private IMongoCollection<BsonDocument> accountCollection;
    private ComponentDisabler componentDisabler;
    private Bird bird;

    public static string LoggedInUsername { get; private set; }
    public static int LoggedInAvatarIndex { get; private set; }

    private void Start()
    {
        componentDisabler = manager.GetComponent<ComponentDisabler>();
        client = new MongoClient("mongodb+srv://popo:K5q4fl0en5NzhkLq@unity.yrrt9gw.mongodb.net/?retryWrites=true&w=majority&appName=unity");
        userDatabase = client.GetDatabase("UserDatabase");
        accountCollection = userDatabase.GetCollection<BsonDocument>("UserAccounts");
        bird = FindObjectOfType<Bird>();

        componentDisabler.DisableComponents();
        loginButton.onClick.AddListener(OnLogin);
        guestButton.onClick.AddListener(OnGuestLogin);  // 為訪客模式按鈕添加事件監聽器
        //loginPanel.SetActive(true);
    }

    private async void OnLogin()
    {
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "使用者名稱和密碼不能為空";
            return;
        }

        var filter = Builders<BsonDocument>.Filter.Eq("username", username) & Builders<BsonDocument>.Filter.Eq("password", password);
        var existingAccount = await accountCollection.Find(filter).FirstOrDefaultAsync();

        if (existingAccount != null)
        {
            // 将用户数据存储到 UserData 中
            UserData.Instance.Username = username;
            UserData.Instance.AvatarIndex = existingAccount["avatarIndex"].AsInt32;

            messageText.text = "登入成功!";
            loginPanel.SetActive(false);
            bird.StartIntro();
            componentDisabler.EnableComponents();
            componentDisabler.HideStartCanvas();
            userSetting.InitializeUser();
        }
        else
        {
            messageText.text = "使用者名稱或密碼錯誤";
        }
    }

    // 訪客模式登入
    private void OnGuestLogin()
    {
        // 設定訪客的資料
        UserData.Instance.Username = "Guest";
        UserData.Instance.AvatarIndex = 0;

        messageText.text = "以訪客模式進入!";
        loginPanel.SetActive(false);
        bird.StartIntro();
        componentDisabler.EnableComponents();
        componentDisabler.HideStartCanvas();
        userSetting.InitializeUser();
    }
}
