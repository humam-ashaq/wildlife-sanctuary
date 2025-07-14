using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeInteract : MonoBehaviour, IInteractable
{
    // Tambahkan variabel ini
    public string nextSceneName; 

    public void Interact()
    {
        Debug.Log("Office Interacted!");

        var rangerStatus = RangerStatus.Instance;
        if (rangerStatus == null)
        {
            Debug.LogError("RangerStatus.Instance is NULL! Make sure RangerStatus script is active and set up as a singleton.");
            return;
        }

        int neededExpForLevelUp = rangerStatus.baseExpToLevelUp + (rangerStatus.level - 1) * 5;
        Debug.Log($"RangerStatus: Current Exp = {rangerStatus.exp}, Needed Exp = {neededExpForLevelUp}, Ready to Level Up = {rangerStatus.readyToLevelUp}");

        if (rangerStatus.readyToLevelUp)
        {
            Debug.Log("Ready to level up! Leveling...");
            rangerStatus.LevelUp(); 

            // ***** UBAH BARIS INI *****
            // Gunakan variabel nextSceneName yang akan diatur di Inspector
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName); 
                Debug.Log($"Attempting to load scene: {nextSceneName}");
            }
            else
            {
                Debug.LogError("nextSceneName is not set in OfficeInteract Inspector!");
            }
        }
        else
        {
            Debug.Log("Not ready to level up yet. Exp not max or other conditions not met.");
            // Opsional: Tampilkan pesan di UI kepada pemain kalau EXP belum cukup
        }
    }
}