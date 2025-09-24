using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    GameObject MissilePrefab;
    Transform LaunchPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (!MissilePrefab || !LaunchPosition) return;

        Instantiate(MissilePrefab, LaunchPosition.position, LaunchPosition.rotation);
    }
}
