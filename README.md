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
For the user-interface to work as intended, two users must be accessing the same network when using two different devices. Starting from the main menu, select which role to play by selecting either the follower or leader button.
When the leader button is pressed, you will be taken to the leader scene. To start a connection, come up with your own password and click on the join button. Note that the other user, the follower, must know the password to establish a connection with the leader. Once the join button is pressed, a wait screen will be active with instructions and a start button. The start button can be pressed once the follower establishes a connection. When the follower establishes a connection, the chat box will inform that someone has connected. Once the start button is pressed, the timer on the top left will start counting down and there will be two images that are displayed for the follower. The leader can cycle through the images of these schematics by clicking the next and previous buttons. The leader can relay instructions through chat and voice connections using the software handled by WebRTC.
When the follower button is pressed, you will be taken to the follower scene. The follower can join a connection with the leader by entering the same password the leader has come up with and click join. The follower will see a wait screen with a demo button. This button, and the event that starts the exercise, is to be replaced by the event that occurs when the leader clicks on the start button. When the demo button is pressed, the timer will start and the canvas for the Arduino will be revealed. The follower can click on the validate button to continue to the next canvas. The drag-and-drop code canvas will be revealed. Once the follower completes both exercises and clicks on the validation button for the final time. The test will be complete, and the follower will be brought to the final scene informing them that the exercise is completed.

### Arduino
To interact with the Arduino, users can click the wire tool to enable a canvas so wires can be drawn. Once the button is clicked, left clicking draws a dot while right clicking removes a dot. When two dots are drawn, they become connected by a wire. If one of the dots is deleted, the wire also gets removed. The dots can also be moved to change the angle or direction of the wire. The wires can connect to individual pins in the Arduino and positive and negative leads of the LEDs or other objects. Once the user thinks they are done, they can continue to the coding part of the Arduino by clicking the code button. If everything is correct, the LEDs should blink at two different frequencies. 
### Drag-and-Drop code
To interact with the drag-and-drop code you will first need to click on the dropdown menu that says, “Select Code”. This is where all the code blocks are stored. From here you will be able to click on the code blocks and drag it where it belongs. There are two different dropdowns and each one will only be draggable to its grid block. After you drag the block, it will no longer be in the dropdown menu until you close and reopen it to refresh but you can still drag other blocks before you do this. Once all the code you want is placed on the screen the user will then press the “Validate” button to check that the code is correct. The final interaction of this button is still a work in progress but for now it prints out to the debug console if each line is valid or invalid.  

## Future development  
Finishing the validation of the Arduino wiring.  
Integration of the Arduino assembly and the user interface, for the follower, is still needed. 
The Leader start button event should be used to start the exercise for the follower instead of the demo button. 
Connecting the Drag-and-Drop code validation button to the UI asking the user what to do if there is an error.  
Cleaning up the look of the UI.  
