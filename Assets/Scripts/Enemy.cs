using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float navigationUpdate;
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    private Transform[] checkpoints;
    [SerializeField]
    private float healthPoints;

    private int target = 0;
    private Transform enemy;
    private Collider2D enemyCollider;
    private float navigationTime = 0;

    private void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        GameManager.Instance.RegisterEnemy(this);
    }

    // Move enemy towards next checkpoint

    private void Update()
    {
        if (checkpoints != null)
        {
            navigationTime += Time.deltaTime;

            if (navigationTime > navigationUpdate)
                if (target < checkpoints.Length)
                    enemy.position = Vector2.MoveTowards(enemy.position, checkpoints[target].position, navigationTime);
                else
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);

            navigationTime = 0;
        }
    }

    // Change target checkpoint
    // Remove on exit
    // Register hit by projectile

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
            target += 1;
        else if (other.tag == "Exit")
            GameManager.Instance.UnregisterEnemy(this);
        else if (other.tag == "Projectile")
        {
            Projectile newP = other.gameObject.GetComponent<Projectile>();
            GetHit(newP.AttackStrength);
            Destroy(other.gameObject);
        }
    }

    public void GetHit(int hitPoints)
    {
        if (healthPoints - hitPoints > 0)
            healthPoints -= hitPoints;
        else
        {
            GameManager.Instance.UnregisterEnemy(this);
            Destroy(this.gameObject);
        }
    }
}
