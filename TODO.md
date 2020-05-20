# Property Operations
 - List$append(List<T> values)
 - List$prepend(List<T> values)
 - List$remove(List<string> values) (Only works for List<String>, and so it does not work for slots)
 - Int$add(int value)
 - Int$minus(int value)
 - Dictionary$extend(Dictionary<T, T> newValues)
 - Dictionary$remove(List<string> keys)

 - Legacy.statusbarelements

 - bool Recipe.signalimportantloop
 - string Recipe.portaleffect:
   - "None",
   - "Wood",
   - "WhiteDoor",
   - "StagDoor",
   - "SpiderDoor",
   - "PeacockDoor",
   - "TricuspidGate"
 - string Recipe.signalEndingFlavour:
   - "None",
   - "Grand",
   - "Melancholy",
   - "Pale",
   - "Vile"
 - Dictionary<string, int> Recipe.purge(elementId, maxToPurge)
 - Dictionary<string, int> Recipe.haltverb(string verbId, int maxToHalt)
 - Dictionary<string, int> Recipe.deleteverb(string verbId, int maxToDelete)


 RecipeViewer needs to have Property Operations implemented for Warmup and Max Executions