using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserSetting : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public Image avatarImage;
    public GameObject additionalLight; 
    public GameObject directionalLight; 
    public GameObject databaseManager;

    private AvatarManager avatarManager;

    void Start()
    {
        UpdateAmbientColor(0);
    }
    public void InitializeUser()
    {
        avatarManager = databaseManager.GetComponent<AvatarManager>();
        Sprite avatarSprite = avatarManager.GetAvatar(UserData.Instance.AvatarIndex);
        
        if (avatarSprite != null)
        {
            avatarImage.sprite = avatarSprite;
        }
        else
        {
            // 預設頭像
            avatarImage.sprite = avatarManager.GetAvatar(0);
        }
        
        if (UserData.Instance != null && usernameText != null)
        {
            usernameText.text = UserData.Instance.Username;
        }
        
    }

    public void UpdateAmbientColor(int state)
    {
        if (state == 1)
        {
            RenderSettings.ambientLight = new Color32(0, 0, 91, 255); 
            if (additionalLight != null) additionalLight.SetActive(true); 
            if (directionalLight != null) directionalLight.SetActive(false); 
        }
        else
        {
            RenderSettings.ambientLight = new Color32(255, 255, 255, 255); 
            if (additionalLight != null) additionalLight.SetActive(false); 
            if (directionalLight != null) directionalLight.SetActive(true); 
        }
    }
}
