using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCcanvas : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject SettingCanvas;
    bool flag = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(flag){
                showlangageCanvas();
                flag = !flag;
            }
            else{
                exitlangageCanvas();
                flag = !flag;
            }
        }
    }
    public void showlangageCanvas(){
        SettingCanvas.SetActive(true);
    }
    public void exitlangageCanvas(){
        SettingCanvas.SetActive(false);
    }
}
