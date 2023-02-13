using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {
    public class GlobalVars : MonoBehaviour
    {

        public enum ElementType
        {
            FIRE,
            WATER,
            NATURE,
            EARTH,
            AIR,
            ICE
        }

        [SerializeField] public static Dictionary<ElementType, Color> ElementColors = new Dictionary<ElementType, Color>(){
            {ElementType.FIRE,      HexToColor("F14838")},
            {ElementType.WATER,     HexToColor("147EF8")},
            {ElementType.NATURE,    HexToColor("77C04F")},
            {ElementType.EARTH,     HexToColor("93672C")},
            {ElementType.AIR,       HexToColor("FFFDC6")},
            {ElementType.ICE,       HexToColor("70F1E9")},
        };

        public static Color HexToColor(string hex) {
            return new Color(
                (float)int.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.AllowHexSpecifier) / 255, 
                (float)int.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.AllowHexSpecifier) / 255, 
                (float)int.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.AllowHexSpecifier) / 255
            );
        }
        
        public static Color HexToColor(string hex, float alpha) {
            return new Color(
                (float)int.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.AllowHexSpecifier), 
                (float)int.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.AllowHexSpecifier), 
                (float)int.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.AllowHexSpecifier),
                alpha
            );
        }
    }
}
