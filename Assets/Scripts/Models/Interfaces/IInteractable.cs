using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract(Player player);

    public void OnInteractHoldStart(Player player);
    public void OnInteractHoldEnd(Player player);
}
