using UnityEngine;
using TMPro; // Підключаємо бібліотеку для роботи з UI текстом

public class BuildingManager : MonoBehaviour
{
    [Header("Налаштування будівництва")]
    public BuildingData buildingData; 
    public LayerMask groundLayer;     // Шар для землі, щоб ставити будівлі
    public LayerMask buildingLayer;   // Шар для існуючих будівель, щоб їх видаляти

    [Header("Ресурси та UI")]
    public int coins = 500;           // Вимога 2: Починаємо з 500 монет
    public TMP_Text coinsUI;          // Вимога 4: Вивід у UI

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        UpdateUI();
    }

    void Update()
    {
        // Ліва кнопка миші — Будівництво
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceBuilding();
        }

        // Права кнопка миші — Видалення (Вимога 1)
        if (Input.GetMouseButtonDown(1))
        {
            TryRemoveBuilding();
        }
    }

    void TryPlaceBuilding()
    {
        if (buildingData == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            // Вимога 3: Перевірка ресурсів
            if (coins >= buildingData.cost)
            {
                coins -= buildingData.cost;
                UpdateUI();
                Instantiate(buildingData.prefab, hit.point, Quaternion.identity);
            }
            else
            {
                // Вимога 3: Повідомлення в консоль
                Debug.Log("Недостатньо ресурсів");
            }
        }
    }

    void TryRemoveBuilding()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, buildingLayer))
        {
            // Перевіряємо, чи є на об'єкті наш скрипт-маркер
            if (hit.collider.GetComponent<Building>() != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void UpdateUI()
    {
        if (coinsUI != null)
        {
            coinsUI.text = "Монети: " + coins;
        }
    }
}