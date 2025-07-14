using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerInteract : MonoBehaviour
{
    public float interactRange = 1f;
    public LayerMask animalLayer;
    // HAPUS ATAU KOMENTARI INI: public RangerStatus rangerStatus; 

    public void InteractFeed()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, interactRange, animalLayer);
        if (target != null)
        {
            AnimalStatus status = target.GetComponent<AnimalStatus>();
            if (status != null && status.isHungry)
            {
                status.Feed();
                // AKSES MELALUI SINGLETON INSTANCE
                if (RangerStatus.Instance != null)
                {
                    RangerStatus.Instance.AddExp(1);
                    RangerStatus.Instance.AddGold(Random.Range(5, 16));
                    Debug.Log("Feed: Added Exp/Gold via RangerStatus.Instance.");
                }
                else
                {
                    Debug.LogError("RangerStatus.Instance is NULL in InteractFeed!");
                }
            }
        }
    }

    public void InteractClean()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, interactRange, animalLayer);
        if (target != null)
        {
            AnimalStatus status = target.GetComponent<AnimalStatus>();
            if (status != null && status.isDirty)
            {
                status.Clean();
                // AKSES MELALUI SINGLETON INSTANCE
                if (RangerStatus.Instance != null)
                {
                    RangerStatus.Instance.AddExp(1);
                    RangerStatus.Instance.AddGold(Random.Range(5, 16));
                    Debug.Log("Clean: Added Exp/Gold via RangerStatus.Instance.");
                }
                else
                {
                    Debug.LogError("RangerStatus.Instance is NULL in InteractClean!");
                }
            }
        }
    }

    // Perhatikan: Fungsi InteractGeneral di sini dan di Update() melakukan hal yang sama.
    // Anda mungkin ingin menghapus salah satunya atau memanggil satu sama lain.
    public void InteractGeneral()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }

    private void Update()
    {
        // Interaksi Feed via Key E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, interactRange, animalLayer);
            if (target != null)
            {
                AnimalStatus status = target.GetComponent<AnimalStatus>();
                if (status != null && status.isHungry)
                {
                    status.Feed();
                    // AKSES MELALUI SINGLETON INSTANCE
                    if (RangerStatus.Instance != null)
                    {
                        RangerStatus.Instance.AddExp(1);
                        RangerStatus.Instance.AddGold(Random.Range(5, 16)); // 5 - 15 gold
                        Debug.Log("Key E (Feed): Added Exp/Gold via RangerStatus.Instance.");
                    }
                    else
                    {
                        Debug.LogError("RangerStatus.Instance is NULL when pressing E!");
                    }
                }
            }
        }

        // Interaksi Clean via Key F
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, interactRange, animalLayer);
            if (target != null)
            {
                AnimalStatus status = target.GetComponent<AnimalStatus>();
                if (status != null && status.isDirty)
                {
                    status.Clean();
                    // AKSES MELALUI SINGLETON INSTANCE
                    if (RangerStatus.Instance != null)
                    {
                        RangerStatus.Instance.AddExp(1);
                        RangerStatus.Instance.AddGold(Random.Range(5, 16)); // 5 - 15 gold
                        Debug.Log("Key F (Clean): Added Exp/Gold via RangerStatus.Instance.");
                    }
                    else
                    {
                        Debug.LogError("RangerStatus.Instance is NULL when pressing F!");
                    }
                }
            }
        }

        // Interaksi General via Key T (atau tombol interaksi umum lainnya)
        if (Input.GetKeyDown(KeyCode.T))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach (Collider2D hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    break;
                }
            }
        }
    }

    // Ini adalah fungsi Interact() yang overload (sama nama dengan IInteractable.Interact())
    // HATI-HATI: Jika script ini sendiri adalah IInteractable, ini bisa menyebabkan loop atau kebingungan.
    // Jika tidak digunakan, lebih baik dihapus.
    private void Interact() 
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }
}