using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CultistSimulatorModdingToolkit.ObjectViewers;
using CultistSimulatorModdingToolkit.ObjectTypes;

namespace CultistSimulatorModdingToolkit
{
    public class Utilities
    {
        public static List<ModViewer> currentMods = new List<ModViewer>();
        private static string directoryToVanillaContent = "./cultistsimulator_Data/StreamingAssets/content/core/";


        public static string getIdType(string id)
        {
            if (aspectExists(id)) return "aspect";
            if (elementExists(id)) return "element";
            if (recipeExists(id)) return "recipe";
            if (deckExists(id)) return "deck";
            if (legacyExists(id)) return "legacy";
            if (endingExists(id)) return "ending";
            if (verbExists(id)) return "verb";
            return "unknown";
        }

        public static Image getAspectImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/icons40/aspects/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if(File.Exists(directoryToVanillaContent + "/images/elementArt/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/elementArt/_x.png");
            return null;
        }

        public static Image getElementImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/elementArt/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/elementArt/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/elementArt/_x.png");
            return null;
        }

        public static Image getEndingImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/endingArt/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/endingArt/despair.png")) return Image.FromFile(directoryToVanillaContent + "/images/endingArt/despair.png");
            return null;
        }

        public static Image getLegacyImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/icons100/legacies/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/icons100/legacies/ritual.png")) return Image.FromFile(directoryToVanillaContent + "/images/icons100/legacies/ritual.png");
            return null;
        }

        public static Image getVerbImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/icons100/verbs/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/icons100/verbs/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/icons100/verbs/_x.png");
            return null;
        }

        public static Image getCardBackImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/cardBacks/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/cardBacks/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/cardBacks/_x.png");
            return null;
        }

        public static Image getBurnImage(string id)
        {
            foreach (ModViewer mv in currentMods)
            {
                string pathToImage = mv.currentDirectory + "/images/burnImages/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            return null;
        }

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
