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
    }

    public T GetComponent<T>() where T : Component
    {
        return this.GetComponents<T>().FirstOrDefault();
    }

    public IEnumerable<T> GetComponents<T>() where T : Component
    {
        return this.componentsKeyedByType.GetValueOrDefault(typeof(T))?.OfType<T>()
            ?? Enumerable.Empty<T>();
    }
}
