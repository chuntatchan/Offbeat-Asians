using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{

    [SerializeField]
    private int maxHealth, health, armour, speed, finesse, charaNum;
    [SerializeField]
    private WeaponStats weaponEquiped;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Animator characterAnimator;

    private bool _isDead;

    void Start()
    {
        hpSlider.maxValue = health;
        hpSlider.minValue = 0;
        hpSlider.value = health;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        resetUI();
    }

    public void AttackAnim()
    {
        resetUI();
        characterAnimator.SetTrigger("attack");
    }

    public void SetWalkingAnim(bool state)
    {
        resetUI();
        characterAnimator.SetBool("isWalking", state);
    }

    public void SetSpeed(int i)
    {
        speed = i;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int weaponAttack(int i)
    {
        return weaponEquiped.weaponAttack(i);
    }

    public void takeDamage(int i)
    {
        resetUI();
        health -= i;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (hpSlider != null)
        {
            hpSlider.value = health;
        }
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("takingDamage");
        }
        if (health < 1)
        {
            _isDead = true;
			hpSlider.fillRect.gameObject.SetActive (false);
        }
    }

    public WeaponStats GetWeapon()
    {
        return weaponEquiped;
    }

    public bool isDead()
    {
        return _isDead;
    }

    public void deathAnimation()
    {
        characterAnimator.SetBool("isDead", true);
    }

    public void attackAnimation()
    {
        characterAnimator.SetTrigger("attack");
    }

    public void startWalkingAnimation()
    {
        characterAnimator.SetBool("isWalking", true);
    }

	public GameObject GetHPSlider() {
		return hpSlider.gameObject;
	}

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void resetUI()
    {
        if (charaNum != 0)
        {
            if (characterAnimator == null || hpSlider == null)
            {
                GameObject[] player = GameObject.FindGameObjectsWithTag("Player" + charaNum);
                for (int j = 0; j < player.Length; j++)
                {
                    if (player[j].GetComponent<Animator>() != null)
                    {
                        characterAnimator = player[j].GetComponent<Animator>();
                    }
                    if (player[j].GetComponent<Slider>() != null)
                    {
                        hpSlider = player[j].GetComponent<Slider>();
                        hpSlider.maxValue = health;
                        hpSlider.minValue = 0;
                        hpSlider.value = health;
                    }
                }
            }
        }



    }

}
