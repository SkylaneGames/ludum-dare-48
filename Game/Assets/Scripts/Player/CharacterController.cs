using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Color SkinColour = Color.white;
    public Color ShirtColour = Color.white;
    public Color HairColour = Color.white;
    public Color TrouserColour = Color.white;

    public Color[] SkinTones = {
        new Color(197f / 255f, 140f / 255f, 133f / 255f),
        new Color(236 / 255f, 188f / 255f, 180f / 255f),
        new Color(209f / 255f, 163f / 255f, 164f / 255f),
        new Color(161f / 255f, 102f / 255f, 94f / 255f),
        new Color(80f / 255f, 51f / 255f, 53f / 255f),
        new Color(89f / 255f, 47f / 255f, 42f / 255f),
    };

    public Color[] HairColours = {
        new Color(170f / 255f, 136f / 255f, 102f / 255f),
        new Color(222f / 255f, 190f / 255f, 153f / 255f),
        new Color(36f / 255f, 28f / 255f, 17f / 255f),
        new Color(79f / 255f, 26f / 255f, 0f / 255f),
        new Color(154f / 255f, 51f / 255f, 0f / 255f),
    };

    public Color[] ShirtColours = {
        new Color(1f, 0.2f, 0.2f)
    };

    public Color[] TrouserColours = {
        new Color(0.239f, 0.161f, 0.137f)
    };

    public SpriteRenderer HairRenderer;
    public SpriteRenderer ShirtRenderer;
    public SpriteRenderer SkinRenderer;
    public SpriteRenderer ArmRenderer;
    public SpriteRenderer TrouserRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PickRandomShirtColour();
        PickRandomSkinColour();
        PickRandomHairColour();
        PickRandomTrouserColour();
        UpdateRenderers();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    void PickRandomSkinColour()
    {
        SkinColour = SkinTones[Random.Range(0, SkinTones.Length)];
    }

    void PickRandomShirtColour()
    {
        ShirtColour = ShirtColours[Random.Range(0, ShirtColours.Length)];
    }

    void PickRandomHairColour()
    {
        HairColour = HairColours[Random.Range(0, HairColours.Length)];
    }

    void PickRandomTrouserColour()
    {
        TrouserColour = TrouserColours[Random.Range(0, TrouserColours.Length)];
    }

    void UpdateRenderers()
    {
        HairRenderer.color = HairColour;
        ShirtRenderer.color = ShirtColour;
        SkinRenderer.color = SkinColour;
        ArmRenderer.color = SkinColour;
        TrouserRenderer.color = TrouserColour;
    }
}
