<h1 align="center">
  <img src="docs/resources/images/logo/head.png?raw=true" alt="ProJect-Moon Logo" width="1000">
ProJect-Moon
</h1>

<div align="center">

## 状态一览

[![Help Docs](https://github.com/fictional-vision/ProJect-Moon/actions/workflows/help-docs.yml/badge.svg)](https://orange-cliff-0c5466300.3.azurestaticapps.net)
[![Unit Test](https://github.com/fictional-vision/ProJect-Moon/actions/workflows/unit-test.yml/badge.svg)](https://github.com/fictional-vision/ProJect-Moon/actions/workflows/unit-test.yml)
[![Static-Badge](https://img.shields.io/badge/contact-BiliBili-blue)](https://space.bilibili.com/165762441)
</div>

<video id="video" controls="" preload="none" poster="封面">
      <source id="mp4" src="docs/resources/videos/SlicerFollow.mp4" type="video/mp4">
</videos>

## TODO

- [x] Build the basic framework and design the coding style.
- [x] Multi-parameter player controller.
- [x] Polygoncollider2D needs to be generated from the mesh, and concave polygons need to be supported.
- [x] Accurate clipping, and the minimum number of game objects mounted.
- [ ] [Add move following and rotation following states to the SlicerController.](#Add-move-following-and-rotation-following-states-to-the-SlicerController.)
- [ ] The camera controller, based on Cinemachine cameras, requires enough pithy.
- [ ] The AI of game NPCS requires high scalability and state-based production.
- [ ] Optimal handling of physical collisions in which multiple objects physically form an assembly.
- [ ] It can detect complex physical collisions without using rigid body components.
- [ ] Portal, Total War Simulator like level editor, edit the level while the game is running.

## Project milestone

- The algorithm of generating polygoncollider2D mesh based on mesh triangle is completed, and the performance is good, and it supports concave polygon.

  - The core idea of the algorithm is to find all triangles according to mesh and make them two-dimensional, and remove redundant,       intersecting triangles and vertices that do not form a triangle. Finally, a greedy algorithm is used to generate polygoncollider2D. I am glad that Unity mesh models are already triangulated, which saves me a lot of trouble.

  <p align="right">——2023.9.11</p>

<h3 align="center">
  <img src="docs/resources/images/Textures/CreatPolygonCollider.png?raw=true" alt="CreatPolygonCollider" width="1000">
</h3>

- Very accurate slicer box, and the parameters are very concise, the code logic is good.

  - Based on Ezy-Slice. A slicer box is generated based on some mathematical and linear algebra calculations, and the object pool framework is used. So far, slicer box is working very well.

  <p align="right">——2023.9.16</p>

<h3 align="center">
  <img src="docs/resources/images/Textures/SlicerBox.png?raw=true" alt="SlicerBox" width="1000">
</h3>
