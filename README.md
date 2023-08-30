# ProJect-Moon
ProJect-Moon development team

The code naming convention for this project is as follows：

- Class                                                          Robot
- Interface                                                   IEat
- Private Attribute                                     m_robotSpeed
- Public Attribute                                       RobotSpeed
- Protected Attribute                                m_robotSpeed
- Enum Class                                              ROBOT_TYPE
- Local Parameter                                      robotSpeed



The code method parenthesis specification for this project is as follows：

- ```
  public void Motion()
  {
      foreach (var motionState in m_playerMoveStates)
      {
          motionState.Motion();
      }
  }
  ```



The existing architecture class diagram for this project is as follows：



MotionController

```mermaid
classDiagram
MotionStateMachine <|-- MainMotionStateMachine
MotionStateMachine <|-- AdditiveMotionStateMachine
MotionStateMachine *-- MotionState
MotionController o-- MotionStateMachine
PlayerController *-- MotionController
PlayerInformation *-- InputController
PlayerInformation *-- ComponentController
PlayerController *-- PlayerInformation
PlayerInformation *-- CharacterProperty
PlayerInformation *-- PlayerColliding
AdditiveDefultState *-- CoyoteTimer
AdditiveDefultState *-- JumpBufferTimer
MainMotionState <|-- MainDefultState
AdditiveMotionState <|-- AdditiveDefultState
MainMotionState <|-- WalkAndRunState
AdditiveMotionState <|-- JumpState
AdditiveMotionState <|-- PerpendicularGroundState
MotionState <|-- MainMotionState
MotionState <|-- AdditiveMotionState
```
