# Unity-Runtime-Animation-Recorder
  
  This project can make you recording animations in runtime with Unity, and can save into .anim or Maya .ma format.  
  Though Maya has its own physic simulator, but unity is much faster and can easily control detail movement through scripts.  
    
  This idea first came from Taiwanese artist [Hsin-Chien Huang](http://www.storynest.com/)  
  These script were used to help the process of a concert video, simulating some ragdoll falling animation.
    
    
## Installation
  
  Copy just copy Unity Runtime Recorder folder into your Asset folder and it's ready to go.  
  If you want to see same sample you can also copy DemoAssets folder.
  
  
## How To Use

  Here is a short video demo.  
  [https://youtu.be/RAjU5KodE1w](https://youtu.be/RAjU5KodE1w)

#### Unity Anim Saver
※ this function needs UnityEditor to work, so can only work in the Editor.

  1. Drag the UnityAnimationRecorder.cs script on any GameObject, and it will record all transforms in children.  
  2. Press "Set Save Path" button in the inspector, choose pick a folder and enter file name.
  3. Play the scene, and start/end recording by press the key you set in the inspector.
  4. When End Recording pressed, the .anim file will be generated.
  
#### Maya Exporter

  Pretty much the same as Unity Anim Saver.  
  Additionally, you have to select an .ma file which contains all model information.  
  
  My script doesn't generate model informations for maya, it only record animation data and append them at the end of .ma file.
  If you want to export the meshes you make in Unity, you can use [Export2Maya](https://www.assetstore.unity3d.com/en/#!/content/17079) which I also use in the concert project.
  
## Dealing with Lag

  If you want to simulate with a big amount of objects, you might ecountered lag.  
  You just need to adjust the Time.timeScale value in the Project Setting (or by using ChangeTimeScale option in my script).  
    
  All physics in Unity will affect by timeScale setting.
  And if you want to modify the object animation through your own script, please use FixedUpdate instead of Update.  
  
