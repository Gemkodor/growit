using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] Sprite[] states;
    [SerializeField] float[] steps;
    SpriteRenderer spriteRenderer;
    [SerializeField] int minHarvestingRatio = 1;
    [SerializeField] int maxHarvestingRatio = 3;
    float lifeTime = 0;
    int currentState = 0;

    public bool fullGrowth = false;
    public Sprite illustration;

    // Start is called before the first frame update
    void Start()
    {
        SetFirstState();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        SetCurrentState();
    }

    private void SetFirstState()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = states[0];
    }

    void SetCurrentState()
    {
        for (int i = 0; i < states.Length - 1; i++)
        {
            if (lifeTime >= steps[i] && currentState == i)
            {
                spriteRenderer.sprite = states[i + 1];
                currentState = i + 1;

                if (currentState == 4)
                {
                    fullGrowth = true;
                }
            }
        }
    }

    public int GetHarvestingQty()
    {
        return Random.Range(minHarvestingRatio, maxHarvestingRatio);
    }
}
