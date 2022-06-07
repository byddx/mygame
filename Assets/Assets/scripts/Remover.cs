using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;
	private PlayerHealth playerHealth;

	void Awake()
	{
		// Setting up the reference.
		playerHealth = GameObject.Find("Hero").GetComponent<PlayerHealth>();
	}
	void Update()
    {
		if (playerHealth.health <= 0)
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			//防止英雄销毁后再去获取位置出现NullReferenceException
			//if (GameObject.Find("UI_HealthBar").activeSelf)
			//{
			//	GameObject.Find("UI_HealthBar").SetActive(false);
			//}



			StartCoroutine("ReloadGame");
		}
	}

    void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.gameObject.tag == "Player")
		{
			
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			//防止英雄销毁后再去获取位置出现NullReferenceException
			if(GameObject.Find("UI_HealthBar").activeSelf)
			{
				GameObject.Find("UI_HealthBar").SetActive(false);
			}

			
			Instantiate(splash, col.transform.position, transform.rotation);
			
			Destroy (col.gameObject);
			
            
			StartCoroutine("ReloadGame");
		}
		else
		{
			
			Instantiate(splash, col.transform.position, transform.rotation);

			
			Destroy (col.gameObject);	
		}
		
	}

	IEnumerator ReloadGame()
	{			
		
		yield return new WaitForSeconds(2);
		
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
