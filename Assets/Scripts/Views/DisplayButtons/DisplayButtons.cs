using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DisplayButtons<T> : MonoBehaviour
{
    [SerializeField] protected GameObject buttonPrefab;
    [SerializeField] protected Transform buttonHolder;
    protected bool displayed = false;

    [Space]
    [SerializeField] protected int rows;
    [SerializeField] protected float buttonSpacing;
    [SerializeField] protected float rowSpacing;

    [Space]
    [SerializeField] protected bool shouldSwitchOnPress = false;
    [SerializeField] protected SwitchScreen switchScreen;
    [SerializeField] protected ScreenRef screenRef;

    [Space]
    [SerializeField] protected EventSystem eventSystem;

    protected List<GameObject> buttons = new List<GameObject>();

    protected void DisplayButtonsByArray(T[] values)
    {
        if (displayed && buttons.Count > 0) return;

        int buttonsPerRow = (values.Length + 1) / rows;
        int height = rows;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < buttonsPerRow; j++)
            {
                int index = (i * buttonsPerRow) + j;
                if (index >= values.Length) break;

                Vector3 offset = new Vector3(j * buttonSpacing, -i * rowSpacing);

                GameObject newButton = Instantiate(buttonPrefab, buttonHolder);
                newButton.transform.localPosition = offset;

                SetValuesForButton(newButton, values[index]);

                buttons.Add(newButton);
            }
        }

        Vector3 holderOffset = new Vector3(-((buttonsPerRow - 1) * buttonSpacing) / 2, 0);
        buttonHolder.localPosition += holderOffset;
        displayed = true;
    }

    protected abstract void SetValuesForButton(GameObject newButton, T value);

    protected void SwitchScreenOnButtonUse()
    {
        switchScreen.NavigateToScreen(screenRef);
    }

    protected void SetSelectedButton<R>(R reference)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            SetButton<R> buttonSetting = buttons[i].GetComponent<SetButton<R>>();
            if (buttonSetting.ButtonValue.Equals(reference))
            {
                eventSystem.SetSelectedGameObject(buttons[i]);
                return;
            }
        }

        if (buttons.Count > 0) eventSystem.SetSelectedGameObject(buttons[0]);
    }
}
