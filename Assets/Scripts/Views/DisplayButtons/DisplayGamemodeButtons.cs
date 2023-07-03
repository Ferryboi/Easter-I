using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGamemodeButtons : DisplayButtons<GamemodeData>
{
    [Space][SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;

    private void Start()
    {
        DisplayButtonsByArray(GameManager.Instance.GetGamemodeData().GetGamemodes());
        SetSelectedButton(GameManager.Instance.GetLevelSettings().GetGamemode());

        if (shouldSwitchOnPress)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                SetButton<GamemodeRef> buttonSetting = buttons[i].GetComponent<SetButton<GamemodeRef>>();
                buttonSetting.OnButtonUsed += SwitchScreenOnButtonUse;
                buttonSetting.OnButtonSelected += DisplayGamemodeData;
            }
        }
    }

    private void OnEnable()
    {
        if(displayed) SetSelectedButton(GameManager.Instance.GetLevelSettings().GetGamemode());
    }

    private void OnDestroy()
    {
        if(shouldSwitchOnPress)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                SetButton<GamemodeRef> buttonSetting = buttons[i].GetComponent<SetButton<GamemodeRef>>();
                buttonSetting.OnButtonUsed -= SwitchScreenOnButtonUse;
                buttonSetting.OnButtonSelected -= DisplayGamemodeData;
            }
        }
    }

    protected override void SetValuesForButton(GameObject button, GamemodeData values)
    {
        SetButton<GamemodeRef> buttonSetting = button.GetComponent<SetButton<GamemodeRef>>();

        buttonSetting.SetButtonValues(values.Title, values.GamemodeRef);

        if (GameManager.Instance.GetLevelSettings().GetGamemode() == buttonSetting.ButtonValue)
        {
            DisplayGamemodeData(buttonSetting.ButtonValue);
        }
    }

    private void DisplayGamemodeData(GamemodeRef gamemodeRef)
    {
        GamemodeData gamemode = GameManager.Instance.GetGamemodeData().GetGamemode(gamemodeRef);
        if(gamemode)
        {
            image.sprite = gamemode.Image;
            description.text = gamemode.Description;
        }
    }
}
