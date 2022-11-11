using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public int health;
    public int damage;
    public UnitBase(int hp, int dmg) {
        health = hp;
        damage = dmg;
    }
    public void Update() {
        if (health <= 0) {Destroy(this);}
    }
}
