using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifebarUpdater : MonoBehaviour {
    [SerializeField] PlayerReference playerRef;
    [SerializeField] Slider slider;

    private void Reset() {
        slider = GetComponent<Slider>();
    }

    void Start() {
        playerRef.Instance.Health.OnDamage += UpdateHealth;
        playerRef.Instance.Health.OnHeal += UpdateHealth;
        UpdateHealth(0);
    }

    private void OnDestroy() {
        playerRef.Instance.Health.OnDamage -= UpdateHealth;
        playerRef.Instance.Health.OnHeal -= UpdateHealth;
    }

    public void UpdateHealth(int damage) {
        slider.value = Mathf.Max(0, (float)playerRef.Instance.Health.CurrentHealth / (float)playerRef.Instance.Health.MaxHealth);
    }
}
