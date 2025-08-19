using Unity.VisualScripting;
using UnityEngine;

public interface IDamageble
{
    int health { get; set; }


    void TakeDamage(int damage);
    


}
