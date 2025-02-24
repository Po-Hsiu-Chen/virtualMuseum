using UnityEngine;

public class ExitButton : MonoBehaviour

{
    // 退出程式的方法
    public void QuitGame()
    {
        #if UNITY_EDITOR
            // 在 Unity 編輯器中模擬退出
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 在實際執行檔案中退出
            Application.Quit();
        #endif
    }
}
