using UnityEngine;
using UnityEngine.Events;

public class HookADuckMiniGameManager : MonoBehaviour
{
    private int acquiredDucks = 0;
    public UnityEvent acquiredDuck = new UnityEvent();
    
    public void AcquireDuck(DuckComponent duck)
    {
        ++acquiredDucks;

        if (acquiredDucks == 3)
        {
            acquiredDuck.Invoke();
            FindObjectOfType<HookADuckGameZone>().ForceExitGameZone(FindObjectOfType<HookADuckPoleHookComponent>());
            acquiredDucks = 0;
        }
    }
}
