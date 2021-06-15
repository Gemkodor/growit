using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    GameManager gm;
    GameManager.CROPS cropType;
    GameObject plantedCrop;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gm.isPlanting && plantedCrop == null)
            {
                // Instantiate crop and place it on the screen
                Vector3 cropPos = GetComponent<Renderer>().bounds.center;
                GameObject crop = Instantiate(gm.GetSelectedCrop());
                crop.transform.position = cropPos;

                plantedCrop = crop;
                cropType = gm.GetSelectedCropType();
                player.RemoveCrop(cropType, 1);
                gm.SetPlanting(false);
            }
            else if (!gm.isPlanting && plantedCrop != null)
            {
                Crop crop = plantedCrop.GetComponent<Crop>();
                if (crop.fullGrowth)
                {
                    player.AddCrop(cropType, crop.GetHarvestingQty());
                    Destroy(plantedCrop.gameObject);
                }
            }
        }
    }
}
