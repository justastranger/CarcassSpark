 - Several Search Operations were temporarily disabled so that I could push a release on day 1 of Exile, they need to be fixed and re-enabled.
 - Add support for the other 2 languages
 - Once I figure out how to guarantee that the user has access to the game's assets, switch over to ListView's in the ModViewer form so that everything can be displayed with their icons and sorted similarly to frangiclave (vertically as one segregated ListView)
 - "Publish Mod" tool
   - Compresses mod into archive wherever you tell it to
   - Prompt for new version number?
   - Save with CustomManifest property of "release": true
 - Automatically size the splitters for the ModViewerTabControl

# Currently Unsupported Properties
 - List$remove(List\<string\> values) (Only works for List\<String\>, and so it does not work for, for example, slots)
   - Basically everywhere List$remove is used

# Properties That Need to be Implemented in Viewer Forms
  - Element stuff for aspects

