using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отображает стопку тарелок на стойке.
/// </summary>
public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVusialGameObjectList;

    /// <summary>
    /// Инициализирует список визуальных объектов тарелок.
    /// </summary>
    private void Awake()
    {
        plateVusialGameObjectList = new List<GameObject>();
    }

    /// <summary>
    /// Подписывается на события появления и удаления тарелок.
    /// </summary>
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    /// <summary>
    /// Добавляет новый визуальный объект тарелки в стопку.
    /// </summary>
    private void PlatesCounter_OnPlateSpawned()
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVusialGameObjectList.Count, 0);

        plateVusialGameObjectList.Add(plateVisualTransform.gameObject);
    }

    /// <summary>
    /// Удаляет верхний визуальный объект тарелки из стопки.
    /// </summary>
    private void PlatesCounter_OnPlateRemoved()
    {
        GameObject plateGameObject = plateVusialGameObjectList[plateVusialGameObjectList.Count - 1];
        if (plateGameObject == null) return;

        plateVusialGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);

    }
}
