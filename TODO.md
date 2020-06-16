 - RecipeViewer needs to have Property Operations implemented for Warmup and Max Executions
 - Several Search Operations were temporarily disabled so that I could push a release on day 1 of Exile, they need to be fixed and re-enabled.
 - Add support for the other 2 languages
 - Once I figure out how to guarantee that the user has access to the game's assets, switch over to ListView's in the ModViewer form so that everything can be displayed with their icons and sorted similarly to frangiclave (vertically as one segregated ListView)
 - Import Image toolstrip menu item in Mod Viewer's "File..." toolstrip menu
 - Import/Export feature for objects, exporting just the object in a JSON file to be imported into another mod
   - Export as "$(type)_$(id).json" by default in a directory of the user's choosing
 - Help button on Viewer forms to help explain the purposes of various properties

 # Property Operations

# Currently Unsupported Properties
 - List$remove(List\<string\> values) (Only works for List\<String\>, and so it does not work for, for example, slots)
   - Basically everywhere List$remove is used

# Properties That Need to be Implemented in Viewer Forms
 - Int$add(int value)
 - Int$minus(int value)
   - RecipeViewer
     - Warmup
     - Max Executions
   - Element
     - Lifetime
     - Animation Frames