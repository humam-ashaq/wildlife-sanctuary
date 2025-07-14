using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Pastikan ini ada

public class RangerStatus : MonoBehaviour
{
    public static RangerStatus Instance; // Singleton

    public int gold = 0;
    public int exp = 0;
    public int level = 1;

    // Pastikan variabel ini ter-set via Inspector di Scene Awal (Level1)
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;

    [Header("Level Up Settings")]
    public int baseExpToLevelUp = 10;
    public bool readyToLevelUp = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"[RangerStatus] Awake: Instance set. Gold: {gold}, Exp: {exp}, Level: {level}. GameObject Name: {gameObject.name}");
        }
        else
        {
            // Jika sudah ada instance, hancurkan yang ini (duplikat)
            Debug.LogWarning($"[RangerStatus] Awake: Duplicate instance detected ({gameObject.name}). Destroying this one.");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Pastikan event hanya di-subscribe sekali
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe dulu untuk menghindari duplikasi
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("[RangerStatus] OnEnable: Subscribed to SceneManager.sceneLoaded. Initial UI Update Attempt.");
        UpdateUI(); // Update UI saat script diaktifkan (misal setelah DontDestroyOnLoad)
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("[RangerStatus] OnDisable: Unsubscribed from SceneManager.sceneLoaded.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[RangerStatus] OnSceneLoaded: Scene '{scene.name}' loaded. Searching for UI elements...");

        // Penting: Pastikan nama GameObject UI Anda sama persis di setiap scene!
        // Jika Anda mendrag via Inspector di scene Level1, maka di scene berikutnya
        // Anda harus memastikan ada GameObject UI dengan nama yang sama untuk Find
        // Atau Anda harus drag lagi di inspector RangerStatus di scene tersebut.
        // Cara terbaik adalah dengan Find.
        goldText = GameObject.Find("Gold")?.GetComponent<TextMeshProUGUI>();
        expText = GameObject.Find("Exp")?.GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();

        if (goldText == null) Debug.LogWarning($"[RangerStatus] OnSceneLoaded: GoldText UI not found in scene '{scene.name}'.");
        if (expText == null) Debug.LogWarning($"[RangerStatus] OnSceneLoaded: ExpText UI not found in scene '{scene.name}'.");
        if (levelText == null) Debug.LogWarning($"[RangerStatus] OnSceneLoaded: LevelText UI not found in scene '{scene.name}'.");

        UpdateUI(); // Perbarui UI setelah menemukan referensi di scene baru
        Debug.Log($"[RangerStatus] OnSceneLoaded: UI Update finished. Current Gold: {gold}");
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"[RangerStatus] AddGold: Gold increased by {amount}. New Gold: {gold}");
        UpdateUI();
    }

    public void AddExp(int amount)
    {
        exp += amount;
        int neededExp = baseExpToLevelUp + (level - 1) * 5;

        Debug.Log($"[RangerStatus] AddExp: Exp increased by {amount}. Current Exp: {exp}/{neededExp}");

        if (exp >= neededExp)
        {
            exp = neededExp;
            readyToLevelUp = true;
            Debug.Log("[RangerStatus] EXP FULL. Siap naik level!");
        }
        UpdateUI();
    }

    public void LevelUp()
    {
        if (!readyToLevelUp) 
        {
            Debug.LogWarning("[RangerStatus] LevelUp: Not ready to level up.");
            return;
        }

        level++;
        exp = 0;
        readyToLevelUp = false;
        Debug.Log($"[RangerStatus] LevelUp: Leveled up to Level {level}. Exp reset.");
        UpdateUI();
    }

    private void UpdateUI()
    {
        int neededExp = baseExpToLevelUp + (level - 1) * 5;

        if (goldText != null) goldText.text = "Gold: " + gold;
        if (expText != null) expText.text = "EXP: " + exp + "/" + neededExp;
        if (levelText != null) levelText.text = "Level: " + level;

        Debug.Log($"[RangerStatus] UpdateUI: UI updated. Gold: {gold}, Exp: {exp}, Level: {level}");
    }
}