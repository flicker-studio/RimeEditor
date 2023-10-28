# The custom structs for this project are as follows

Structs

- Custom data structures to make the code clearer.
  - TrianglePoints:  The triangle struct, which contains three Vector2 vertices, simulates a triangle and has a static extension method for it.
  - InputProperty:  A GENERIC structure THAT stores the input, used to get and set the input, and can get the start input instant, the cancel input instant, and the input status.

```mermaid
graph TB
A(Structs)
B(TrianglePoints)
C(InputProperty)
A --> B
A --> C
```

------

**The existing architecture class diagram for this project is as follows：**

## StaticExtensions

- These static classes provide methods that other classes extend statically.
  - Now service classes are: LayerMask, MotionState,Raycast, Rigidbody, Vector2 / Vector3,TrianglePoints,Mesh,Transform,CharacterProperty.

```mermaid
graph TB
A(StaticExtensions)
B(LayerMaskMethod)
C(MotionStateMethod)
D(RaycastMethod)
E(RigidbodyMethod)
F(VectorMethod)
G(TrianglePointsMethod)
H(MeshMethod)
I(TransformMethod)
J(CharacterPropertyMethod)
A --> B
A --> C
A --> D
A --> E
A --> F
A --> G
A --> H
A --> I
A --> J
```

## Singleton

- A class that inherits from a singleton and acts as a manager for some underlying in-game functionality.
  - EventManager is the event manager responsible for managing all global events in the game.
  - InputManager is the input manager and is responsible for detecting all inputs from the player.
  - ObjectPool is the global object pool for the game. It primarily manages the pool of instantiated objects from prefabricated ones.

```mermaid
classDiagram
Singleton <|-- EventManager
Singleton <|-- InputManager
Singleton <|-- ObjectPool
```

## MotionController

- A state controller designed with state mode and factory mode is used.
  - The state machine is MotionStateMachine
  - The state is MotionState
  - The factory is MotionStateFactory
  - The data is BaseInformation

```mermaid
classDiagram
MotionController o-- MotionStateMachine
MotionController *-- BaseInformation
MotionController *-- MotionCallBack
MotionStateMachine <|-- AddtiveMotionStateMachine
MotionStateMachine <|-- MainMotionStateMachine
MotionStateMachine *-- MotionState
MotionStateMachine *-- MotionStateFactory
MotionState <|-- AdditiveMotionState
MotionState <|-- MainMotionState
MotionStateFactory <|-- AddtiveMotionStateFactory
MotionStateFactory <|-- MainMotionStateFactory
```

## PlayerController

```mermaid
classDiagram
MotionStateMachine <|-- MainMotionStateMachine
MotionStateMachine <|-- AdditiveMotionStateMachine
MotionStateMachine *-- MotionState
MotionStateMachine *-- MotionStateFactory
MotionStateFactory ..> MotionState
MotionStateFactory ..> MotionStateEnum
MotionStateFactory <|-- MainMotionStateFactory
MotionStateFactory <|-- AdditiveMotionStateFactory
MotionController o-- MotionStateMachine
PlayerController *-- MotionController
PlayerInformation *-- MotionInputController
PlayerInformation *-- ComponentController
PlayerController *-- PlayerInformation
PlayerInformation *-- CharacterProperty
PlayerInformation *-- PlayerColliding
PlayerInformation *-- PlayerRaycasting
AdditiveDefultState *-- CoyoteTimer
AdditiveDefultState *-- JumpBufferTimer
PlayerMainMotionState <|-- MainDefultState
PlayerMainMotionState <|-- SlideState
PlayerAdditiveMotionState <|-- AdditiveDefultState
PlayerMainMotionState <|-- WalkAndRunState
PlayerAdditiveMotionState <|-- JumpState
PlayerAdditiveMotionState <|-- PerpendicularGroundState
PlayerMotionState <|-- PlayerMainMotionState
PlayerMotionState <|-- PlayerAdditiveMotionState
MotionState <|-- PlayerMotionState
```

## SlicerController

```mermaid
classDiagram
MotionController o-- MotionStateMachine
SlicerController *-- SlicerInformation
SlicerController *-- MotionController
MotionController *-- BaseInformation
MotionController *-- MotionCallBack
MotionStateMachine <|-- AddtiveMotionStateMachine
MotionStateMachine <|-- MainMotionStateMachine
MotionStateMachine *-- MotionState
MotionStateMachine *-- MotionStateFactory
MotionState <|-- MainMotionState
MotionState <|-- AdditiveMotionState
MainMotionState <|-- SlicerMainMotionState
SlicerMainMotionState <|-- SlicerTranslationFollowState
SlicerMainMotionState <|-- SlicerRotationFollowState
AdditiveMotionState <|-- SlicerAdditiveMotionState
SlicerAdditiveMotionState <|-- SlicerCloseState
SlicerAdditiveMotionState <|-- SlicerOpenState
SlicerAdditiveMotionState <|-- SlicerCopyState
SlicerAdditiveMotionState <|-- SlicerReleaseState
MotionStateFactory <|-- AddtiveMotionStateFactory
MotionStateFactory <|-- MainMotionStateFactory

```

## LevelEditorCameraController

```mermaid
classDiagram
MotionController o-- MotionStateMachine
LevelEditorCameraController *-- MotionController
LevelEditorCameraController *-- LevelEditorCameraInformation
MotionController *-- BaseInformation
MotionController *-- MotionCallBack
MotionStateMachine <|-- AddtiveMotionStateMachine
MotionStateMachine <|-- MainMotionStateMachine
MotionStateMachine *-- MotionState
MotionStateMachine *-- MotionStateFactory
MotionState <|-- MainMotionState
MotionState <|-- AdditiveMotionState
MainMotionState <|-- LevelEditorCameraMainState
AdditiveMotionState <|-- LevelEditorCameraAdditiveState
MotionStateFactory <|-- AdditiveMotionStateFactory
MotionStateFactory <|-- MainMotionStateFactory
LevelEditorCameraAdditiveState <|-- LevelEditorCameraAdditiveDefultState
LevelEditorCameraAdditiveState <|-- LevelEditorCameraMoveState
LevelEditorCameraAdditiveState <|-- LevelEditorCameraChangeFovState
LevelEditorCameraAdditiveState <|-- MouseSelecteState
LevelEditorCameraAdditiveState <|-- MouseDragPositionState
LevelEditorCameraAdditiveState <|-- PositionAxisDragState
MouseSelecteState *-- OutlinePrinter
```

## LevelEditor

```mermaid
classDiagram
LevelEditorCommand <|-- ItemPositionCommand
LevelEditorCommand <|-- ItemRotationCommand
LevelEditorCommand <|-- ItemScaleCommand
LevelEditorCommand <|-- ItemRectCommand
LevelEditorCommand <|-- ItemSelecteCommand
LevelEditorCommand <|-- LevelEditorActionChangeCommand
LevelEditorCommandManager *-- LevelEditorCommand
LevelEditor *-- LevelEditorCommandManager
LevelEditor *-- LevelEditorConroller
LevelEditorConroller *-- LevelEditorInformation
LevelEditorInformation *-- LevelEditorInputController
LevelEditorInformation *-- LevelEditorUIManager
LevelEditorUIManager *-- ActionPanel
LevelEditorUIManager *-- ControlHandlePanel
LevelEditorUIManager *-- ItemTransformPanel
ItemTransformPanel *-- InputFieldVector3
LevelData *-- ItemData
```
