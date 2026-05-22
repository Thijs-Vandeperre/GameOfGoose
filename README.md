# Game of Goose

A console-based implementation of the classic **Game of Goose** board game, built with **C# / .NET 10** as a SOLID + TDD exercise.

---

## About

This project implements the Game of Goose for 4 players. Each turn, players roll two dice and move their piece along a 64-space board. Special spaces trigger unique effects. The first player to land exactly on space 63 wins.

---

## Tech Stack

- **Runtime:** .NET 10
- **Language:** C#
- **Testing:** xUnit + coverlet
- **Architecture:** SOLID principles, TDD

---

## Project Structure

```
GameOfGoose/
├── GameOfGoose.Core/        # Main application
│   ├── Dice/                # IDie, IDiceRoll, Die, FakeDie, TwoDiceRoll
│   ├── Engine/              # Game loop, GameContext, GameStatus
│   │   └── Rules/           # IGameRule implementations (FirstTurn, Bounce, SkipTurn, SpaceAction)
│   ├── Factories/           # BoardFactory, PlayerFactory, PieceFactory
│   ├── Spaces/              # ISpace and all special space types
│   ├── UI/                  # ILogger, IInputReader, IGameFormatter + console implementations
│   ├── Board.cs
│   ├── Piece.cs
│   ├── Player.cs
│   └── Program.cs
└── GameOfGoose.Tests/       # xUnit test project mirroring the Core structure
```

---

## How to Run

```bash
cd GameOfGoose.Core
dotnet run
```

## How to Run Tests

```bash
cd GameOfGoose.Tests
dotnet test
```

---

## Board Layout

The board has 64 spaces (0–63). Special spaces:

| Space | Type    | Effect                                                                 |
|-------|---------|------------------------------------------------------------------------|
| 6     | Bridge  | Jump to space 12                                                       |
| 19    | Inn     | Skip 1 turn                                                            |
| 31    | Well    | Stuck until another player lands on the same space and frees you       |
| 42    | Maze    | Sent back to space 39                                                  |
| 52    | Prison  | Skip 3 turns                                                           |
| 58    | Death   | Reset to space 0                                                       |
| 63    | End     | Win — but only if you land exactly; otherwise the BounceRule applies   |
| 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 | Goose | Move forward again by the same roll |

---

## Architecture & SOLID Highlights

**Single Responsibility** — Each class has one job: `Game` orchestrates turns, spaces handle their own effects, rules handle their own logic, formatters handle output.

**Open/Closed** — New spaces and rules can be added without touching existing code. `ISpace` and `IGameRule` are the extension points.

**Liskov Substitution** — All `ISpace` implementations are interchangeable in the board's space dictionary. `FakeDie` substitutes `IDie` transparently in tests.

**Interface Segregation** — `ILogger`, `IInputReader`, and `IGameFormatter` are kept separate so they can be swapped or mocked independently.

**Dependency Inversion** — `Game` depends on abstractions (`IDiceRoll`, `ILogger`, `IGameFormatter`, `IInputReader`, `IEnumerable<IGameRule>`), never on concrete implementations.

---

## Rules Pipeline

Rules are applied in order each turn:

1. **SkipTurnRule** — checks if the player must sit out this turn
2. **FirstTurnRule** — applies the special 6+3 / 5+4 first-roll rules
3. **BounceRule** — bounces the piece back if it overshoots space 63
4. **SpaceActionRule** — delegates to the `ISpace` the piece landed on