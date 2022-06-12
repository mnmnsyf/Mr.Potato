using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius = 10f;          // 消灭敌人的半径
    public float bombForce = 100f;          //从爆炸中炸出敌人的力量
    public AudioClip boom;                  // 爆炸的音频
    public AudioClip fuse;                  // 保险丝音频
    public float fuseTime = 1.0f;
    public GameObject explosion;            // 预设体爆炸效果


    private LayBombs layBombs;              // 引用玩家的LayBombs脚本
    private PickupSpawner pickupSpawner;    // PickupSpawner脚本的引用
    private ParticleSystem explosionFX;     // 引用粒子系统的爆炸效果


    void Awake()
    {
        explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
        if (GameObject.FindGameObjectWithTag("Player"))
            layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
    }
    void Start()
    {
        if (transform.root == transform)
            StartCoroutine(BombDetonation());
    }

    // Update is called once per frame
    IEnumerator BombDetonation()
    {
        AudioSource.PlayClipAtPoint(fuse, transform.position);

        // 等待 几秒.
        yield return new WaitForSeconds(fuseTime);

        Explode();
    }

	public void Explode()
	{

		// 玩家现在可以自由放置炸弹
		layBombs.bombLaid = false;

		// 开始一个新的拾取
		pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

		//在bomradius的enemies中找到所有的碰撞器
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius, 1 << LayerMask.NameToLayer("Enemies"));

		foreach (Collider2D en in enemies)
		{
			// 检查它是否有一个刚体(因为每个敌人只有一个刚体，在父节点)
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if (rb != null && rb.tag == "Enemy")
			{
				rb.gameObject.GetComponent<Enemy>().HP = 0;
				Vector3 deltaPos = rb.transform.position - transform.position;
				Vector3 force = deltaPos.normalized * bombForce;
				rb.AddForce(force);
			}
		}

		explosionFX.transform.position = transform.position;
		explosionFX.Play();

		Instantiate(explosion, transform.position, Quaternion.identity);

		AudioSource.PlayClipAtPoint(boom, transform.position);

		Destroy(gameObject);
	}
}
