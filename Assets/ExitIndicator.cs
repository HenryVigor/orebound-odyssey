using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitIndicator : MonoBehaviour
{
    TextMeshProUGUI exitText;
    private Vector3 trackingPos;
    [SerializeField] Transform playerTransform;
    private float distance = 0f;

    void Awake()
    {
        exitText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        distance = Vector3.Distance(trackingPos, playerTransform.position);
        exitText.text = "Exit: " + Mathf.FloorToInt(distance) + "m";
    }

    public void TrackNewItem(Vector3 newObjPos)
    {
        trackingPos = newObjPos;
    }

}
