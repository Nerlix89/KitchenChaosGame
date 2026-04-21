using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVusialGameObjectList;

    private void Awake()
    {
        plateVusialGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned()
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVusialGameObjectList.Count, 0);

        plateVusialGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved()
    {
        GameObject plateGameObject = plateVusialGameObjectList[plateVusialGameObjectList.Count - 1];
        if (plateGameObject == null) return;

        plateVusialGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);

    }
}
