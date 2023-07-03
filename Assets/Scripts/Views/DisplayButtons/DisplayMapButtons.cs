using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMapButtons : DisplayButtons<MapData>
{
    [Space][SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;

    private void Start()
    {
        DisplayButtonsByArray(GameManager.Instance.GetMapData().GetMaps());
        SetSelectedButton(GameManager.Instance.GetLevelSettings().GetMapType());

        if (shouldSwitchOnPress)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                SetButton<MapRef> buttonSetting = buttons[i].GetComponent<SetButton<MapRef>>();
                buttonSetting.OnButtonUsed += SwitchScreenOnButtonUse;
                buttonSetting.OnButtonSelected += DisplayMapData;
            }
        }
    }

    private void OnEnable()
    {
        if(displayed) SetSelectedButton(GameManager.Instance.GetLevelSettings().GetMapType());
    }

    private void OnDestroy()
    {
        if (shouldSwitchOnPress)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                SetButton<MapRef> buttonSetting = buttons[i].GetComponent<SetButton<MapRef>>();
                buttonSetting.OnButtonUsed -= SwitchScreenOnButtonUse;
                buttonSetting.OnButtonSelected -= DisplayMapData;
            }
        }
    }

    protected override void SetValuesForButton(GameObject button, MapData values)
    {
        SetButton<MapRef> buttonSetting = button.GetComponent<SetButton<MapRef>>();

        buttonSetting.SetButtonValues(values.Title, values.MapRef);

        if (GameManager.Instance.GetLevelSettings().GetMapType() == buttonSetting.ButtonValue)
        {
            DisplayMapData(buttonSetting.ButtonValue);
        }
    }

    private void DisplayMapData(MapRef mapRef)
    {
        MapData map = GameManager.Instance.GetMapData().GetMap(mapRef);
        if (map)
        {
            image.sprite = map.Image;
            description.text = map.Description;
        }
    }
}
