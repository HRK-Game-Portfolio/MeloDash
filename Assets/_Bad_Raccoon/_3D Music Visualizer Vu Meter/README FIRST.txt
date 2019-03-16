----------
Contacts : 
----------

big.bad.raccoon@gmail.com

Please, a small review/and stars rating should be really appreciated :) 

-----------
UPDATE 1.2
-----------

A small issues fixed on the scene 7.


-----------
UPDATE 1.1 
-----------

Rewriten from scratch… We do some shit sometime :) I re-thinked a more extensible system.
More visualizer added, the new structure permit some very cool visualizer :) 

I hope you like them :)

Now visualizer size is lerped (smoothed) with channel volume information for a smoother
effect. A small tool is included : _Debug_Channel_Tool, this small helper is created to 
help you to see what is the better beat channel for a music. 

You select the channel (bottom of the window) and the circle in the center of the screen
change of size with this new channel beat.

This channel number can be used into the script : « SpectrumSize » in the case « Audio Channel »
This script is an exemple of object sizing with a channel beat.

Some functions using this tool are planned, for the moment don’t care about it :)

-----------
FOR NOOBS :
-----------

To change the music : Select the game object named « Spectrum manager » and drop another music
In the « audio source » component  


--------------------------------------------------
First to do (before importing package) : IMPORTANT
--------------------------------------------------

- Create a new project.
- Download this package on the asset store : http://u3d.as/KTp and import it.
- Import the raccoon package.
- Load the examples scenes.
- Add a component to each scenes camera : Add Component => Effects => Post-Processing Behavior
- And into the inspector of this new component drop the file : Post-Processing Profile
- Prepare to be blind! :)

----------------------
Why this procedure? :
----------------------
Because include the effects package, generate always problems when unity is updated...


-------------------------------------------
What is the file : Post-Processing Profile?
-------------------------------------------
This file is the file to use with the new unity post process stack.

												Bad Raccoon
												
												
												
												
												
												
