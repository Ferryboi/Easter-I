using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SetButton<T> : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField] private TextMeshProUGUI text;

    public T ButtonValue => buttonValue;
    protected T buttonValue;

    public delegate void OnButtonUseDelegate();
    public OnButtonUseDelegate OnButtonUsed;

    public delegate void OnButtonSelectedDelegate(T value);
    public OnButtonSelectedDelegate OnButtonSelected;

    public void SetButtonValues(string name, T value)
    {
        text.text = name;
        buttonValue = value;
    }

    public virtual void UseButton() { OnButtonUsed?.Invoke(); }

    public void OnSelect(BaseEventData eventData)
    {
        OnButtonSelected?.Invoke(buttonValue);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonSelected?.Invoke(buttonValue);
    }
}
