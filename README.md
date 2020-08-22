# Installation
Acquire the binaries (by any means necessary) and place them in your Cultist Simulator installation Folder.

The latest release can be found [here](https://github.com/justastranger/CarcassSpark/releases/).

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
       - Aspects$add
       - Aspects$remove
     - XTriggers (aka Cross Triggers)
       - XTriggers$add
       - XTriggers$remove
     - Slots
       - Slots$prepend
       - Slots$append
   - Recipes
     - Requirements
       - Requirements$add
       - Requirements$remove
     - Table Requirements
       - tablereqs$add
       - tablereqs$remove
     - Extant Requirements
       - extantreqs$add
       - extantreqs$remove
     - Effects
       - Effects$add
       - Effects$remove
     - Aspect Effects
       - Aspects$add
       - Aspects$remove
     - Deck Effects
       - deckeffects$add
       - deckeffects$remove
     - Purge
       - Purge$add
       - Purge$remove
     - Halt Verb
       - HaltVerb$add
       - HaltVerb$remove
     - Delete Verb
       - DeleteVerb$add
       - DeleteVerb$remove
     - Alternative Recipe Links
       - alt$prepend
       - alt$append
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
       - drawmessages$add
       - drawmessages$remove
     - Spec (The actual Deck)
       - spec$prepend
       - spec$append
       - spec$remove
   - Legacies
     - Effects
       - effects$add
       - effects$remove
     - Exclude on Ending
       - excludeonending$prepend
       - excludeonending$append
       - excludeonending$remove
     - Table Images
     - New Start
   - Endings
   - Verbs
     - Slot
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
 - Aspect icons in `images/aspects`
 - Element icons in `images/elements`
 - Legacy icons in `images/legacies`
 - Verb icons in `images/verbs`
 - Ending art in `images/endings`
 - Burn images in `images/burns`

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