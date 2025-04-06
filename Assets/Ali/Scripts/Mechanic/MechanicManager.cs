using System.Collections.Generic;
using UnityEngine;

public class MechanicManager
{
    private readonly Dictionary<int, List<IMechanic>> levelMechanics = new();
    private readonly List<IMechanic> activeMechanics = new();
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void RegisterMechanic(int level, IMechanic mechanic)
    {
        if (!levelMechanics.ContainsKey(level))
            levelMechanics[level] = new List<IMechanic>();
        levelMechanics[level].Add(mechanic);
    }

    public void ActivateMechanicsForLevel(int level)
    {
        if (!levelMechanics.TryGetValue(level, out var mechanics)) return;
        foreach (var mechanic in mechanics)
        {
            if (activeMechanics.Contains(mechanic)) continue;
            mechanic.Initialize(character);
            mechanic.Enable();
            activeMechanics.Add(mechanic);
        }
    }

    public void UpdateMechanics()
    {
        foreach (var mechanic in activeMechanics)
        {
            if (mechanic is IUpdatableMechanic updatable)
                updatable.Update();
        }
    }
}
