using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnimalStatus : MonoBehaviour
{
    [Header("Makan")]
    public bool isHungry = false;
    public float minHungerInterval = 5f;
    public float maxHungerInterval = 30f;
    private float hungerTimer;

    [Header("Kebersihan")]
    public bool isDirty = false;
    public float minDirtyInterval = 10f;
    public float maxDirtyInterval = 60f;
    private float dirtyTimer;

    [Header("Indikator Visual")]
    [SerializeField] private GameObject hungerIndicator;
    [SerializeField] private GameObject dirtyIndicator;

    private void Start()
    {
        SetNewHungerTimer();
        SetNewDirtyTimer();

        if (hungerIndicator != null) hungerIndicator.SetActive(false);
        if (dirtyIndicator != null) dirtyIndicator.SetActive(false);
    }

    private void Update()
    {
        // Timer lapar
        hungerTimer -= Time.deltaTime;
        if (hungerTimer <= 0f && !isHungry)
        {
            isHungry = true;
            if (hungerIndicator != null) hungerIndicator.SetActive(true);
        }

        // Timer kotor
        dirtyTimer -= Time.deltaTime;
        if (dirtyTimer <= 0f && !isDirty)
        {
            isDirty = true;
            if (dirtyIndicator != null) dirtyIndicator.SetActive(true);
        }
    }

    public void Feed()
    {
        if (isHungry)
        {
            isHungry = false;
            SetNewHungerTimer();
            if (hungerIndicator != null) hungerIndicator.SetActive(false);
            Debug.Log($"{gameObject.name} sudah diberi makan!");
        }
    }

    public void Clean()
    {
        if (isDirty)
        {
            isDirty = false;
            SetNewDirtyTimer();
            if (dirtyIndicator != null) dirtyIndicator.SetActive(false);
            Debug.Log($"{gameObject.name} sudah dibersihkan kandangnya!");
        }
    }

    private void SetNewHungerTimer()
    {
        hungerTimer = Random.Range(minHungerInterval, maxHungerInterval);
    }

    private void SetNewDirtyTimer()
    {
        dirtyTimer = Random.Range(minDirtyInterval, maxDirtyInterval);
    }
}
