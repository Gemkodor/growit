using UnityEngine;

public class CropField : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        bool cropFieldComplete = true;
        foreach(Transform child in transform)
        {
            if (!child.GetComponent<Plant>().isPlantOccupied())
            {
                cropFieldComplete = false;
            }
        }

        if (gm.isPlanting)
        {
            gm.SetPlanting(!cropFieldComplete);
        }
        
    }
}
