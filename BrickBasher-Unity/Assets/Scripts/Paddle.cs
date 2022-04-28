/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Bobby Ouyang
 * Last Edited: April 28, 2022
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle
    public GameObject paddle;

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position = pos;


    }//end Update()


}
