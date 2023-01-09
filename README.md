# Inworld-AI-Island

[Download demo app (Windows)](https://drive.google.com/file/d/1l8XSx9J9hzrjw5TqybGzu8CJA9j0t20D/view?usp=share_link)

The app contains two characters. To move in the world you need to use buttons on the keyboard: **1**, **2**, **3** and **Escape** button for quit.

### Feature: realtime weather in any city in US

![ezgif com-gif-maker](https://user-images.githubusercontent.com/9135028/211234530-03311412-ccc7-4c82-856b-0301b7095e8e.gif)

Say keyword 'weather' and any city in US to see current weather in it. Some cities such as "Los Angeles" could not be recognized as US city, that is why you may have 404/500 error for that city, just try another one, such as Seattle, New York, San Diego.
Only one of the characters is ready to interact with "Weather feature", you will see which one. Because there is a weather panel close to the character which shows current weather in Seattle, WA, USA.

### Inworld.AI SDK changes

Also I made some changes to the Inworld.AI SDK. There is summary of changes:

New events in InworldCharacter
- added onStartListening event
- added onEndListening event
- added onStartPlayerTalking event
- added onEndPlayerTalking event

New events in InworldPlayer
- added onTextSend event

Fixes
- fixed exception in InworldPlayer.cs (34 line)
- fixed exception in InworldPlayer.cs (90 line)

Changes
- added autofocus on InputText field on opening text panel
- added dark overlay to chat panel for better visibility on bright backgrounds
