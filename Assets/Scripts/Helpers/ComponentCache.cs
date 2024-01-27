using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentCache
{
    private readonly Dictionary<Type, List<Component>> componentsKeyedByType = new Dictionary<Type, List<Component>>();

    public void Populate(GameObject gameObject)
    {
        this.componentsKeyedByType.Clear();

        foreach (var component in gameObject.GetComponents(typeof(Component)))
        {
            if (!this.componentsKeyedByType.TryAdd(component.GetType(), new List<Component> { component }))
            {
                this.componentsKeyedByType[component.GetType()].Add(component);
            }
        }

        this.componentsKeyedByType.Add(typeof(Transform), new List<Component> { gameObject.transform });
    }

    public T GetComponent<T>() where T : Component
    {
        this.componentsKeyedByType.TryGetValue(typeof(T), out List<Component> components);
        return components?.OfType<T>().FirstOrDefault();
    }

    public IReadOnlyCollection<T> GetComponents<T>() where T : Component
    {
        this.componentsKeyedByType.TryGetValue(typeof(T), out List<Component> components);
        return (components?.OfType<T>() ?? Enumerable.Empty<T>()).ToArray();
    }
}
