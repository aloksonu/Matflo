using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject coinPrefab;
    public GameObject[] stepsPrefab;
    public GameObject obstacalPrefab;
    public GameObject parentPlatFormPrefab;
    public GameObject parentCoinPrefab;
    public GameObject parentObstacalPrefab;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private Rigidbody2D rb;

    private float playerPositionXtoSpawanPlateform;
    private float PlatformPMultiplyer;
    private float PlatformSpawnPositionX;
    private int plateformCount;

    private int stepsCounter;
    internal int totalSteps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPositionXtoSpawanPlateform = this.transform.position.x+9;
        PlatformSpawnPositionX = 0f;
        PlatformPMultiplyer = platformPrefab.transform.localScale.x;
        plateformCount = 2;
        stepsCounter = 1;
        totalSteps = stepsPrefab.Length;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        if(this.transform.position.x > playerPositionXtoSpawanPlateform)
        {
            playerPositionXtoSpawanPlateform = playerPositionXtoSpawanPlateform + 9;
            PlatformSpawnPositionX = PlatformPMultiplyer* plateformCount;
            SpawnPlatform(PlatformSpawnPositionX);
            plateformCount++;

            float coinTransformPositionX = Random.Range(this.transform.position.x+4 , this.transform.position.x +9f);
            float coinTransformPositionY = Random.Range(-3.3f, 0f);
            //SpawnCoin(coinTransformPositionX, coinTransformPositionY);
            StepsCoin(coinTransformPositionX, coinTransformPositionY);

            //float obstacalTransformPositionX = Random.Range(this.transform.position.x + 4, this.transform.position.x + 9f);
            //float obstacalTransformPositionY = Random.Range(-3.3f, 0f);
            float obstacalTransformPositionX = Random.Range(coinTransformPositionX+5, coinTransformPositionX+8);
            SpawnObstacal(obstacalTransformPositionX, -3.73f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
        }
    }

    void SpawnPlatform(float x)
    {
        GameObject g = Instantiate(platformPrefab, new Vector3(x, platformPrefab.transform.position.y, 0f), Quaternion.identity);
        g.transform.SetParent(parentPlatFormPrefab.transform);
    }
    void SpawnCoin(float x , float y)
    {
        GameObject g = Instantiate(coinPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        g.transform.SetParent(parentCoinPrefab.transform);
    }
    void SpawnObstacal(float x, float y)
    {
        GameObject g = Instantiate(obstacalPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        g.transform.SetParent(parentObstacalPrefab.transform);
    }
    void StepsCoin(float x, float y)
    {
        GameObject g = Instantiate(stepsPrefab[stepsCounter], new Vector3(x, y, 0f), Quaternion.identity);
        g.transform.SetParent(parentCoinPrefab.transform);
        stepsCounter++;
    }
}
