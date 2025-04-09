using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class Data
{
    [Range(0f, 1f)]  
    public float value;
    public float valueMax=1;
    public float time;
    public float timeRate;
    public float timeFrameRate =0;
    public Data() { 
    
    
    }
}
public class DataAgent : MonoBehaviour
{
    public Data Energy = new Data();
    public Data Sleep = new Data();
    public Data WC = new Data();
    Coroutine CoroutineEnergy=null;
    Coroutine CoroutineSleep = null;
    Coroutine CoroutineWC = null;

    public bool CantLoadEnergy { get => CoroutineEnergy == null; }
    public bool CantLoadSleep { get => CoroutineSleep == null; }
    public bool CantLoadWC { get => CoroutineWC == null; }

    // === ENERGÍA ===
    IEnumerator LoadEnergyTime(float time)
    {
        while(time > 0)
        {
            time--;
            Energy.value = Mathf.Lerp(Energy.value, Energy.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        Energy.value = Energy.valueMax;
        StopCoroutine(CoroutineEnergy);
        CoroutineEnergy = null;
    }

    public void LoadEnergy()
    {
        if(CoroutineEnergy == null)
            CoroutineEnergy = StartCoroutine(LoadEnergyTime(Energy.time));
    }

    public void DiscountEnergy()
    {
        if(Energy.timeFrameRate > Energy.timeRate)
        {
            Energy.timeFrameRate = 0;
            Energy.value = Mathf.Max(0, Energy.value - 0.03f);
        }
        Energy.timeFrameRate += Time.deltaTime;
    }

    // === SUEÑO ===
    IEnumerator LoadSleepTime(float time)
    {
        while (time > 0)
        {
            time--;
            Sleep.value = Mathf.Lerp(Sleep.value, Sleep.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        Sleep.value = Sleep.valueMax;
        StopCoroutine(CoroutineSleep);
        CoroutineSleep = null;
    }

    public void LoadSleep()
    {
        if (CoroutineSleep == null)
            CoroutineSleep = StartCoroutine(LoadSleepTime(Sleep.time));
    }

    public void DiscountSleep()
    {
        if (Sleep.timeFrameRate > Sleep.timeRate)
        {
            Sleep.timeFrameRate = 0;
            Sleep.value = Mathf.Max(0, Sleep.value - 0.03f);
        }
        Sleep.timeFrameRate += Time.deltaTime;
    }

    // === WC ===
    IEnumerator LoadWCTime(float time)
    {
        while (time > 0)
        {
            time--;
            WC.value = Mathf.Lerp(WC.value, WC.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        WC.value = WC.valueMax;
        StopCoroutine(CoroutineWC);
        CoroutineWC = null;
    }

    public void LoadWC()
    {
        if (CoroutineWC == null)
            CoroutineWC = StartCoroutine(LoadWCTime(WC.time));
    }

    public void DiscountWC()
    {
        if (WC.timeFrameRate > WC.timeRate)
        {
            WC.timeFrameRate = 0;
            WC.value = Mathf.Max(0, WC.value - 0.03f);
        }
        WC.timeFrameRate += Time.deltaTime;
    }
}