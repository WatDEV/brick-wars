using Assets.Characters;
using Assets.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueScript : MonoBehaviour {

    public Sprite RhinoT1;
    public Sprite RhinoT2;
    public Sprite JumperT1;
    public Sprite JumperT2;
    public Sprite TowerT1;
    public Sprite TowerT2;
    public Sprite ShieldT1;
    public Sprite ShieldT2;
    public Sprite CatterpillarT1;
    public Sprite CatterpillarT2;

    public List<Image> Images; 

    public void UpdateSprites(LinkedList<Character> queue)
    {
        var i = 0;
        foreach (var c in queue)
        {
            Images[i].sprite = GetSprite(c);
            Images[i].color = c.CharacterAttributes.isStunned?
                new Color(0.3f,0.3f,0.3f):
                new Color(1f,1f,1f);
            i++;
            if (i >= 10)
                break;
        }
    }

    private Sprite GetSprite(Character c)
    {
        switch (c.Type)
        {
            case CharacterEnum.Rhino:
                if (c.Team == 1)
                    return RhinoT1;
                return RhinoT2;
            case CharacterEnum.Jumper:
                if (c.Team == 1)
                    return JumperT1;
                return JumperT2;
            case CharacterEnum.Tower:
                if (c.Team == 1)
                    return TowerT1;
                return TowerT2;
            case CharacterEnum.Shield:
                if (c.Team == 1)
                    return ShieldT1;
                return ShieldT2;
            case CharacterEnum.Worm:
                if (c.Team == 1)
                    return CatterpillarT1;
                return CatterpillarT2;
        }
        return null;
    }
}
