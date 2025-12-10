# ⚔️ Console RPG Adventure

Willkommen zu **Console RPG Adventure**, einem rundenbasierten Rollenspiel für die Konsole, entwickelt in C# und .NET. Erstelle einen Charakter, bereise fantastische Orte, kämpfe gegen Monster und rette die Welt (oder sammle einfach nur Loot)!

## 📋 Inhaltsverzeichnis

- [Überblick](#überblick)
- [Features](#features-✨)
- [Charakterklassen](#charakterklassen-🛡️)
- [Spielmechaniken](#spielmechaniken-🎮)
  - [Kampfsystem](#kampfsystem-⚔️)
  - [Die Welt](#die-welt-🗺️)
  - [Handel & Inventar](#handel--inventar-💰)
- [Installation & Start](#installation--start-🚀)
- [Speichern & Laden](#speichern--laden-💾)

## Überblick

In diesem Spiel schlüpfst du in die Rolle eines Abenteurers, der sich durch eine gefährliche Welt kämpfen muss. Du startest in "Abenteuerland" und musst dich durch verschiedene zufällig generierte Orte bewegen, Monster besiegen, Gold sammeln und deinen Charakter verbessern.

## Features ✨

*   **Charaktererstellung:** Wähle deinen Namen und deine Klasse.
*   **RPG-Statistiken:** Ein tiefgehendes System mit Attribute wie Stärke, Intelligenz, Ausdauer, Glück und mehr.
*   **Rundenbasierte Kämpfe:** Taktische Kämpfe gegen eine Vielzahl von Monstern (Goblins, Drachen, Vampire uvm.).
*   **Magiesystem:** Nutze mächtige Zauber wie Feuerball oder Heilung (abhängig von der Klasse).
*   **Zufallsgenerierte Karten:** Entdecke bei jedem Spiel neue Orte.
*   **Loot & Ausrüstung:** Sammle Gold und kaufe Gegenstände im Shop.
*   **Speichersystem:** Dein Fortschritt kann jederzeit gespeichert und später wieder geladen werden (JSON-basiert).

## Charakterklassen 🛡️

Wähle zu Beginn eine von vier Klassen, die deinen Spielstil bestimmt:

| Klasse | Fokus | Besonderheit |
| :--- | :--- | :--- |
| **🗡️ Krieger** | Physischer Schaden | Hohe Stärke und Gesundheit. Der klassische Nahkämpfer. |
| **🔮 Magier** | Magischer Schaden | Hohe Intelligenz und Mana. Startet mit Zaubern wie *Feuerball* und *Blitz*. |
| **🗡️ Schurke** | Kritische Treffer | Hohe Beweglichkeit und kritische Trefferchance. Schnell und tödlich. |
| **🌿 Heiler** | Unterstützung | Hohe Weisheit. Kann sich selbst im Kampf effektiv heilen. |

## Spielmechaniken 🎮

### Kampfsystem ⚔️
Wenn du auf ein Monster triffst, beginnt der Kampf. Du hast folgende Optionen:
1.  **Angreifen:** Ein Standardangriff mit deiner Waffe.
2.  **Zauber wirken:** Nutze Mana, um Schaden zu verursachen oder dich zu heilen (nur für magische Klassen verfügbar).
3.  **Verteidigen:** Reduziere den eingehenden Schaden in der nächsten Runde.
4.  **Fliehen:** Versuche, dem Kampf zu entkommen (chancebasiert, abhängig von Beweglichkeit).

### Die Welt 🗺️
Die Welt besteht aus verschiedenen Orten wie Dörfern, Wäldern, Höhlen und Ruinen. Du kannst:
*   Die Karte ansehen, um zu sehen, wo du bist.
*   Zu zufälligen Orten reisen (Vorsicht: Monster lauern überall!).

### Handel & Inventar 💰
Besuche den **Shop**, um dein Gold auszugeben:
*   Kaufe Heiltränke, um dich im Kampf zu stärken.
*   Erwerbe Waffen wie Schwerter, Dolche oder Zauberstäbe.

Im **Inventar** kannst du deine gesammelten Gegenstände ansehen und Tränke benutzten.

## Installation & Start 🚀

Voraussetzungen: [.NET SDK](https://dotnet.microsoft.com/download) installiert.

1.  Klone das Repository oder lade die Dateien herunter.
2.  Öffne ein Terminal im Ordner `ConsoleRPGApp`.
3.  Starte das Spiel mit folgendem Befehl:

```bash
dotnet run
```

## Speichern & Laden 💾

Das Spiel bietet ein automatisches Speichersystem.
*   Wähle im Hauptmenü **"8. Spiel speichern"**, um deinen Fortschritt in einer JSON-Datei im Ordner `savegames` zu sichern.
*   Beim Neustart kannst du **"9. Spiel laden"** wählen oder direkt beim Starten des Programms einen alten Spielstand laden.

---
*Viel Spaß bei deinem Abenteuer!*
