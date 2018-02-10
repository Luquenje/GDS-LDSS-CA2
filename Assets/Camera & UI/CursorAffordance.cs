using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField]
    Texture2D walkCursor = null;
    [SerializeField]
    Texture2D targetCursor = null;
    [SerializeField]
    Texture2D unknownCursor = null;
    [SerializeField]
    Vector2 cursorHotSpot = new Vector2(0, 0);
    [SerializeField]
    Vector2 cursorHotSpotTarget = new Vector2(0, 0);
    [SerializeField]
    Vector2 cursorHotSpotUnknown = new Vector2(0, 0);

    [SerializeField]
    const int walkableLayerNumber = 8;
    [SerializeField]
    const int enemyLayerNumber = 9;
    [SerializeField]
    const int stiffLayerNumber = 10;

    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
    }

    // Update is called once per frame
    void OnLayerChanged(int newLayer)
    { //only called when layer changes
        //print(cameraRaycaster.layerHit);
        switch (newLayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(targetCursor, cursorHotSpotTarget, CursorMode.Auto);
                break;
            //case CameraRaycaster.Layer.RaycastEndStop:
            //    Cursor.SetCursor(unknownCursor, cursorHotSpotUnknown, CursorMode.Auto);
            //    break;
            default:
                Cursor.SetCursor(unknownCursor, cursorHotSpotUnknown, CursorMode.Auto);
                return;
        }

    }
}
