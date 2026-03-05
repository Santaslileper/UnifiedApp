# dependencies/

This folder contains **pre-built importable binaries** that the application either links against or ships with.  
The source code for these is kept separately in `tools/` for reference.

| File/Folder | Origin | Used by |
|---|---|---|
| `interaction.dll` | `tools/interaction/` (Rust, legacy) | Not currently used — C# reimplementation in `src/Core.cs` |
| `libinteraction.dll.a` | `tools/interaction/` (Rust, legacy) | Not used |
| `wall-renderer/` | `tools/wall-renderer/` (Rust + WebView2) | Optional wallpaper renderer (if present) |
| `ultralight/` | Ultralight SDK | Live HTML wallpapers via CPU renderer |

## Portable Install

Just copy `UnifiedApp.exe` to any folder. On first launch it auto-creates:

```
<install dir>/
  UnifiedApp.exe
  assets/          ← auto-created
    data/          ← settings.json
    icon.ico       ← optional, falls back to generated icon
  Data/            ← auto-created
    Mods/          ← drop .cs mod widgets here
    Wallpapers/    ← drop wallpaper HTML files here
    Notes/         ← widget note storage
    HtmlWidgets/   ← per-widget HTML source files
  dependencies/    ← optional, enables extra features
    ultralight/    ← enables CPU HTML wallpapers
    wall-renderer/ ← enables WebView2 GPU wallpapers
```
