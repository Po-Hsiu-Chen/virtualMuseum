## directory structure

新版資料主要在 `Assets/Resources/` 下。

#### Resources 資料夾
```
Resources/
├─ Audio/          # 展品說明語音  
├─ Material/       # 材質  
├─ Model/          # 3D 模型  
├─ Pictures/       # 圖片資源  
│  ├─ Avatars/  
│  ├─ icon/  
│  ├─ 休閒館(子計畫一)/  
│  ├─ 教育館(子計畫二)/  
│  ├─ 文化館(子計畫四)/  
│  ├─ 海洋館(子計畫五)/  
│  └─ 資訊館(子計畫三)/  
├─ Prefab(temp)/   # 暫時用來存 Prefab  
├─ Scripts/        # 新版新增的所有腳本  
├─ Video/          # 展示影片  
└─ Shader/         # NoAmbientLightingShader.shader：使模型保持原色彩，不受 Ambient 影響
```

#### 現有模型
- *熱帶魚*：`Assets/CGSoul/`
- *各種卡通魚*：`Assets/Alstra Infinite/`
- *泡泡*：`Assets/Jiggly Bubble Free/`
- *燈條*：`Assets/VolumetricLines/`
- *聚光燈*：`Assets/SpaceZeta_Spotlight/`
- *寶箱*：`Assets/ChestFree`

## main functions of scripts under `Assets/Resources/Scripts/`
- **AudioManager.cs**：找到對應 Canvas 名稱的音訊。
- **ButtonState.cs**：管理 SideCanvas 按鈕狀態。
- **ComponentDisabler.cs**：用於啟用或禁用特定 Component。
- **ExhibitInteraction.cs**：處理展品的互動邏輯。
- **ExitButton.cs**：退出程式的按鈕。
- **PhotoSwitcher.cs**：提供圖片左右切換功能。
- **RaycastManager.cs**：負責射線檢測，判斷玩家點擊的物件。
- **RotateObject.cs**：滑鼠控制模型旋轉。
- **VideoController.cs**：影片播放與暫停管理。
- **Database/**：與資料庫相關腳本（註冊、登入、留言等）。
- **海洋館小遊戲/**：海洋館內的小遊戲相關腳本。

## database related
- **MongoDB Atlas**：
  - 若長時間沒連線可能會被暫停。
  - Unity 執行時可能會出現錯誤：
    ```
    MongoConfigurationException: No hosts were found in the SRV record for unity.yrrt9gw.mongodb.net.
    ```
  - 可替換成自己的資料庫。(`Assets/Resources/Scripts/Database/` 下的 `MongoDBManager.cs`、`LoginManager.cs`、`RegistrationManager.cs`)

## other
- **修改人物的跳入點**
  - `PlayerSpawner.cs` (初始點)
  - `positionTest.cs` (每個館)