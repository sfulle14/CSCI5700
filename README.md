# VR Mechatronics

## Introduction of Project
The purpose of this project was to work on developing a VR game that can be used by a research group at Vanderbilt University to measure the interaction of Autistic and Neurotypical people. 
This was a Leader-Follower game so there would be a leader who can see a scematic of the Arduino and pseudo code for the Arduino code that they would use to try and explain how the follower should wire the Arduino and code the Arduino.
The Follower would then see an Arduino interface that they could wire up to 2 LEDs. These LEDs would then blink at different frequencies if they were wired correctly.
There was then the Drag-and-Drop Arduino code section that the follower would interact with to try and drag the code in the proper place based on what the leader was telling them. 

## Dependencies
Unity version 2022.3.9f1 or later  
C# for modifying the codebase  

## User Guide  
### User Interface

### Arduino

### Drag-and-Drop code
To interact with the drag-and-drop code you will first need to click on the dropdown menu that says, “Select Code”. This is where all the code blocks are stored. From here you will be able to click on the code blocks and drag it where it belongs. There are two different dropdowns and each one will only be draggable to its grid block. After you drag the block, it will no longer be in the dropdown menu until you close and reopen it to refresh but you can still drag other blocks before you do this. Once all the code you want is placed on the screen the user will then press the “Validate” button to check that the code is correct. The final interaction of this button is still a work in progress but for now it prints out to the debug console if each line is valid or invalid.  

## Future development  
Finishing the validation of the Arduino wiring.  
Connecting the Drag-and-Drop code validation button to the UI asking the user what to do if there is an error.  
Cleaning up the look of the UI.  
