# Essentials

[![EssentialsCommunity](https://img.shields.io/discord/1188494824001773620?style=for-the-badge)](https://discord.com/invite/eNJfZyhqj8)
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/SCP-SLEssentials-Team/SCPSL-Essentials/total?style=for-the-badge)
<a href="https://github.com/SCP-SLEssentials-Team/SCPSL-Essentials/releases">
<img src="https://img.shields.io/github/v/release/SCP-SLEssentials-Team/SCPSL-Essentials?display_name=release&style=for-the-badge" alt="Releases">
</a>

**SCPSL-Essentials** is a plugin for [SCP: Secret Laboratory](https://scpslgame.com/) servers using LabAPI. It provides essential admin tools, utility commands, and enhancements for server management and player moderation.

---

## 🚀 Features

- **Core to other Commands**
  - `ET`: Core to other commands ( will be deleted soon )

- **Performance Monitoring**
  - `TPS`: Displays the current server ticks per second.

- **Player Visibility**
  - `Invis`: Makes a player invisible.
  - `Visible`: Makes a player visible again.

- **Movement Control**
  - `Freeze`: Freezes a player in place.
  - `UnFreeze`: Unfreezes the player.

- **Size Modification**
  - `Size`: Changes the player’s scale.
  - `FakeSize`: Changes the player’s scale and make they not real.

- **Cheat Check**
  - `CheatCheck`: Marks a player to cheat check.
  - `CheatCheckPassed`: Marks a player as having passed the check.

- **Logging & Moderation**
  - `Log`: Writes to logs on your locale admin to Discord.
  - `Warn`: Issues a warning to a player.

- **Broadcasting**
  - `Broadcast`: Sends a broadcast message to all players.
  - `ShowHint`: Sends a hint message to a specific player.

- **Miscellaneous**
  - `GodmodeTutorial`: Enables godmode in the tutorial role.
  - `AutoFFToggle`: Automatically toggles friendly fire.
  - `AutoBanLog`: Logs automatic bans.
  - `BCReport`: Generates reports via broadcast messages.
  - `SaveOverwatch`: Saves the Overwatch mode state.

---

## 🔧 Requirements

- **LabAPI**: This plugin requires LabAPI.
- **Newtonsoft**: In dependencies.
- **Harmony0**: In dependencies.

---

## 📦 Installation

1. Download the latest release from the [Releases page](https://github.com/SCP-SL-Nexus/SCPSL-Essentials/releases).
2. Place the `Essentials.dll` file into your server's `plugins` folder.
3. Place the all files in `dependencies` into your server's `dependencies` folder.
4. Restart the server to apply changes.

---

## 🔐 Permissions

| Command              | Required Permission |
|----------------------|---------------------|
| `TPS`                | None                |
| `Invis`, `Visible`   | `Effect`            |
| `Freeze`, `UnFreeze` | `Effect`            |
| `Size`               | `PLM`               |
| `CheatCheck`, `CheatCheckPassed`, `Log`, `Warn` | `Overwatch` |

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 📫 Feedback

If you encounter issues or have suggestions, feel free to open an [issue](https://github.com/SCP-SL-Nexus/SCPSL-Essentials/issues) or submit a pull request.

---

> *This README is based on available source code and public information. For the latest updates and details, refer to the official repository.*
