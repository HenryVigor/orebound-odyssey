using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : BaseEnemy
{
    //public float knockback;

    public float stunRate = 0.15f;

    private AIChase aiChase;

    private SpriteRenderer enemySprite;

    // Sound
    public EnemyAudio enemyAudio;

    private void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        enemySprite.color = new(255f / 255f, 255f / 255f, 255f / 255f);
        currentHealth = maxHealth;
        aiChase = GetComponent<AIChase>();
    }

    public override void Damage(int damageAmount)
    {
        if (isKillable)
        {
            currentHealth -= damageAmount;
            // Knockback
            //transform.position = Vector2.MoveTowards(this.transform.position, Player.Obj.transform.position, -1 * knockback * Time.deltaTime);
            // Stun
            aiChase.canMove = false;
            Invoke("ResetMove", stunRate);
        }
        // Play hurt sound
        if (enemyAudio != null) {
            enemyAudio.PlaySoundHurt();
        }

        if (currentHealth <= 0)
        {
            aiChase.canMove = false;
            isKillable = false;
            // Fade out on death instead of the insta-destroy
            StartCoroutine(FadeAway(0.15f));
        }
    }

    public override void ObjectDestroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator FadeAway(float time)
    {
        float timeCount = 0;
        while (timeCount < time)
        {
            float t = timeCount / time;
            enemySprite.color = Color.Lerp(Color.white, new(0, 0, 0, 0), t);
            timeCount += Time.deltaTime;
            yield return null;
        }
        enemySprite.color = new(0, 0, 0, 0);
        ObjectDestroy();
    }

    private void ResetMove()
    {
        if (aiChase != null)
        {
            aiChase.canMove = true;
        }
        else
        {
            Debug.LogError("AIChase component not found!");
        }
    }

    public override void SetHealthBonus(float healthBonus)
    {
        maxHealth = Mathf.FloorToInt(maxHealth+(healthBonus*maxHealth));
        currentHealth = maxHealth;
    }
}
