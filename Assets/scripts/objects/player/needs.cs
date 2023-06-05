using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class needs : MonoBehaviour
{
    public float maxHarvest = 200f;
    public float maxHealth = 200f;
    public float harvest = 200f;
    public float speedOfHarvest = 2f;
    public float health = 200f;
    public float speedOfHealth = 10f;
    public float spawnModifHealth = 0.5f;
    public float spawnModifHarvest = 0.5f;
    public GameObject harvestUp;
    public GameObject healthUp;
    public GameObject harvestUnder;
    public GameObject healthUnder;
    public GameObject spawn;
    public thingsInteract thingsInteract;

    public bool sprint = false;

    private RectTransform rectTransfromHarvest;
    private RectTransform rectTransfromHealth;
    private RectTransform rectTransfromMaxHarvest;
    private RectTransform rectTransfromMaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
        harvestUp = GameObject.FindGameObjectWithTag("HarvestUp");
        healthUp = GameObject.FindGameObjectWithTag("HealthUp");
        spawn = GameObject.FindGameObjectWithTag("Respawn");
        rectTransfromHarvest = harvestUp.GetComponent<RectTransform>();
        rectTransfromHealth = healthUp.GetComponent<RectTransform>();
        rectTransfromMaxHarvest = harvestUnder.GetComponent<RectTransform>();
        rectTransfromMaxHealth = healthUnder.GetComponent<RectTransform>();
        harvestupdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.Pause)
        {
            if(rectTransfromHarvest)
                rectTransfromHarvest.sizeDelta = new Vector2(harvest / 100, 0.39f);
            if(rectTransfromHealth)
                rectTransfromHealth.sizeDelta = new Vector2(health / 100, 0.39f);
            if(rectTransfromMaxHarvest)
                rectTransfromMaxHarvest.sizeDelta = new Vector2(maxHarvest / 100, 0.39f);
            if (rectTransfromMaxHealth)
                rectTransfromMaxHealth.sizeDelta = new Vector2(maxHealth / 100, 0.39f);
            if (health <= 0)
            {
                thingsInteract.removeObjFromHands();
                transform.position = spawn.transform.position;
                health = maxHealth * spawnModifHealth;
                harvest = maxHarvest * spawnModifHarvest;
            }
        }
    }
    private async void harvestAsync()
    {
        await Task.Run(() => harvestupdate());
    }
    private void harvestupdate()
    {
        StartCoroutine(Harvest());
    }
    IEnumerator Harvest()
    {
        yield return new WaitForSeconds(0.05f);
        if(!pause.Pause)
            harvest = harvest - speedOfHarvest / 100;
        rectTransfromHarvest = harvestUp.GetComponent<RectTransform>();
        rectTransfromHarvest.sizeDelta = new Vector2(harvest / 100, 0.39f);
        if (harvest > 0)
            StartCoroutine(Harvest());
        else
            StartCoroutine(Health());
    }
    IEnumerator Health()
    {
        yield return new WaitForSeconds(0.01f);
        if (!pause.Pause)
            health -= speedOfHealth / 100;
        if (harvest > 0)
            StartCoroutine(Harvest());
        else
            StartCoroutine(Health());
    }
    public void Eat(float satiety)
    {
        if (harvest + satiety > maxHarvest)
            satiety = maxHarvest - harvest;
        harvest += satiety;
    }
    public void Heal(float aid)
    {
        if (health + aid > maxHealth)
            aid = maxHealth - health;
        health += aid;
    }
    public void GetDamage(float damage)
    {
        if (health - damage < 0)
            damage = health;
        health -= damage;
    }
    public void setHealth(float num)
    {
        //harvest += satiety;
       // rectTransfromHarvest.transform.position = new Vector2(rectTransfromHarvest.transform.position.x + satiety / 4, rectTransfromHarvest.transform.position.y);
    }
    public void setHarvest(float num)
    {

    }
}
