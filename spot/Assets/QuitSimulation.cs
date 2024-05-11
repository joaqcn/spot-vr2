using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitSimulation : MonoBehaviour
{
   public void QuitButton()
   {
      Application.Quit();
      Debug.Log("Quiting game....");
   }
}
