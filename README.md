# ProJect-Moon
ProJect-Moon development team

MotionController

```mermaid
classDiagram
MotionStateMachine <|-- MainMotionStateMachine
MotionStateMachine <|-- AdditiveMotionStateMachine
MotionStateMachine o-- MotionState
MotionController o-- MotionStateMachine
MotionState <|-- DefultState
```
