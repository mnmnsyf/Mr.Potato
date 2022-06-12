using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius = 10f;          // ������˵İ뾶
    public float bombForce = 100f;          //�ӱ�ը��ը�����˵�����
    public AudioClip boom;                  // ��ը����Ƶ
    public AudioClip fuse;                  // ����˿��Ƶ
    public float fuseTime = 1.0f;
    public GameObject explosion;            // Ԥ���屬ըЧ��


    private LayBombs layBombs;              // ������ҵ�LayBombs�ű�
    private PickupSpawner pickupSpawner;    // PickupSpawner�ű�������
    private ParticleSystem explosionFX;     // ��������ϵͳ�ı�ըЧ��


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

        // �ȴ� ����.
        yield return new WaitForSeconds(fuseTime);

        Explode();
    }

	public void Explode()
	{

		// ������ڿ������ɷ���ը��
		layBombs.bombLaid = false;

		// ��ʼһ���µ�ʰȡ
		pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

		//��bomradius��enemies���ҵ����е���ײ��
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius, 1 << LayerMask.NameToLayer("Enemies"));

		foreach (Collider2D en in enemies)
		{
			// ������Ƿ���һ������(��Ϊÿ������ֻ��һ�����壬�ڸ��ڵ�)
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
