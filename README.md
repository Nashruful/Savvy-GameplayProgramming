# 🎮 Mastermind Console Game (C#)

This is a C# console version of the classic **Mastermind** game, built for the **Savvy Kickstarter Program – Gameplay Programming Track (2025)**.

---

## ✅ How to Play

- The secret code consists of **4 unique digits** chosen from **0 to 8**.
- The player has **10 attempts** (or custom amount via command-line) to guess the code.
- After each guess, the game displays:
  - `Well placed pieces`: correct digits in the correct position.
  - `Misplaced pieces`: correct digits in the wrong position.
- The game ends when:
  - The player guesses the code (`Congratz! You did it!`)
  - The player runs out of attempts (`Game over!`)
  - You can also Force end the game by pressing CTRL+C 

---

## 🖥️ How to Run

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed (version 6.0 or later)

### Build and Run
1. Open a terminal and navigate to the project folder.
2. Run the game with:

```bash
dotnet run
