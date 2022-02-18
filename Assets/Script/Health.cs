using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth
{
    // Champs
    [SerializeField] int _startHealth;
    [SerializeField] int _maxHealth;

    [Space]
    [SerializeField] UnityEvent<int> onDamage;
    [SerializeField] UnityEvent _onDeath;

    // Propriétés
    public bool CanTakeDamage { get; set; }
    public int CurrentHealth { get; private set; }
    public int MaxHealth => _maxHealth;
    public bool IsDead => CurrentHealth <= 0;

    // Events
    public event UnityAction OnSpawn;
    public event UnityAction<int> OnDamage { add => onDamage.AddListener(value); remove => onDamage.RemoveListener(value); }
    public event UnityAction<int> OnHeal;
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    // Methods
    void Awake() => Init();

    void Init()
    {
        CurrentHealth = _startHealth;
        CanTakeDamage = true;
        OnSpawn?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) throw new ArgumentException($"Argument amount {nameof(amount)} is negativ");

        if (!CanTakeDamage) { return; }

        var tmp = CurrentHealth;
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        var delta = CurrentHealth - tmp;
        onDamage?.Invoke(delta);

        if(CurrentHealth <= 0)
        {
            _onDeath?.Invoke();
        }
    }

    public void Heal(int amount) {
        if (amount < 0) throw new ArgumentException($"Argument amount {nameof(amount)} is negativ");

        int tmp = CurrentHealth;
        CurrentHealth = Mathf.Max(CurrentHealth + amount, MaxHealth);
        int delta = CurrentHealth - tmp;

        OnHeal?.Invoke(delta);
    }

    [Button("test")]
    void MaFonction()
    {
        var enumerator = MesIntPrefere();

        while(enumerator.MoveNext())
        {
            Debug.Log(enumerator.Current);
        }
    }


    List<IEnumerator> _coroutines;

    IEnumerator<int> MesIntPrefere()
    {

        //

        var age = 12;

        yield return 12;


        //

        yield return 3712;

        age++;
        //

        yield return 0;



        //
        yield break;
    }





}
