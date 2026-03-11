# Unity Game Project with GameMaker Framework

A Unity game development project featuring a modular **GameMaker Framework** - a comprehensive game development toolkit with data-driven architecture, designed for creating scalable and maintainable games.

## 📋 Project Overview

This project is built on Unity with Universal Render Pipeline (URP) and includes a custom-built **GameMaker Framework** that provides a robust foundation for game development with features like currency systems, inventory management, in-app purchases, shops, daily rewards, and more.

**Project Name:** GameMakerCore  
**Company:** GameMaker  
**Unity Version:** 6000.x+  
**Render Pipeline:** Universal Render Pipeline (URP) 17.3.0

### Overview Screenshots

![Overview](Demo/overview.png)
![Currency](Demo/currency.png)
![Property](Demo/property.png)
![Item](Demo/item.png)
![Item Detail](Demo/item-detail.png)
![Bundle](Demo/bundle.png)
![Shop](Demo/shop.png)
![Timed](Demo/timed.png)
![Sound](Demo/sound.png)
![Config](Demo/config.png)
![IAP Config](Demo/iapconfig.png)
![UI Controller](Demo/ui-controller.png)
![Data Storage](Demo/data-storage.png)
![Code Generation](Demo/code-generation.png)
![Quick Use](Demo/quick-use.png)
![Sample Data](Demo/sample-data.png)
![Sample Demo](Demo/sample-demo.png)
![Action](Demo/action.png)

## 🎯 Key Features

### GameMaker Framework Modules

- **Core System** - Foundation framework with data managers, definitions, and editor tooling
- **Currency System** - Multi-currency support with BigInt and Long types
- **Item Management** - Inventory system with item definitions and properties
- **Bundle System** - Reward bundles and package management
- **Property System** - Dynamic attribute and stat management
- **Shop System** - In-game store functionality
- **IAP Integration** - In-App Purchase support with Unity Purchasing
- **Daily Rewards** - Time-based reward systems
- **Sound Management** - Audio system powered by BroAudio
- **UI Framework** - Custom UI toolkit components

### Third-Party Integrations

- **UniTask** - High-performance async/await for Unity
- **LitMotion** - Modern animation library
- **UI Effects** - Advanced UI visual effects (Coffee.UIEffect)
- **Particle Effects for UGUI** - Particle system for UI
- **Cinemachine** - Advanced camera system
- **Unity Input System** - Modern input handling
- **Addressables** - Asset management system
- **AI Navigation** - NavMesh and pathfinding

## 🏗️ Project Structure

```
core/
├── Assets/
│   ├── GameMaker/                             # Custom GameMaker Framework
│   │   ├── Core/                              # Core framework functionality
│   │   │   ├── Runtime/
│   │   │   │   ├── Scripts/
│   │   │   │   │   ├── Gateways/
│   │   │   │   │   ├── RuntimeDataManagers/
│   │   │   │   │   │   ├── BaseRuntimeDataManager.cs
│   │   │   │   │   │   ├── PlayerCurrencyRuntimeDataManager.cs
│   │   │   │   │   │   ├── PlayerPropertyRuntimeDataManager.cs
│   │   │   │   │   │   ├── PlayerTimedRuntimeDataManager.cs
│   │   │   │   │   │   ├── PlayerItemDetailRuntimeDataManager.cs
│   │   │   │   │   │   ├── BundleRuntimeDataManager.cs
│   │   │   │   │   │   └── RuntimeActionManager.cs
│   │   │   │   │   ├── PlayerDataManagers/
│   │   │   │   │   │   ├── BasePlayerDataManager.cs
│   │   │   │   │   │   ├── PlayerCurrencyManager.cs
│   │   │   │   │   │   ├── PlayerPropertyManager.cs
│   │   │   │   │   │   ├── PlayerTimedManager.cs
│   │   │   │   │   │   └── PlayerItemDetailManager.cs
│   │   │   │   │   ├── DataSpaceProviders/
│   │   │   │   │   │   ├── Bundle/
│   │   │   │   │   │   ├── Currency/
│   │   │   │   │   │   ├── Item/
│   │   │   │   │   │   ├── Property/
│   │   │   │   │   │   └── Timed/
│   │   │   │   │   ├── LocalDatas/
│   │   │   │   │   ├── Times/
│   │   │   │   │   │   ├── TimeManager.cs
│   │   │   │   │   │   ├── LocalTimeStrategy.cs
│   │   │   │   │   │   ├── ServerTimeStrategy.cs
│   │   │   │   │   │   └── HybridTimeStrategy.cs
│   │   │   │   │   ├── Managers/
│   │   │   │   │   ├── Definitions/
│   │   │   │   │   │   ├── Actions/
│   │   │   │   │   │   ├── Bundle/
│   │   │   │   │   │   ├── Configs/
│   │   │   │   │   │   ├── Currency/
│   │   │   │   │   │   ├── Item/
│   │   │   │   │   │   ├── Property/
│   │   │   │   │   │   └── Timed/
│   │   │   │   │   └── IDs/
│   │   │   └── Editor/
│   │   │       ├── Scripts/
│   │   │       │   ├── PropertyDrawers/
│   │   │       │   ├── Holders/
│   │   │       │   └── TabContentHolder/
│   │   ├── Features/
│   │   │   ├── DailyReward/
│   │   │   │   ├── Runtime/
│   │   │   │   └── Editor/
│   │   │   ├── IAP/
│   │   │   │   ├── Runtime/
│   │   │   │   └── Editor/
│   │   │   └── Shop/
│   │   │       ├── Runtime/
│   │   │       └── Editor/
│   │   ├── Sound/
│   │   │   ├── Runtime/
│   │   │   │   ├── Scripts/Managers/
│   │   │   │   ├── Scripts/DataSpaceProviders/
│   │   │   │   ├── Scripts/RuntimeManagers/
│   │   │   │   ├── Scripts/LocalDatas/
│   │   │   │   ├── Scripts/PlayerDatas/
│   │   │   │   └── Scripts/IDs/
│   │   │   └── Editor/
│   │   │       └── Scripts/
│   │   ├── UI/
│   │   │   └── Runtime/
│   │   │       ├── Scripts/Controllers/
│   │   │       ├── Scripts/Views/
│   │   │       ├── Scripts/Popups/
│   │   │       ├── Scripts/Animations/
│   │   │       └── Scripts/Interactions/
│   │   └── Extend/
│   ├── _GamePlay/
│   │   ├── Runtime/
│   │   │   ├── Scripts/UIs/
│   │   │   │   ├── Views/
│   │   │   │   └── Elements/
│   │   │   └── Scripts/
│   │   └── Editor/
│   ├── Scenes/
│   ├── Resources/
│   ├── Settings/
│   ├── AddressableAssetsData/
│   ├── Docs/
│   │   └── Images/
│   ├── _Gen/
│   │   ├── Currency/
│   │   │   └── Scripts/
│   │   └── Property/
│   │       └── Scripts/
│   └── Samples/
├── Packages/
├── ProjectSettings/
├── UserSettings/
└── README.md
```

## 🔧 GameMaker Framework Architecture

The GameMaker Framework follows a **definition-based architecture** with clear separation between data and logic:

### Core Concepts

- **Definitions** - Data containers for game objects (items, currencies, bundles, etc.)
- **Managers** - Singleton managers that handle definition collections and runtime operations
- **Runtime Data Managers** - Player runtime state built from definitions and data providers
- **Data Space Providers** - Data sources (local/remote) for player state and saves
- **Time System** - Unified time access for cooldowns, resets, and timers
- **Holders** - Editor UI components using UI Toolkit for custom inspectors
- **Data-Driven Design** - Game content defined through ScriptableObjects

### Runtime Managers and Data Providers

Runtime data uses a **DataSpaceProvider layer** to decide where data is loaded/saved (local, server, hybrid).

- **BaseDataSpaceSetting** initializes LocalDataManager and registers providers tagged with DataSpaceAttribute.INIT_ANY.
- **LocalDataSpaceSetting** registers providers tagged with LocalDataSpaceSetting.LOCAL_SPACE.
- **IDataSpaceProvider** is the interface for loading/saving player data.
- **Runtime Data Managers** (e.g., PlayerCurrencyRuntimeDataManager) consume providers based on RuntimeDataManagerAttribute.

Example: Local provider for Currency

```csharp
[DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
public class LocalCurrencyDataSpaceProvider : BaseCurrencyDataSpaceProvider
{
    public override async UniTask<bool> InitAsync(BaseDataSpaceSetting setting)
    {
        // Load LocalDataManager and read local save data
        return true;
    }
}
```

Example: Provider that initializes in any data space

```csharp
[DataSpace(nameof(DataSpaceAttribute.INIT_ANY))]
public class SoundDataSpaceProvider : IDataSpaceProvider
{
    public async UniTask<bool> InitAsync(BaseDataSpaceSetting setting)
    {
        // Always available (local or remote settings)
        return true;
    }
}
```

Example: Runtime manager consumes provider types

```csharp
[RuntimeDataManager(new Type[] { typeof(BaseCurrencyDataSpaceProvider) })]
public class PlayerCurrencyRuntimeDataManager : BaseRuntimeDataManager
{
    // Uses provider to load and update player currency
}
```

### Attributes Reference

- **DataSpaceAttribute** - Marks a provider with a named space (INIT_ANY or LOCAL_SPACE) to control initialization scope.
- **RuntimeDataManagerAttribute** - Declares which provider types a runtime manager requires.
- **ScriptableObjectSingletonPathAttribute** - Sets the Resources path for manager singletons.
- **TypeContainAttribute** - Maps definition types to editor holders (used in custom inspectors).
- **TypeCache** - Marks types to be cached for fast discovery at editor/runtime.

### Time System

The time system provides a single source of truth for timers, resets, and cooldowns.

```csharp
// Initialize once (e.g., at boot)
await TimeManager.Instance.InitializeAsync(TimeMode.Hybrid);

// Use anywhere
var utcNow = TimeManager.Instance.UTCNow;
var unixTime = TimeManager.Instance.UnixTimestamp;
var untilMidnight = TimeManager.Instance.TimeUntilMidnight;
```

### Key Components

#### Currency System
- `BaseCurrencyDefinition` - Base class for all currencies
- `LongCurrencyDefinition` - Standard 64-bit integer currency
- `BigIntCurrencyDefinition` - Support for extremely large numbers
- `CurrencyManager` - Centralized currency management

#### Item System
- `ItemDefinition` - Item data definitions
- `ItemDetailDefinition` - Extended item information
- `ItemManager` - Item registry and queries
- Property-based item attributes (Stats, Attributes)

#### Bundle & Reward System
- `BundleDefinition` - Package multiple rewards together
- `BaseRewardDefinition` - Base class for all reward types
- `CurrencyRewardDefinition` - Currency-based rewards
- Dynamic reward composition

#### Property System
- `PropertyDefinition` - Base property definition
- `StatDefinition` - Numeric properties (health, damage, etc.)
- `AttributeDefinition` - String-based properties
- `PropertyManager` - Property registry

## 📦 Dependencies

### Core Unity Packages
- Addressables 2.8.1
- Universal Render Pipeline 17.3.0
- Input System 1.18.0
- Cinemachine 3.1.5
- Timeline 1.8.10
- Visual Effect Graph 17.3.0
- AI Navigation 2.0.10
- Unity Purchasing 5.1.2

### Third-Party Libraries
- **UniTask** - Async/await utilities
- **LitMotion** - Performance-focused animation
- **BroAudio** - Audio management
- **Coffee.UIEffect** - UI visual effects
- **UIParticle** - Particle system for UI
- **Unity UI Extensions** - Extended UI components

## 🚀 Getting Started

### Prerequisites
- Unity 2023.x or later
- Git with LFS support (for large assets)
- Visual Studio 2022 or Rider (recommended)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd core
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the `core` folder
   - Open the project

3. **Package Installation**
   - Packages will auto-install from manifest.json
   - If issues occur, use Window → Package Manager → Resolve

4. **Open Main Scene**
   - Navigate to `Assets/Scenes/`
   - Open the main scene to start development

### First Run

The project uses custom editor tools for the GameMaker Framework. Access them via:
- **Window → GameMaker → [Feature Name]**
- Custom inspectors will appear when selecting framework assets

## 🛠️ Development

### Working with GameMaker Framework

#### Creating New Definitions

1. Create a new C# class inheriting from the appropriate base:
   ```csharp
   [System.Serializable]
   public class MyItemDefinition : ItemDefinition
   {
       // Your custom fields
   }
   ```

2. Create corresponding Editor Holder:
   ```csharp
   [TypeContain(typeof(MyItemDefinition))]
   public class MyItemDefinitionHolder : ItemDefinitionHolder
   {
       // Custom editor UI
   }
   ```

### 📌 Using ID Classes for Inspector Reference

The framework provides **type-safe ID classes** for referencing definitions in the Inspector with dropdown selection.

#### Available ID Types

- **CurrencyID** - Reference currency definitions
- **PropertyID** - Reference property definitions (Stats, Attributes)
- **ItemID** - Reference item definitions
- **BundleID** - Reference bundle definitions
- **TimedID** - Reference timed definitions
- **SoundID** - Reference sound definitions
- **ConfigID** - Reference config definitions
- **ConditionID** - Reference condition definitions

#### Using IDs in Your Scripts

```csharp
using GameMaker.Core.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // IDs serialize as dropdowns in Inspector
    [SerializeField] private CurrencyID _goldCurrencyID;
    [SerializeField] private PropertyID _healthPropertyID;
    [SerializeField] private BundleID _rewardBundleID;
    
    private void Start()
    {
        // Get player's current currency value
        var playerGold = _goldCurrencyID.GetPlayerCurrency();
        Debug.Log($"Gold: {playerGold.GetValue()}");
        
        // Get player's property
        var playerHealth = _healthPropertyID.GetPlayerStat();
        Debug.Log($"Health: {playerHealth.Value}");
        
        // Get the definition
        var goldDef = _goldCurrencyID.GetBaseCurrencyDefinition();
        Debug.Log($"Currency Name: {goldDef.GetName()}");
    }
    
    // Modify player data
    private async void AddGold(int amount)
    {
        await _goldCurrencyID.AddCurrencyAsync(amount, null);
    }
    
    private async void SetHealth(float value)
    {
        await _healthPropertyID.SetPropertyAsync(value.ToString(), null);
    }
}
```

#### Inspector Behavior

When you add an ID field to your script:
1. **Dropdown appears** in Inspector showing all available definitions
2. **Type-safe** - only shows definitions of the correct type
3. **Auto-updates** when definitions are added/removed
4. **Displays definition names** (not IDs) for better readability

### 🔌 Gateway Pattern - Accessing Player Data

Gateways provide **static access** to runtime player data managers.

#### Available Gateways

```csharp
// Currency Gateway
CurrencyGateway.Manager.GetPlayerCurrency("Coin");
CurrencyGateway.Manager.AddPlayerCurrencyAsync("Gem", 100, null);

// Property Gateway  
PropertyGateway.Manager.GetPlayerProperty("Level");
PropertyGateway.Manager.SetPlayerPropertyAsync("Health", "100", null);

// Item Gateway
ItemGateway.Manager.GetPlayerItemDetail("sword_001");

// Timed Gateway
TimedGateway.Manager.GetPlayerTimed("daily_bonus");
TimedGateway.Manager.AddPlayerTimedAsync("energy", 1, null);

// Bundle Gateway
BundleGateway.Manager // Access bundle operations
```

#### Gateway Usage Examples

```csharp
using GameMaker.Core.Runtime;
using Cysharp.Threading.Tasks;

public class RewardSystem
{
    // Method 1: Using Gateway directly
    public async UniTask GiveReward()
    {
        await CurrencyGateway.Manager.AddPlayerCurrencyAsync("Coin", 100, null);
        await PropertyGateway.Manager.SetPlayerPropertyAsync("Experience", "50", null);
    }
    
    // Method 2: Using ID reference (recommended)
    [SerializeField] private CurrencyID _rewardCurrency;
    
    public async UniTask GiveRewardWithID()
    {
        await _rewardCurrency.AddCurrencyAsync(100, null);
    }
    
    // Method 3: Using generated code (type-safe)
    public void CheckPlayerGold()
    {
        var coinCurrency = new Currency.Coin();
        var playerCoin = coinCurrency.GetPlayerCurrency();
        
        if (playerCoin.GetLongValue() >= 1000)
        {
            Debug.Log("Player is rich!");
        }
    }
}
```

### 💾 LocalDataManager - Save System

LocalDataManager handles **persistent data storage** with automatic serialization.

#### Accessing LocalDataManager

```csharp
using GameMaker.Core.Runtime;

public class SaveSystem
{
    private LocalDataManager _localDataManager;
    
    public async UniTask Initialize()
    {
        _localDataManager = new LocalDataManager();
        await _localDataManager.InitAsync(); // Load all data
    }
    
    // Get specific save data
    public void GetCurrencyData()
    {
        var currencySaveData = _localDataManager.Get<LocalCurrencySaveData>();
        // Access saved currency data
    }
    
    // Save specific data type
    public async UniTask SaveCurrencies()
    {
        await _localDataManager.SaveAsync<LocalCurrencySaveData>();
    }
    
    // Save all data
    public async UniTask SaveAll()
    {
        await _localDataManager.SaveAll();
    }
}
```

#### Available Save Data Types

- **LocalCurrencySaveData** - Player currency values
- **LocalPropertySaveData** - Player properties (stats, attributes)
- **LocalTimedSaveData** - Timed data (cooldowns, timestamps)
- **LocalSoundSaveData** - Sound/audio settings
- **Custom save data** - Extend `BaseLocalData`

#### LocalData Storage Location

- **Path**: `Application.persistentDataPath/[ClassName].json`
- **Format**: JSON with type information
- **Auto-serialization**: Handled by Newtonsoft.Json

### 🎯 Using Managers

#### Definition Managers (ScriptableObject Singletons)

```csharp
// Currency Manager - Manages currency definitions
var coinDef = CurrencyManager.Instance.GetDefinition("Coin");
var allCurrencies = CurrencyManager.Instance.GetDefinitions();

// Property Manager - Manages property definitions  
var levelDef = PropertyManager.Instance.GetDefinition("Level");

// Item Manager - Manages item definitions
var swordDef = ItemManager.Instance.GetDefinition("legendary_sword");

// Bundle Manager - Manages bundle definitions
var starterPackDef = BundleManager.Instance.GetDefinition("starter_pack");

// Sound Manager - Manages sound definitions
var bgmDef = SoundManager.Instance.GetDefinition("main_theme");
var mixerGroups = SoundManager.Instance.GetMixerGroupNames();
```

#### Runtime Data Managers (Player Data)

```csharp
// Access through Gateways (recommended)
var playerCurrency = CurrencyGateway.Manager.GetPlayerCurrency("Coin");
var playerLevel = PropertyGateway.Manager.GetPlayerProperty("Level");

// Modify player data
await CurrencyGateway.Manager.AddPlayerCurrencyAsync("Gem", 50, null);
await PropertyGateway.Manager.SetPlayerPropertyAsync("Health", "100", null);
```

### 🔧 Generated Code - Type-Safe Access

The framework **auto-generates** type-safe helper classes from your definitions.

#### Generated Currency Helper

Location: `Assets/_Gen/Currency/Scripts/Currency.gen.cs`

```csharp
// Auto-generated from CurrencyManager definitions
public class Currency
{
    public class Coin
    {
        public const string ID = "Coin";
        
        public BaseCurrencyDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(ID);
        }
        
        public BasePlayerCurrency GetPlayerCurrency()
        {
            return CurrencyGateway.Manager.GetPlayerCurrency("Coin");
        }
    }
    
    public class Gem { /* ... */ }
    public class Energy { /* ... */ }
}

// Usage
var coin = new Currency.Coin();
var playerCoins = coin.GetPlayerCurrency();
var coinDefinition = coin.GetDefinition();
```

#### Generated Property Helper

Location: `Assets/_Gen/Property/Scripts/Property.gen.cs`

```csharp
// Auto-generated from PropertyManager definitions
public class Property
{
    public class Level
    {
        public const string ID = "Level";
        
        public StatDefinition GetDefinition()
        {
            return PropertyManager.Instance.GetDefinition(ID);
        }
        
        public PlayerStat GetPlayerStat()
        {
            return PropertyGateway.Manager.GetPlayerProperty("Level") as PlayerStat;
        }
    }
    
    public class DefaultName
    {
        public const string ID = "DefaultName";
        
        public PlayerAttribute GetPlayerAttribute()
        {
            return PropertyGateway.Manager.GetPlayerProperty("DefaultName") as PlayerAttribute;
        }
    }
}

// Usage
var level = new Property.Level();
var playerLevel = level.GetPlayerStat();
Debug.Log($"Player Level: {playerLevel.Value}");
```

### 🎮 Complete Usage Example

```csharp
using GameMaker.Core.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // Method 1: Using ID References (Inspector-assigned)
    [SerializeField] private CurrencyID _goldID;
    [SerializeField] private PropertyID _levelID;
    [SerializeField] private BundleID _rewardID;
    
    private async void Start()
    {
        // Get current values
        var currentGold = _goldID.GetPlayerCurrency().GetLongValue();
        var currentLevel = _levelID.GetPlayerStat().Value;
        
        Debug.Log($"Gold: {currentGold}, Level: {currentLevel}");
        
        // Modify values
        await _goldID.AddCurrencyAsync(100, null);
        await _levelID.SetPropertyAsync((currentLevel + 1).ToString(), null);
    }
    
    // Method 2: Using Generated Code (Type-safe)
    private void UseGeneratedCode()
    {
        var coin = new Currency.Coin();
        var playerCoins = coin.GetPlayerCurrency();
        
        var level = new Property.Level();
        var playerLevel = level.GetPlayerStat();
        
        Debug.Log($"Coins: {playerCoins.GetValue()}");
        Debug.Log($"Level: {playerLevel.Value}");
    }
    
    // Method 3: Using Gateway Directly (Dynamic)
    private async UniTask UseGatewayDirect(string currencyId, long amount)
    {
        await CurrencyGateway.Manager.AddPlayerCurrencyAsync(currencyId, amount, null);
        
        var currency = CurrencyGateway.Manager.GetPlayerCurrency(currencyId);
        Debug.Log($"{currencyId}: {currency.GetValue()}");
    }
    
    // Method 4: Using Manager for Definitions
    private void UseManagers()
    {
        var coinDefinition = CurrencyManager.Instance.GetDefinition("Coin");
        var levelDefinition = PropertyManager.Instance.GetDefinition("Level");
        
        Debug.Log($"Coin Name: {coinDefinition.GetName()}");
        Debug.Log($"Coin Icon: {coinDefinition.GetIcon()}");
    }
}
```

### 🎵 Sound System

The sound system integrates **BroAudio** with the GameMaker Framework.

#### SoundManager (Definition Manager)

```csharp
// Get sound definitions
var bgmDefinition = SoundManager.Instance.GetDefinition("main_theme");

// Get audio mixer groups
var mixerGroups = SoundManager.Instance.GetMixerGroupNames();

// Access AudioMixer
var mixer = SoundManager.Instance.AudioMixer;
```

#### SoundRuntimeDataManager

```csharp
// Playing sounds (through BroAudio integration)
SoundRuntimeManager.Instance.PlayLoopFade(soundDefinition);
SoundRuntimeManager.Instance.StopLoopFade(soundDefinition);

// Volume control
SoundRuntimeDataManager.Instance.SetVolume("Master", 0.8f);
SoundRuntimeDataManager.Instance.SetVolume("Music", 0.6f);
```

#### Using SoundID in Scripts

```csharp
using GameMaker.Sound.Runtime;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundID _bgmSoundID;
    [SerializeField] private SoundID _sfxClickID;
    
    private void Start()
    {
        // Get sound definition
        var bgmDef = _bgmSoundID.GetSoundDefinition();
        
        // Play sound
        SoundRuntimeManager.Instance.PlayLoopFade(bgmDef);
    }
    
    private void OnButtonClick()
    {
        var clickDef = _sfxClickID.GetSoundDefinition();
        // Play one-shot sound
        // Implementation depends on BroAudio
    }
}
```

### 🖼️ UI System

The UI system provides **View-Popup-Alert architecture** with Addressables integration.

#### UIController - Main UI Manager

```csharp
using GameMaker.UI.Runtime;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    
    private void Start()
    {
        _uiController.OnInit();
        
        // Access sub-controllers
        var viewController = _uiController.ViewController;
        var popupController = _uiController.PopupController;
        var alertController = _uiController.AlertController;
    }
}
```

#### ViewController - Screen Management

```csharp
using GameMaker.UI.Runtime;
using Cysharp.Threading.Tasks;

public class NavigationManager
{
    private ViewController _viewController;
    
    // Show view with different transition types
    public async UniTask ShowHomeView()
    {
        // Parallel - Hide current and show new simultaneously
        await _viewController.ShowAsync("HomeView", ViewShowType.Parallel);
    }
    
    public async UniTask ShowGameView()
    {
        // Before - Show new view before hiding current
        await _viewController.ShowAsync("GameView", ViewShowType.Before);
    }
    
    public async UniTask ShowMapView()
    {
        // After - Hide current view before showing new
        await _viewController.ShowAsync("MapView", ViewShowType.After);
    }
    
    // Pass data to view
    public async UniTask ShowLevelView(int levelId)
    {
        var data = new LevelData { LevelId = levelId };
        await _viewController.ShowAsync("LevelView", ViewShowType.Parallel, data);
    }
    
    // Get current view
    public BaseView GetCurrentView()
    {
        return _viewController.CurrentView;
    }
}
```

#### PopupController - Popup Management

```csharp
using GameMaker.UI.Runtime;
using Cysharp.Threading.Tasks;

public class PopupManager
{
    private PopupController _popupController;
    
    // Show popup (parallel - can have multiple)
    public async UniTask ShowRewardPopup()
    {
        var popup = await _popupController.ShowAsync<RewardPopup>(
            "RewardPopup", 
            data: null, 
            PopupShowType.Parallel
        );
        
        // Use the popup
        popup.SetRewardData(coins: 100, gems: 50);
    }
    
    // Show popup (sequence - queued)
    public async UniTask ShowTutorialPopup()
    {
        var popup = await _popupController.ShowAsync<TutorialPopup>(
            "TutorialPopup",
            data: null,
            PopupShowType.Sequence // Waits for other popups to close
        );
    }
    
    // Hide specific popup
    public async UniTask HidePopup(BasePopup popup)
    {
        await _popupController.HideAsync(popup);
    }
    
    // Get active popup
    public BasePopup GetActivePopup<T>() where T : BasePopup
    {
        return _popupController.GetPopup<T>();
    }
}
```

#### Creating Custom Views

```csharp
using GameMaker.UI.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public const string VIEW_NAME = "HomeView";
    
    [SerializeField] private Button _playButton;
    [SerializeField] private UICurrency[] _currencies;
    
    // Called when view is initialized
    protected override void OnInit(ViewController viewController)
    {
        base.OnInit(viewController);
        // Setup initialization
    }
    
    // Called when view starts showing
    protected override void OnShow()
    {
        base.OnShow();
        
        // Initialize UI elements
        foreach (var currency in _currencies)
        {
            currency.Init();
        }
        
        // Add listeners
        _playButton.onClick.AddListener(OnPlayClicked);
    }
    
    // Called when view starts hiding
    protected override void OnHide()
    {
        base.OnHide();
        
        // Remove listeners
        _playButton.onClick.RemoveListener(OnPlayClicked);
    }
    
    // Called after view is completely hidden
    protected override void OnHidden()
    {
        base.OnHidden();
        
        // Cleanup
        foreach (var currency in _currencies)
        {
            currency.Clear();
        }
    }
    
    private void OnPlayClicked()
    {
        viewController.ShowAsync("GameView", ViewShowType.Before).Forget();
    }
}
```

#### Creating Custom Popups

```csharp
using GameMaker.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : BasePopup
{
    [SerializeField] private Text _titleText;
    [SerializeField] private Button _claimButton;
    
    private RewardData _rewardData;
    
    // Set popup data
    public override void SetData(object data)
    {
        base.SetData(data);
        _rewardData = data as RewardData;
        UpdateUI();
    }
    
    protected override void OnShow()
    {
        base.OnShow();
        _claimButton.onClick.AddListener(OnClaimClicked);
    }
    
    protected override void OnHide()
    {
        base.OnHide();
        _claimButton.onClick.RemoveListener(OnClaimClicked);
    }
    
    private void UpdateUI()
    {
        _titleText.text = _rewardData.Title;
        // Update UI with reward data
    }
    
    private async void OnClaimClicked()
    {
        // Give rewards
        await GiveRewards();
        
        // Close popup
        await popupController.HideAsync(this);
    }
}
```

#### UI Animations

The UI system supports **LitMotion animations** for smooth transitions.

```csharp
using GameMaker.UI.Runtime;
using UnityEngine;

public class AnimatedView : BaseView
{
    // Animation automatically plays on Show/Hide
    // Configure animation in Inspector using LMotionUIAnimation component
    
    protected override async UniTask OnShowAnimation()
    {
        // Custom show animation
        await _uiAnimation.ShowAsync();
    }
    
    protected override async UniTask OnHideAnimation()
    {
        // Custom hide animation
        await _uiAnimation.HideAsync();
    }
}
```

### Code Organization

- **Runtime** - All gameplay and runtime code
- **Editor** - Editor-only tools, inspectors, and utilities
- Follow namespace convention: `GameMaker.[Module].[Runtime|Editor]`

### Custom Editor Tools

The framework includes extensive custom editor tooling:
- **Data Manager Drawers** - Visual list editors for definitions
- **Filter Systems** - Search and filter large datasets
- **Property Inspectors** - Type-specific property editors
- **Validation** - Built-in data validation

## 🎮 Game-Specific Code

Game-specific implementation resides in:
- `Assets/_GamePlay/` - Main gameplay code
- `Assets/CatAdventure.GamePlay.csproj` - Game assembly
- `Assets/NekoLegends.SharedAssets.csproj` - Shared assets

## 📝 Code Generation

The framework includes code generation utilities:
- Property code generation for typed access
- Auto-generation of definition factories
- Type caching for performance optimization

## 🧪 Testing

- Test Framework included (Unity Test Framework 1.6.0)
- Create tests in `Assets/Tests/` folder
- Run via Test Runner (Window → General → Test Runner)

## 🤝 Contributing

When contributing to this project:
1. Follow existing code conventions and namespaces
2. Add XML documentation for public APIs
3. Create corresponding Editor tooling for new definitions
4. Test both runtime and editor functionality

## 📄 License

[Specify your license here]

## 🔗 Resources

### Unity Resources
- [Unity Documentation](https://docs.unity3d.com/)
- [URP Documentation](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
- [Addressables Guide](https://docs.unity3d.com/Packages/com.unity.addressables@latest)

### Third-Party Documentation
- [UniTask](https://github.com/Cysharp/UniTask)
- [LitMotion](https://github.com/AnnulusGames/LitMotion)
- [UI Effect](https://github.com/mob-sakai/UIEffect)
- [ParticleEffectForUGUI](https://github.com/mob-sakai/ParticleEffectForUGUI)

---

**Last Updated:** February 26, 2026
