using UnityEngine;

[CreateAssetMenu(fileName = "SpellDataSO", menuName = "Scriptable Objects/SpellDataSO")]
public class SpellDataSO : ScriptableObject
{
    public string spellName;
    public int damage;
    public float cooldown;
    public float origincooldown;
    public float speed;
    public int weight;
    public int count;
    public GameObject spellPrefab;

}
