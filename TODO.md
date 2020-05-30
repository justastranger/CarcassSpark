 - RecipeViewer needs to have Property Operations implemented for Warmup and Max Executions

 - Several Search Operations were temporarily disabled so that I could push a release on day 1 of Exile, they need to be fixed and re-enabled.

 - Add support for the other 2 languages

 # Property Operations
 - List$remove(List\<string\> values) (Only works for List<String>, and so it does not work for slots)
   - Basically everywhere List$remove is used
 - Int$add(int value)
 - Int$minus(int value)
   - RecipeViewer
     - Warmup
     - Max Executions
   - Element
     - Lifetime
     - Animation Frames


# Currently Unsupported Properties
 - bool Recipe.signalimportantloop
 - string Recipe.portaleffect: Implemented in RecipeViewer but not Recipe
   - "None",
   - "Wood",
   - "WhiteDoor",
   - "StagDoor",
   - "SpiderDoor",
   - "PeacockDoor",
   - "TricuspidGate"
 - string Recipe.signalEndingFlavour: Implemented in RecipeViewer but not Recipe
   - "None",
   - "Grand",
   - "Melancholy",
   - "Pale",
   - "Vile"

# Properties That Need to be Implemented in Viewer Forms
 - Dictionary<string, int> Recipe.purge(string elementId, int maxToPurge)
 - Dictionary<string, int> Recipe.haltverb(string verbId, int maxToHalt)
 - Dictionary<string, int> Recipe.deleteverb(string verbId, int maxToDelete)
