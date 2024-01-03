using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
   [SerializeField]
   float currentExp, maxExp, currentLevel;
   public ExpBar expBar;

   public void ExpPlayer(float expAmount)
   {
     currentExp += expAmount;
      expBar.SetSlider(currentExp);
    if( currentExp >= maxExp)
    {
        LevelUp();
    }
   }
   private void LevelUp()
   {
       currentLevel++;
       currentExp = 0;
       maxExp += 100;
   }
}
