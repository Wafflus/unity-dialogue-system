# Unity Dialogue System

A Node-Based Dialogue System made for Unity using the Experimental GraphView API.

![Graph Example](https://imgur.com/LlLZkmc.png)

# Tutorial

This System is the final result of a [Tutorial Series](https://www.youtube.com/watch?v=nvELzBYMK1U&list=PL0yxB6cCkoWK38XT4stSztcLueJ_kTx5f) made by me
whose purpose is to teach you how to use Unity's GraphView API.

# Features

This System offers the user a few features:

### Creation of Single or Multiple Choice Nodes

Able to be created both through a contextual menu or a search window.

![Creation of the Nodes](https://imgur.com/g7Oo4pQ.gif)

### Grouping of Nodes

Nodes can be grouped to allow for a better organization of the Graph.

These are also useful to filter the dialogues by Groups in the Custom Inspector.

![Grouped Nodes](https://imgur.com/B21Znkk.gif)

### Feedback on Nodes or Groups with the same name in the same scope

Whenever a Node or a Group have elements of the same type with the same name in the same scope,
their colors will be updated to allow the user to know they need to update their names.

The Save button will also be disabled until the names are changed.

This happens because the Graph Elements will end up becoming asset files in the Project folder.

![Same Name in the Same Scope Error Feedback](https://imgur.com/6rEqcR0.gif)

### Minimap

A minimap that allows the user to have an overview of where the Nodes are placed
and to navigate to them by clicking in the respective shapes.

![Minimap](https://imgur.com/2rGVj3b.gif)

### Save & Load System

A Graph can be saved into a Scriptable Object that can be used to load it again later on.

The Graph elements will also be transformed into Scriptable Objects that can be used at Runtime.

Some, or most of those Scriptable Objects are used just for the Custom Inspector.

![Save & Load System](https://imgur.com/PUk2Jtq.gif)

### Clear & Reset Button

Allows for clearing the Graph (remove any element on it) and resetting the file name.

![Clearing and Resetting the Graph](https://imgur.com/ucHMBgQ.gif)

### Custom Inspector

Allows the ease of choosing a starting dialogue for a runtime dialogue system the user might want to develop.

The Inspector allows the filtering of dialogues by grouped and non-grouped dialogues, as well as by dialogues that can be considered "starting" dialogues.

![Custom Inspector](https://imgur.com/7gqUHpR.gif)

# License

[MIT License](LICENSE.md)

# Version

Current version: 1.0.3

# Contributing

This Dialogue System won't be updated further than this as its main purpose was to fit my game needs and the tutorial series, which should now be over.

If you want to support me, subscribing to my [Youtube Channel](https://www.youtube.com/c/IndieWafflus) or following me on [Twitter](https://twitter.com/IndieWafflus) would help me out a ton and be appreciated.

# Notes

The GraphView API is likely being deprecated towards the [GTF (GraphTools Foundation) Package](https://docs.unity3d.com/Packages/com.unity.graphtools.foundation@0.8/manual/index.html), but should still work in Unity 2019 and Unity 2020.

This was made using Unity 2020.
