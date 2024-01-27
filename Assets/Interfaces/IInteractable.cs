using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerController interactor);
    bool CanInteract(PlayerController interactor) => true;
    bool ShouldHighlight() => false;
    string PopupText();
}
public static class InteractableExtensions
{
    public static IEnumerable<Material> GetMaterials(this MonoBehaviour monoBehaviour)
        => monoBehaviour.GetComponentsInChildren<Renderer>()?.SelectMany(renderer => renderer.materials);
}
