using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helical_Wheel_App
{
    public class AminoAcids
    {
        private static List<char> LetterPolarAminoAcids = new List<char>
        {
            'R','K','D','E','Q','N','H','S','T','Y','C','W'
        };
        private static List<char> LetterNonPolarAminoAcids = new List<char>
        {
            'A','I','L','M','F','V','P','G'
        };
        private static List<string> ThreeLetterPolarAminoAcids = new List<string>
        {
            "Arg","Lys","Asp","Glu","Gln","Asn","His","Ser","Thr","Tyr","Cys","Trp"
        };
        private static List<string> ThreeLetterNonPolarAminoAcids = new List<string>
        {
            "Ala","Ile","Leu","Met","Phe","Val","Pro","Gly"
        };
        public bool IsPolar(string aminoAcid = "", char aminoAcidChar = '0')
        {
            if (!aminoAcidChar.Equals('0'))
            {
                if (LetterPolarAminoAcids.Contains(aminoAcidChar.ToString().ToUpper().ToCharArray()[0]))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (ThreeLetterPolarAminoAcids.Contains(aminoAcid.Substring(0, 1).ToString().ToUpper() + aminoAcid.Substring(1)))
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsAminoAcid(string aminoAcid = "", char aminoAcidChar = '0')
        {
            if (!aminoAcidChar.Equals('0'))
            {
                if (LetterPolarAminoAcids.Contains(aminoAcidChar.ToString().ToUpper().ToCharArray()[0]) || LetterNonPolarAminoAcids.Contains(aminoAcidChar.ToString().ToUpper().ToCharArray()[0]))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (ThreeLetterPolarAminoAcids.Contains(aminoAcid.Substring(0, 1).ToString().ToUpper() + aminoAcid.Substring(1)) || ThreeLetterNonPolarAminoAcids.Contains(aminoAcid.Substring(0, 1).ToString().ToUpper() + aminoAcid.Substring(1)))
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsValidOneLetterAppreviation(string aminoSequence)
        {
            var val = true;
            foreach(var letter in aminoSequence)
            {
                if (!char.IsLetter(letter))
                {
                    val = false;
                    break;
                }
            }
            return val;
        }
        public bool IsValidThreeLetterAppreviation(string aminoSequence)
        {
            var val = true;
            var list = aminoSequence.Split(',').ToList();
            foreach(var item in list)
            {
                if(item.Length != 3)
                {
                    val = false;
                    break;
                }
                foreach (var letter in item)
                {
                    if (!char.IsLetter(letter))
                    {
                        val = false;
                        break;
                    }
                }
                if (val == false)
                    break;
            }
            return val;
        }
        public bool ContainsBulkyAminos(string firstAmino, string secondAmino)
        {
            if(firstAmino.Length > 1)
            {
                var bulkyAminos = new List<string>
                {
                    "phe","tyr","trp"
                };
                if (bulkyAminos.Contains(firstAmino.ToLower()) && bulkyAminos.Contains(secondAmino.ToLower()))
                    return true;
            }
            else
            {
                var bulkyAminos = new List<string>
                {
                    "f","y","w"
                };
                if (bulkyAminos.Contains(firstAmino.ToLower()) && bulkyAminos.Contains(secondAmino.ToLower()))
                    return true;
            }
            return false;
        }
    }
}
