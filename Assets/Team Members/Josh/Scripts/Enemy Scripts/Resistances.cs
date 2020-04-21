using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Resistances", menuName = "Damage Resistances")]
public class Resistances : ScriptableObject
{
    [System.Serializable]
    public struct Resistance
    {

        public DamageType damageType;
        public int damageToTake;

    }

    public List<Resistance> resistances = new List<Resistance>();

    public int TotalDamageTaken(int damage, DamageType damageType)
    {

        for (int i = 0; i < resistances.Count; i++)
        {
            if(resistances[i].damageType == damageType)
            {
                return ((damage * resistances[i].damageToTake) / 100);
            }
        }
        return 0;
    }


}
