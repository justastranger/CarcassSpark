# Installation
Acquire the binaries (by any means necessary) and place them in your Cultist Simulator installation Folder.

Pre-compiled binaries can be found [here](https://github.com/justastranger/CarcassSpark/releases).

If you downloaded it through Steam like I did, the correct folder will be in `steamapps/common/Cultist Simulator`.

# Starting Out
One of the first things you should do is open the vanilla files. There's a checkbox in the Settings menu that causes this to happen automatically. This allows all of the vanilla content to be inspected wherever it comes up in the Viewe forms. You'll be presented with a Mod Viewer. It's a simple overview, listing everything that's part of that particular content source in searchable ListBoxes.

To start off with creating a new mod, we'll be going back to the Main Form where there's a handy "New Mod" button. It'll open a dialog window allowing you to choose the folder to use for the mod. There's a button to create a new folder on this dialog, if you need it.

Once you've chosen your new mod's folder, a manifest editor will appear for you to fill out. You can put in anything for this, or nothing at all if you're really lazy. Once you press the OK button to close the dialog, the manifest will be saved and your modding has officially begun!

Mods go into your `LocalLow/Weather Factory/Cultist Simulator/mods` folder on Windows. This folder can be accessed by entering `%appdata%/../locallow/Weather Factory/Cultist Simulator/` into explorer or by creating a shortcut to `%appdata%/../locallow/Weather Factory/Cultist Simulator/`.

# Property Support:
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
 - List$remove is unsupported for Lists that don't contain strings because of a bug in the game's Property Operations processor


# Building
You will need to provide a reference to Assembly-CSharp.dll from your copy of Cultist Simulator in order to build this project.
This file is located in the `Cultist Simulator/cultistsimulator_Data/Managed/` folder.

Newtonsoft's JSON.net is needed to compile the program and can be found through NuGet.

MindFusion LLC has compiled a custom set of assemblies so that this project didn't need a license key. These assemblies only allow this project to not need the license key. I've been granted permission to redistribute these binaries with the source code so that y'all can use it to compile the program too :)

# Images
In order to display the vanilla images, you will need to extract and sort them yourself (unless permission is granted to redistribute the images).

I've found that uTinyRipper works wonderfully for this purpose. It takes a bit of time to export everything and exports all the resources and metadata, but everything is pre-sorted and it's fairly easy to delete everything that's not a .png file.

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
     - json files in folders or not, structured however you want as long as they have the `.json` extension. Carcass Spark will save aspects in `aspects.json`, elements in `elements.json`, recipes in `recipes.json`, etc.
   - `images`
     - `burnimages`
     - `elementart`
     - `endingart`
     - `icons40`
       - `aspects`
     - `icons100`
       - `legacies`
       - `verbs`
     - `statusbarelements`
