# Cultist-Simulator-Modding-Toolkit
Tool to aid in the creation of mods for Cultist Simulator by Weather Factory


# Building
You will need to provide a reference to Assembly-CSharp.dll from your copy of Cultist Simulator in order to build this project.
This file is located in the `Cultist Simulator/cultistsimulator_Data/Managed/` folder.

# Images
In order to display the vanilla images, you will need to extract and sort them yourselves (unless permission is granted to redistribute the images).

The toolkit will look for these images in `Cultist Simulator/cultistsimulator_Data/StreamingAssets/content/core/images/`
 - Aspect icons in `images/icons40/aspects`
 - Element icons in `images/elementart`
 - Legacy icons in `images/icons100/legacies`
 - Verb icons in `images/icons100/verbs`
 - Ending art in `images/endingart`
 - Burn images in `images/burnimages`

# Mod Folder Structure
 - Your Mod Folder
   - `content`
     - json files in folders or not, structured however you want as long as they have the `.json` extension
   - `images`
     - `burnimages`
     - `elementart`
     - `endingart`
     - `icons40`
       - `aspects`
     - `icons100`
       - `legacies`
	   - `verbs`