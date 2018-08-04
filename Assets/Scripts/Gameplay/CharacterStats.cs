using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterStats : MonoBehaviour
{

    [SerializeField]
    private int maxHealth, health, armour, speed, finesse, charaNum;
    [SerializeField]
    private WeaponStats weaponEquiped;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private TMP_Text HPText, statText;
    [SerializeField]
    private Animator characterAnimator;
    [SerializeField]
    private GameObject damagePopUpPrefab;
    [SerializeField]
    private Transform damagePopUpLoc;
    [SerializeField]
    private Transform pointerLoc;

    [Header("Enemy Stuff")]

    [SerializeField]
    private bool isEnemy;
    [SerializeField]
    private GameObject tbox;
    [SerializeField]
    private Text tbox_text;

    private bool _isDead;

    void Start()
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.minValue = 0;
        hpSlider.value = health;
        HPText.text = GetHealthDisplay();
        if (!isEnemy)
        {
            statText.text = GetStatsDisplay();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        resetUI();
    }

    public string GetHealthDisplay()
    {
        return health + "/" + maxHealth;
    }

    public string GetStatsDisplay()
    {
        return "Armor: " + armour + "  Finesse: " + finesse + "%";
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

    public void SetFinesse(int i)
    {
        finesse = i;
    }

    public int GetFinesse()
    {
        return finesse;
    }

    public void SetArmour(int i)
    {
        armour = i;
    }

    public int GetArmour()
    {
        return armour;
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
        if (HPText != null)
        {
            HPText.text = health.ToString();
        }
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("takingDamage");
        }
        if (damagePopUpPrefab != null)
        {
            print("spawnDamagePopUp");
            GameObject temp = Instantiate(damagePopUpPrefab, damagePopUpLoc.position, damagePopUpLoc.rotation);
            temp.GetComponent<TMP_Text>().text = i.ToString();
        }
        if (health < 1)
        {
            _isDead = true;
            hpSlider.fillRect.gameObject.SetActive(false);
        }
    }

    public WeaponStats GetWeapon()
    {
        return weaponEquiped;
    }

    public void SetWeapon(WeaponStats newWeapon)
    {
        weaponEquiped = newWeapon;
    }

    public bool isDead()
    {
        return _isDead;
    }

    public Transform GetDamagePopUpLoc()
    {
        return damagePopUpLoc;
    }

    public Transform GetPointerLoc()
    {
        return pointerLoc;
    }

    public void deathAnimation()
    {
        characterAnimator.SetBool("isDead", true);
    }

    public void attackAnimation()
    {
        characterAnimator.SetTrigger("attack");
    }

    public void SetActiveTbox(bool state)
    {
        tbox.SetActive(state);
    }

    public Text GetTboxText()
    {
        return tbox_text;
    }

    public void startWalkingAnimation()
    {
        characterAnimator.SetBool("isWalking", true);
    }

    public GameObject GetHPSlider()
    {
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

    public void SetMaxHealth(int n_maxHeath)
    {
        maxHealth = n_maxHeath;
    }

    public void resetUI()
    {
        if (charaNum != 0)
        {
            if (characterAnimator == null || hpSlider == null || HPText == null || damagePopUpLoc == null || pointerLoc == null || statText == null)
            {
                GameObject[] player = GameObject.FindGameObjectsWithTag("Player" + charaNum);
                for (int j = 0; j < player.Length; j++)
                {
                    if (player[j].GetComponent<Animator>() != null)
                    {
                        characterAnimator = player[j].GetComponent<Animator>();
                    }
                    else
                    if (player[j].GetComponent<Slider>() != null)
                    {
                        hpSlider = player[j].GetComponent<Slider>();
                        hpSlider.maxValue = health;
                        hpSlider.minValue = 0;
                        hpSlider.value = health;
                    }
                    else
                    if (player[j].GetComponent<TMP_Text>() != null && player[j].name == "HP Text")
                    {
                        HPText = player[j].GetComponent<TMP_Text>();
                        HPText.text = GetHealthDisplay();
                    }
                    else if (player[j].GetComponent<TMP_Text>() != null && player[j].name == "Stats" && !isEnemy)
                    {
                        statText = player[j].GetComponent<TMP_Text>();
                        statText.text = GetStatsDisplay();
                    }
                    else
                    if (player[j].name == "DamagePopUpLoc")
                    {
                        damagePopUpLoc = player[j].transform;
                    }
                    else
                    if (player[j].name == "PointerLoc")
                    {
                        pointerLoc = player[j].transform;
                    }
                }
            }
        }
    }

}
