# Orbital Sandbox

**Winner: Best in Theme at AuburnHacks 2024**

**Devpost Page: https://devpost.com/software/orbital-sandbox**

**Authors: Eirik Mulder, Dakoda Willingham, Cameron Cohen**

## Inspiration

Interplanetary space exploration is becoming all-to prevalent in the modern day. 
We wanted to design a simulator that allows the general public and kids to explore and
gain understanding of some of the basic orbital mechanics and challenges facing space travel.

## About
The simulator allows you to fly a spacecraft around Jupiter and its moons, 
accurately simulating gravitational dynamics and controls. 
For example, any change to your angle is added as thrust, using fuel and battery, and giving you angular velocity, 
meaning you have to reverse your thrust when you are pointing in the right direction. 
The batteries charge based on whether the solar panels can see the sun (including effects from Jupiter's shadow), 
and you can both change your orientation and fire thrusters to change your trajectory 
(with real-time n-body orbital prediction generated through an RK4 numeric propagator).

## How We Built It
The app was built in the Unity game-engine, which allowed for rapid development and quick iteration of 3D designs. 
3D Models were produced in OnShape and Blender, and the textures for Jupiter were acquired from NASA. 
GitHub was used to manage commits and changes.
