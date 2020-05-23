# Installation
Acquire the binaries (by any means necessary) and place them in your Cultist Simulator installation Folder.

If you downloaded it through Steam like I did, that will be in `steamapps/common/Cultist Simulator`.


# Cultist-Simulator-Modding-Toolkit
Tool to aid in the creation of mods for Cultist Simulator by Weather Factory

Currently Supported:
 - Creation of:
   - Aspects
    - Induces
     - Induces$prepend
     - Induces$append
   - Elements
    - Aspects
     - Aspects$extend
     - Aspects$remove
    - XTriggers (aka Cross Triggers)
     - XTriggers$extend
     - XTriggers$remove
    - Slots
     - Slots$prepend
     - Slots$append
   - Recipes
    - Requirements
     - Requirements$extend
     - Requirements$remove
    - Table Requirements
     - tablereqs$extend
     - tablereqs$remove
    - Extant Requirements
     - extantreqs$extend
     - extantreqs$remove
    - Effects
     - Effects$extend
     - Effects$remove
    - Aspect Effects
     - Aspects$extend
     - Aspects$remove
    - Deck Effects
     - deckeffect$extend
     - deckeffect$remove
    - Alternative Recipe Links
     - alternativerecipes$prepend
     - alternativerecipes$append
    - Linked Recipe Links
     - linked$prepend
     - linked$prepend
    - Mutations
     - mutations$prepend
     - mutations$prepend
    - Slots
    - Internal Decks
   - Decks
    - DrawMessages
     - drawmessages$extend
     - drawmessages$remove
    - Spec (The actual Deck)
     - spec$prepend
     - spec$append
     - spec$remove
   - Legacies
    - Effects
     - effects$extend
     - effects$remove
	- Exclude on Ending
	 - excludeonending$prepend
	 - excludeonending$append
	 - excludeonending$remove
   - Endings
   - Verbs
    - Slots
   - Manifests
    - Dependencies

Currently Unsupported
 - Property Operations
  - Most Property Operations are supported, but there's an oversight with how List$Remove works, making it useless for anything but a List<string>


# Building
You will need to provide a reference to Assembly-CSharp.dll from your copy of Cultist Simulator in order to build this project.
This file is located in the `Cultist Simulator/cultistsimulator_Data/Managed/` folder.

Newtonsoft's JSON.net is needed to compile the program and can be found through NuGet.

MindFusion LLC has compiled a custom set of assemblies so that this project didn't need a license key. These assemblies only allow this project to not need the license key. I've been granted permission to redistribute these binaries with the source code so that y'all can use it to compile the program too :)

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