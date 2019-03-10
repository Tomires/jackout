# Jackout

A short virtual reality puzzle game set in a cyberpunk world.

## Dependencies
| Dependency | Download link |
| --- | --- |
| Unity 2018.3.6 | https://unity3d.com/ |
| SteamVR Unity Plugin 1.2.3 | https://github.com/ValveSoftware/steamvr_unity_plugin/releases/download/1.2.3/SteamVR.Plugin.unitypackage |
| ProBuilder 2.9.8 | https://assetstore.unity.com/packages/tools/modeling/probuilder-2-x-111418 |

## How to build
1. Clone this repository.
2. Open folder as a Unity project.
3. Install SteamVR from provided Unity package.
4. Install ProBuilder 2.x from Asset Store using link provided.
5. From Unity's toolbar, select *TButt -> Core Settings* and then click *Save All Settings*.
6. Discard all changes that Unity made to files tracked by version control (mostly applicable to *ProjectSettings* files).
7. Using Unity's Project window, navigate to *Assets/Scenes* and open the *Jackout* scene.
8. *File -> Build Settings -> Build*

## Troubleshooting

### Teleportation does not work
Unity has a weird habit of removing tags and layers associated with the project. To fix this issue, just roll back any changes to *ProjectSettings/TagManager.asset*.
