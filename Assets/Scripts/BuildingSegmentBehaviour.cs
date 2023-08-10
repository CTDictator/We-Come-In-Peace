using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;


public class BuildingSegmentBehaviour : MonoBehaviour
{
    // Reference to the game manager.
    private GameManager gameManager;
    // Building segment health.
    public Slider bISlider;
    // Building segment variables.
    public ParticleSystem buildingCollapseFX;
    public GameObject SmokeFX;
    private const int buildingIntegrityMax = 3;
    [SerializeField] private int buildingIntegrity = buildingIntegrityMax;
    [SerializeField] private bool freeFall;
    // Building segment acessible properties.
    public bool FreeFall
    {
        get { return freeFall; }
    }
    // Constants of the building segment.
    private static readonly float buildingLinearVelocityMax = 2.0f;

    // Set up and display building integrity display as well as game manager reference.
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bISlider.maxValue = buildingIntegrity;
        bISlider.value = buildingIntegrity;
        bISlider.gameObject.SetActive(false);
    }
    // Remove the building segment from play when its integrity reaches zero.
    private void Update()
    {
        // Check if building is in freefall.
        if (!freeFall) freeFall = CheckFreefalling();
        // Blow up building segment when integrity reaches zero.
        BlowUpBuildingSegment();
    }

    // If the building segment is falling or collides with a spaceship, destroy it.
    private void OnCollisionEnter(Collision collision)
    {
        // If the building segment is struck by a projectile, reduce integrity by 1 level.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            --buildingIntegrity;
            if (buildingIntegrity > 0) DisplayBuildingIntegrity();
            gameManager.gameAudio.PlayOneShot(gameManager.explosionSound);
        }
        // If the building is in free fall ...
        else if (freeFall)
        {
            // If the building segment falls onto a non free falling segment, blow it up.
            if (collision.gameObject.CompareTag("Building") 
                && !collision.gameObject.GetComponent<BuildingSegmentBehaviour>().FreeFall)
            {
                buildingIntegrity = 0;
            }
            // If the building segment falls onto the ground, blow it up.
            else if (collision.gameObject.CompareTag("Ground"))
            {
                buildingIntegrity = 0;
            }
        }
        // If the building segment is struck by a spaceship, blow it up.
        else if (collision.gameObject.CompareTag("Player"))
        {
            buildingIntegrity = 0;
        }
    }

    // Check the building segments integrity and remove it from play when reaching zero.
    private void BlowUpBuildingSegment()
    {
        // If building took any damage, make it start smoking.
        if (!SmokeFX.activeInHierarchy && buildingIntegrity < buildingIntegrityMax)
        {
            SmokeFX.SetActive(true);
        }
        // If building segment reaches zero, remove it from play.
        else if (buildingIntegrity <= 0)
        {
            // Play destruction sound.
            gameManager.gameAudio.PlayOneShot(gameManager.glassbreakingSound);
            // Add score and remove object.
            gameManager.Score += 1;
            gameManager.UpdateScore();
            Instantiate(buildingCollapseFX,transform.position, buildingCollapseFX.transform.rotation);
            gameObject.SetActive(false);
        }
    }

    // Check the magnitude of the building to determine if it is in freefall or not.
    private bool CheckFreefalling()
    {
        // Once an object starts freefalling, it is forever considered freefalling.
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > buildingLinearVelocityMax)
            return true;
        return false;
    }

    // Update the integrity bar for the building.
    private void DisplayBuildingIntegrity()
    {
        if (!bISlider.IsActive()) bISlider.gameObject.SetActive(true);
        bISlider.value = buildingIntegrity;
    }
}
