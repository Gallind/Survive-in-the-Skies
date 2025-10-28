# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Stadnard Workflow

1. First think through the problem, read the codebase for relevant files, and write a plan to todo.md.
2. The plan should have a list of todo items that you can check off as you complete them
3. Before you begin working, check in with me and I will verify the plan.
4. Then, begin working on the todo items, marking them as complete as you go.
5. Please every step of the way just give me a high level explanation of what changes you made
6. Make every task and code change you do as simple as possible. We want to avoid making any massive or complex changes. Every change should impact as little code as possible. Everything is about simplicity.
7. Finally, add a review section to the todo.md file with a summary of the changes you made and any other relevant information
8. after the changes are made, add a brief explenation about the changes you made to a history file called editHistory.md. this file will store the history of all edits. 
9. also like the editHistory.md file, add the changes to editHistoryAI.md file. the difference is that this file will be read by an AI agent and not a human so you can write it in the most efficient way that an AI agent will understand it.
10. if any additional steps are required to be done in the unity editor, include an explenation about what to do.

## Project Overview

**Survive in the Skies** is a Unity-based 3D real-time strategy (RTS) game set on floating islands suspended in the sky. Players control a hero and units to gather food resources, defend against enemy waves, and meet survival quotas within a time limit.

### Game Concept
The game blends classic RTS mechanics with modern visuals in a fantasy skyborne setting. Players start with a base center and hero, must collect food from neighboring islands, and survive enemy attacks while meeting resource quotas to ensure their people's prosperity.

### Core Gameplay Loop
- **Resource Collection**: Gather food from floating islands and deliver to collection point
- **Time Pressure**: 5-minute countdown to meet food quota (5 food required)
- **Enemy Waves**: Defend against spawning enemy units attacking your base
- **Unit Management**: Select and control units using RTS-style commands
- **Strategic Spawning**: Spend food resources to spawn additional defender units (10 food per unit)
- **Win/Lose Conditions**: Meet quota before time expires (win) or fail to meet quota (lose)

### Visual Style
Floating islands with lush environments inspired by Warcraft 3 and League of Legends aesthetics, featuring stone-based buildings, grass platforms, bridges between islands, and stylized hero units in fantasy armor.

### Demo Scope (Current Implementation)
- Single handcrafted map with multiple connected floating islands
- Player-controlled hero with movement and combat
- Enemy wave spawning system
- Food resource gathering and quota tracking
- Unit spawning mechanics (knights)
- Basic building placement system (future expansion)
- Complete game loop with win/lose states

## Development Commands

This is a Unity C# project - all development happens within the Unity Editor:

- **Open Project**: Open Unity Hub → Open → Select project folder
- **Build**: File → Build Settings → Build (or Build and Run)
- **Test**: Unity Test Runner (Window → General → Test Runner)
- **Scripts**: Located in `Assets/Scripts/` - use Visual Studio or your preferred C# editor

## Architecture Overview

### Design Patterns
- **Singleton Pattern**: Used for global managers (UnitSelectionManager, RTSCameraController, FoodQuotaTimer, CursorManager, UnitSpawner)
- **State Machine Pattern**: Animator-driven states for unit combat AI and building placement system
- **Component-Based Architecture**: Separation of concerns across multiple components per game object
- **Event-Driven Design**: InputManager uses C# events for decoupled input handling
- **Scriptable Objects**: ObjectsDatabaseSO for centralized building/object data configuration

### Core Game Systems

#### 1. Unit Selection System
- **UnitSelectionManager.cs**: Singleton managing all unit selection logic
  - Maintains `allUnitsList` registry of all player units
  - Single-click selection, Shift+Click multi-select, drag selection support
  - Raycast-based interaction with layer filtering
  - Ground marker visual feedback
  - Attack target assignment

#### 2. Unit Movement & Combat
- **Unit.cs**: Base unit class with health, animation, NavMeshAgent integration
- **UnitMovement.cs**: Right-click movement commands with direction indicators
- **AttackController.cs**: Centralized combat system for both players and enemies
  - Attack range, damage, cooldown management
  - Trigger-based auto-target acquisition
  - Polymorphic damage application
- **State Machine**: Idle → Follow → Attack states via Animator StateMachineBehaviour
  - UnitIdleState.cs: Monitors for targets
  - UnitFollowState.cs: NavMesh pathfinding to target
  - UnitAttackState.cs: Executes attacks on cooldown

#### 3. Enemy AI System
- **Enemy.cs**: Enemy unit controller with health and AI behavior
  - Coroutine-based target finding (0.2s intervals)
  - Searches UnitSelectionManager.allUnitsList for closest player unit
  - NavMeshAgent pathfinding and AttackController integration
- **EnemySpawner.cs**: Wave-based enemy spawning
  - Fixed spawn location: (40, 0, 17) with 10.0f radius
  - Randomized wave timing (10-20s) and spawn intervals (2-5s)
  - NavMesh validation for spawn positions

#### 4. Resource Management System
- **FoodSpawner.cs**: Periodic food object spawning (5s intervals, max 10 objects)
- **FoodCreate.cs**: Food pickup trigger zones
- **FoodCollectionPoint.cs**: Base delivery point for food resources
- **FoodQuotaTimer.cs**: Game state manager (Singleton)
  - 5-minute countdown timer (300s)
  - Tracks current food vs quota (5 food required to win)
  - Win/lose condition checking
  - Scene transitions on game end

#### 5. Unit Spawning System
- **UnitSpawner.cs**: Player unit instantiation (Singleton)
  - Placement mode with NavMesh validation
  - Left-click to place, right-click to cancel
  - Integrates with food cost system
- **BuildUnitButton.cs**: UI button for unit spawning
  - Food cost: 10 per unit
  - Button interactability based on available resources

#### 6. Camera System
- **RTSCameraController.cs**: Multi-mode camera control
  - Keyboard movement (WASD/Arrow keys)
  - Edge scrolling with dynamic cursor arrows
  - Middle-mouse drag panning
  - Optional unit follow mode
  - Speed modifiers (normal: 0.01f, fast: 0.05f with Cmd key)

#### 7. Building/Placement System
- **PlacementSystem.cs**: State-based building controller
  - PlacementState: Object placement mode
  - RemovingState: Object removal mode
- **GridData.cs**: Dictionary-based spatial grid for placement validation
- **PreviewSystem.cs**: Semi-transparent preview with color feedback (green/red)
- **ObjectPlacer.cs**: Handles instantiation and lifecycle
- **Constructable.cs**: Interface for placed objects (activates NavMeshObstacle)
- **ObjectsDatabaseSO.cs**: ScriptableObject database for building data
  - Stores building properties, costs, benefits
  - BuildRequirement and BuildBenefits systems

#### 8. UI & Visual Feedback
- **CursorManager.cs**: Dynamic cursor system (Singleton)
  - Cursor types: Walkable, Selectable, Attackable, UnAvailable
  - 3D marker prefabs positioned via ground raycasts
- **HealthTracker.cs**: Smooth health bar animations
  - Coroutine-based interpolation (0.5s)
  - Color gradient: Green (>60%) → Yellow (>30%) → Red (<30%)
- **DirectionIndicator.cs**: Line renderer for movement path visualization
- **PauseMenu.cs**: Pause/resume with Time.timeScale control
- **MainMenu.cs**: Scene loading for game startup

### Technical Implementation Details

#### NavMesh Integration
- All moving entities use NavMeshAgent for pathfinding
- SamplePosition validation for spawn/placement operations
- Path visualization with DirectionIndicator line renderer

#### Raycast-Based Interaction
- Layer-masked raycasts for different interaction types:
  - Clickable layer: Unit selection
  - Ground layer: Movement commands
  - Attackable layer: Combat targets
  - Placement layer: Building placement

#### Performance Optimizations
- Enemy target finding: Coroutine every 0.2s (not per frame)
- Object pooling approach for food objects (static list with max cap)
- Grid-based collision detection: O(1) lookups via Dictionary
- Health bar updates: Coroutine-based smooth transitions

#### Animation & Visual Polish
- Animator integration for unit states (isMove boolean)
- Smooth camera movement via Lerp
- Unit rotation towards targets via Slerp
- Billboard effect for floating UI elements (FaceCamera.cs)
- Material-based visual feedback for health and placement validity

## File Structure

```
Assets/Scripts/
├── AttackController.cs          # Centralized combat system for units and enemies
├── BuildUnitButton.cs           # UI button for unit spawning with cost system
├── CursorManager.cs             # Dynamic cursor feedback system
├── DirectionIndicator.cs        # Line renderer for movement visualization
├── Enemy.cs                     # Enemy AI controller with target finding
├── EnemySpawner.cs              # Wave-based enemy spawning system
├── FaceCamera.cs                # Billboard effect for UI elements
├── FoodCollectionPoint.cs       # Base delivery point for resources
├── FoodCreate.cs                # Food pickup trigger zones
├── FoodQuotaTimer.cs            # Game state manager and win/lose logic
├── FoodSpawner.cs               # Periodic food object spawning
├── HealthTracker.cs             # Smooth health bar UI component
├── MainMenu.cs                  # Main menu scene loader
├── PauseMenu.cs                 # Pause menu with time control
├── RTSCameraController.cs       # Multi-mode RTS camera system
├── SimplePatrol.cs              # Basic patrol behavior (unused)
├── Unit.cs                      # Base unit class with health and animation
├── UnitAttackState.cs           # Attack state for unit state machine
├── UnitFollowState.cs           # Chase/follow state for unit AI
├── UnitIdleState.cs             # Idle state monitoring for targets
├── UnitMovement.cs              # Right-click movement command handler
├── UnitSelectionBox.cs          # Drag selection rectangle UI
├── UnitSelectionManager.cs      # Singleton managing all unit selection
├── UnitSpawner.cs               # Player unit placement system
└── Episode11_Assets_RTS/Scripts/  # Building placement system
    ├── PlacementSystem.cs       # Building state controller
    ├── GridData.cs              # Spatial grid for placement validation
    ├── PreviewSystem.cs         # Visual preview for placement
    ├── ObjectPlacer.cs          # Instantiation handler
    ├── Constructable.cs         # Placed object interface
    ├── ObjectsDatabaseSO.cs     # Building data ScriptableObject
    ├── InputManager.cs          # Input events for building system
    ├── IBuildingState.cs        # State interface
    ├── PlacementState.cs        # Placement mode state
    └── RemovingState.cs         # Removal mode state
```

## Key Configuration Requirements

### Unity Setup
- **Layers**: clickable, ground, attackable, (placement for buildings)
- **Scenes**: "Game Scene", "GameOver", "Start Menu"
- **Tags**: "Enemy", "Hero"
- **Animator**: State machine with Idle/Follow/Attack states for units
- **NavMesh**: Must be baked on scene terrain for pathfinding
- **Camera**: Main camera required for raycasting
- **TextMeshPro**: UI text requires TMP_Text components

### Dependencies
- Unity 3D rendering pipeline
- NavMesh AI system (com.unity.ai.navigation)
- TextMeshPro for UI
- Netcode for GameObjects (optional, for future multiplayer)

### Game Settings (Configurable)
- Turn time / Timer duration: 300s (5 minutes)
- Food quota to win: 5
- Food per collection: 25
- Unit spawn cost: 10 food
- Enemy spawn location: (40, 0, 17)
- Food spawn radius: 10.0f
- Max food objects: 10
- Attack range: 2.0f (default)
- Attack damage: 10 (default)
- Attack cooldown: 1.5s (default)

## Common Development Tasks

### Adding New Unit Types
1. Create prefab with Unit, UnitMovement, AttackController components
2. Add NavMeshAgent and Animator with state machine
3. Configure unit stats (HP, attack damage, range) in Inspector
4. Add to UnitSelectionManager's unit registry

### Modifying Game Rules
1. Edit FoodQuotaTimer settings: timer duration, food quota
2. Adjust BuildUnitButton food cost
3. Modify AttackController values: range, damage, cooldown
4. Configure EnemySpawner: spawn intervals, wave timing

### Adding New Buildings
1. Create building data in ObjectsDatabaseSO ScriptableObject
2. Define size (Vector2Int), prefab, requirements, benefits
3. Add UI button to trigger PlacementSystem.StartPlacement(ID)
4. Configure NavMeshObstacle on building prefab

### Adjusting Enemy Behavior
1. Modify Enemy.cs target search interval (default: 0.2s)
2. Adjust EnemySpawner spawn location and radius
3. Configure wave timing randomization ranges
4. Modify AttackController settings for enemy stats

## Important Notes

- **No external build system**: Unity Editor handles all building and compilation
- **Scene-based setup**: Game objects and components configured through Unity Inspector
- **Inspector references**: Most component connections done via drag-and-drop in Inspector
- **NavMesh requirement**: Scene must have baked NavMesh for pathfinding to work
- **Layer configuration**: Proper layer setup is critical for raycast-based interactions
- **Performance considerations**: Enemy AI uses coroutines to avoid per-frame updates
- **Extensible design**: Building system uses interfaces and ScriptableObjects for easy expansion

