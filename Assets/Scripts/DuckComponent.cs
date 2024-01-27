using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckComponent : MonoBehaviour
{
    private readonly Dictionary<Type, List<Component>> componentsKeyedByType = new Dictionary<Type, List<Component>>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var component in this.gameObject.GetComponents(typeof(Component)))
        {
            if (!this.componentsKeyedByType.TryAdd(component.GetType(), new List<Component> { component }))
            {
                this.componentsKeyedByType[component.GetType()].Add(component);
            }
        }

        float fuzzAmount = 0.25f;
        var fuzzAmountVector = Vector3.up * Random.Range(1 - fuzzAmount, 1 + fuzzAmount);
        this.GetComponents<RotatorComponent>().ElementAt(1).ApplyRotationFuzz(fuzzAmountVector);
    }

    private new T GetComponent<T>() where T : Component
    {
        return GetComponents<T>().FirstOrDefault();
    }

    private new IReadOnlyCollection<T> GetComponents<T>() where T : Component
    {
        this.componentsKeyedByType.TryGetValue(typeof(T), out List<Component> components);
        return (components?.OfType<T>() ?? Enumerable.Empty<T>()).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
