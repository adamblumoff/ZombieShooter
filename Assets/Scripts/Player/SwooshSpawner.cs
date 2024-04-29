using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwooshSpawner : MonoBehaviour
{
    public Transform UpSpawner;
    public GameObject UpSwooshPrefab;
    public Transform DownSpawner;
    public GameObject DownSwooshPrefab;
    public Transform SideSpawner;
    public GameObject SideSwooshPrefab;
    public AudioClip attackClip;
    private Animator animator;
    private PlayerMovement playerMovement;
    public float speed = 5f;
    public bool rapidFire = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    // Update is called once per frame
    public void Swoosh(Transform spawner, GameObject prefab)
    {
        if (!animator.GetBool("isDead"))
        {
            GameObject swoosh = Instantiate(prefab, spawner.position, spawner.rotation);
            Rigidbody2D rb = swoosh.GetComponent<Rigidbody2D>();

            if (animator.GetBool("isHorizontal"))
            {
                rb.AddForce(spawner.right * speed, ForceMode2D.Impulse);
            }
            else
            {
                if (animator.GetBool("isDown"))
                {
                    rb.AddForce(-spawner.up * speed, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(spawner.up * speed, ForceMode2D.Impulse);
                }

            }

        }

    }
    public void SwooshAttack()
    {
        if (animator.GetBool("isAttacking"))
        {
            SoundManager.PlayAttackSound(attackClip);
            if (animator.GetBool("isHorizontal"))
            {
                Swoosh(SideSpawner, SideSwooshPrefab);
            }
            else
            {
                if (animator.GetBool("isDown"))
                {
                    Swoosh(DownSpawner, DownSwooshPrefab);
                }
                else
                {
                    Swoosh(UpSpawner, UpSwooshPrefab);
                }

            }

        }
    }
    public void IncreaseSpeed()
    {
        if(speed <=15f)
            speed *= 1.1f;
    }

    public void SetRapidFire()
    {
        rapidFire = true;
    }
    private IEnumerator RapidFireAnimation()
    {
        int noCollsionLayer = LayerMask.NameToLayer("NoCollision");
        int defaultLayer = LayerMask.NameToLayer("Default");
        gameObject.layer = noCollsionLayer;
        for (int i = 0; i < 25; i++)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255f, 215f, 0f, 0.75f);
            yield return new WaitForSeconds(.2f);
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSeconds(.2f);
        }
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        gameObject.layer = defaultLayer;
    }
    public void SetRapidFireAnimation()
    {
        if (rapidFire)
        {
            StartCoroutine(RapidFireAnimation());
        }
    }
}
