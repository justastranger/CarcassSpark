using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public static class Utilities
    {
        public static List<ModViewer> currentMods = new List<ModViewer>();


        public static bool aspectExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.aspectExists(id)) return true;
            }
            return false;
        }

        public static Aspect getAspect(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getAspect(id) != null)
                {
                    return mv.getAspect(id);
                }
            }
            return null;
        }

        public static bool elementExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.elementExists(id)) return true;
            }
            return false;
        }

        public static Element getElement(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getElement(id) != null)
                {
                    return mv.getElement(id);
                }
            }
            return null;
        }

        public static bool recipeExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.recipeExists(id)) return true;
            }
            return false;
        }

        public static Recipe getRecipe(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getRecipe(id) != null)
                {
                    return mv.getRecipe(id);
                }
            }
            return null;
        }

        public static bool deckExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.recipeExists(id)) return true;
            }
            return false;
        }

        public static Deck getDeck(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getDeck(id) != null)
                {
                    return mv.getDeck(id);
                }
            }
            return null;
        }

        public static bool legacyExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.legacyExists(id)) return true;
            }
            return false;
        }

        public static Legacy getLegacy(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getLegacy(id) != null)
                {
                    return mv.getLegacy(id);
                }
            }
            return null;
        }

        public static bool endingExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.endingExists(id)) return true;
            }
            return false;
        }

        public static Ending getEnding(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getEnding(id) != null)
                {
                    return mv.getEnding(id);
                }
            }
            return null;
        }

        public static bool verbExists(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.verbExists(id)) return true;
            }
            return false;
        }

        public static Verb getVerb(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                if (mv.getVerb(id) != null)
                {
                    return mv.getVerb(id);
                }
            }
            return null;
        }
    }
}
