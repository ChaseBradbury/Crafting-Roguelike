using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUIController : MonoBehaviour
{
    [SerializeField] float decayTime;
    [SerializeField] float spacing;
    [SerializeField] EffectIconController iconTemplate;
    [SerializeField] Transform effectListTransform;
    private Dictionary<string, EffectIconController> iconDictionary;

    void Start()
    {
        iconDictionary = new Dictionary<string, EffectIconController>();
    }

    public void AddEffectIcon(CombatEffectSO effect, bool decays)
    {
        EffectIconController iconController = Instantiate(iconTemplate, effectListTransform).GetComponent<EffectIconController>();
        iconController.gameObject.SetActive(true);
        iconDictionary[effect.effectCode] = iconController;
        iconController.SetEffect(effect, decays);
    }

    public void DeleteEffectIcon(CombatEffectSO effect)
    {
        if (iconDictionary.ContainsKey(effect.effectCode))
        {
            Destroy(iconDictionary[effect.effectCode].gameObject);
            iconDictionary.Remove(effect.effectCode);
        }
    }

    public void FixedUpdate()
    {
        List<string> iconsToRemove = new List<string>();
        int i = 0;
        foreach (KeyValuePair<string, EffectIconController> entry in iconDictionary)
        {
            bool decayed = entry.Value.Decay(decayTime);

            if (!decayed)
            {
                entry.Value.SetPosition(i * spacing);
                ++i;
            }
            else
            {
                iconsToRemove.Add(entry.Key);
            }
        }
        foreach (string code in iconsToRemove)
        {
            Destroy(iconDictionary[code].gameObject);
            iconDictionary.Remove(code);
        }
    }
}
