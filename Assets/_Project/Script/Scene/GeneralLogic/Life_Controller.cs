using System;
using UnityEngine;

public class Life_Controller : MonoBehaviour, I_IDamage
{
    [Header("Setting")]
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHp = 100;

    public Action<int, int> OnLFChange;
    public Action OnDeath;

    private void OnEnable()
    {
        hp = maxHp;
        OnLFChange?.Invoke(Hp, MaxHp);
    }

    public int Hp { get => hp; set => hp = Mathf.Clamp(value, 0, maxHp); }
    public int MaxHp { get => maxHp; set => maxHp = Mathf.Max(0,value); }

    public void UpdateHp(int ammount)
    {
        Debug.Log("Damage");
        Hp += ammount;
        OnLFChange?.Invoke(Hp, MaxHp);

        if (Hp <= 0) OnDeath?.Invoke();
    }

    public void Damage(int ammount) => UpdateHp(ammount);

    private void OnDisable()
    {
        hp = maxHp;
        OnLFChange?.Invoke(Hp, MaxHp);
    }
}
