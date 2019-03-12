# Simple_CastleSales_App

## Description
A simple castle sales application to add, view, and search castles submitted by users. The data in this app is not persistent.

## Design
Generally when I initially design a project I try to create all of the moving parts and components that I need, so I can implement everything easier. Although this often fails me since unforseen issues arise literally all the time in this line of work. Instead I wanted to design the project as more of a general outline first. Based on that outline I started implementing sub systems before starting work on other aspects of the design. For example I worked and implemented the Account Creation system before working on the Castle Addition system. This way I made sure that what I was doing would work fully before moving onto the next system in the design. This made it quite easy to test as I worked.

When first designing the app I had a couple thoughts in mind. The first being, am I going to make a web front end. With this in mind at the time when I was first starting the design process I needed a pattern that could handle multiple Views. Since I have worked with the MVP pattern before in college I decided to utilize this pattern to abstract the backend from whatever view I used. After creating the console application I decided not to implement the web front end, although it would be easier since I used the MVP pattern.

Another thought I had when designing the backend was that I initially wanted to have the Model hold a list of Castle objects and from there have each Castle hold a singular list of IStringable objects. These IStringable objects would represent a castles components (i.e. rooms, features, and historical events). This way I would only ever have to operate on a singular list of objects. This became an issue when sending data back to the view and then later searching for specific characteristics within a castle. The only way I could see accomplishing this idea of using a singular list would have been to do a lot of type checking and casting which seemed quite dirty. Instead I made the castle hold a list for each of it's components so I knew what was being sent back to the view as well as the view also knew what it was receiving.

## What would I do Differently
There is one major aspect of this code sample that I would have loved to do differently granted I had some more time to implement it. The aspect is simply moving data from the model back to the view for display. I mentioned earlier that this was initially an issue, but in hindsight the whole transferring of data could have been much easier. Instead of transferring Dictionary objects back to the view I wish I would have used JSON Serialization and Deserialization instead. That way regardless of what types of objects where in a list I could serialize all the objects within the list and send them to the view and then the view would know how to pick those objects apart and display them.

## Time Spent

