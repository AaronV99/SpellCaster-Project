using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Resistances", menuName = "Damage Resistances")]
public class Resistances : ScriptableObject
{
    [System.Serializable]
    public struct Resistance
    {
        [Tooltip("Choose the damage type of which enemies will receive damage from")]
        public DamageType damageType;

        [Tooltip("The percent amount that enemies will receive from a damage type. example: 80 will result in the spell/attack/damage type doing 20% less damage")]
        public int percentDamageToTake;

    }

    public List<Resistance> resistances = new List<Resistance>();

    public int TotalDamageTaken(int damage, DamageType damageType)
    {

        for (int i = 0; i < resistances.Count; i++)
        {
            if(resistances[i].damageType == damageType)
            {
                //calculating damage
                return ((damage * resistances[i].percentDamageToTake) / 100);
            }
        }
        return 0;
    }


}
