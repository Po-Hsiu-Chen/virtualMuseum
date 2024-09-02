using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour
{
    public Sprite[] statesImage; 
    public GameObject toggle; 
    private Image toggleImage; 
    private Button toggleButton;
    private int currentState; // 目前狀態

    public string buttonFunction = ""; // 按鈕功能
    public UserSetting userSetting;
    

    void Start()
    {
        currentState = 0; 

        toggleImage = toggle.GetComponent<Image>(); 
        toggleButton = toggle.GetComponent<Button>();
        toggleButton.onClick.AddListener(ChangeButtonState);

        toggleImage.sprite = statesImage[currentState]; 
    }

    public void ChangeButtonState()
    {
        // 切換狀態
        currentState = (currentState == 0) ? 1 : 0;
        toggleImage.sprite = statesImage[currentState];

        switch (buttonFunction)
        {
            case "changeAmbientColor":
                userSetting.UpdateAmbientColor(currentState); 
                break;

            default:
                Debug.LogWarning("未定義的按鈕功能: " + buttonFunction); 
                break;
        }
    }

}
