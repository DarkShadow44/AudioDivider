# AudioDivider
## About
AudioDivider is a program that allows program specific audio control.
It allows to split audio from different application into different audio output devices, even if the application itself doesn't allow to select the desired output device. For example, you can play your music on your speakers while having the sound of a game being played to your headset. Or maybe you want to reroute audio into a virtual audio device?

## License
AudioDivider is open-sourced under the [MIT license](http://choosealicense.com/licenses/mit/). Feel free to adapt or redistribute according to the license.

## Requirements
### Sources
* Windows 7, 32bit or 64Bit
* .NET Framework
* Visual Studio

### Pre-compiled Binaries
* Windows 7, 32bit or 64Bit
* .NET Framework 4
* Visual C++ 2010 Redistributable Package (x32)
* Visual C++ 2010 Redistributable Package (x64)

## Downloads

## How to use
First choose the version you need to run. If you are on a 64Bit OS choose "AudioDivider.exe", else choose "AudioDivider (32Bit).exe".

After starting AudioDivider, it lists all processes currently playing audio. You can either use the title of the window or the PID (Process ID) to find a process.

0. Select a program
0. Check the check-box 'Controlled'
0. Select a device you want to bind the process to
0. Click the button 'Set'
0. You can now close the program again

## Compatibility notes
AudioDivider is mainly designed for Windows 7 64Bit. It should work with the 32bit version too, but it isn't tested.
AudioDivider works with most programs: All major webbrowsers, mediaplayers, games or something else. However, because of the way it is implemented it might be mistaken for being malicious and blocked.

### Known incompatibilities
* VirtualBox newer than 4.3.12 (no way to fix)

## Common Problems
If it doesn't work, make sure you are running the right version, if you are on a 64Bit OS you need the 64Bit version for it to work.

If is still doesn't work, feel free to report the bug in the issue tracker. Make sure you include the log and the name of the program it doesn't work with.

## How does it work
AudioDivider works by injecting a DLL (Dynamic Link Library) into a target process. This allows the DLL to patch the program on the fly to bind it to a specific audio device.
The UI then acts as local "Server" to control the programs by a named pipe. This allows to program to retake the control over a process even after the UI was closed.
