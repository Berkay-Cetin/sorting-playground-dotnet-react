# Sorting Playground — .NET + React
![Tech Stack](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![React](https://img.shields.io/badge/React-19-61DAFB?style=flat&logo=react)
![Vite](https://img.shields.io/badge/Vite-6-646CFF?style=flat&logo=vite)
![SignalR](https://img.shields.io/badge/SignalR-realtime-512BD4?style=flat&logo=dotnet)

A real-time sorting algorithm visualizer built with .NET and React.
Each step of the algorithm is streamed live to the UI via SignalR.

## Features

- ▶️ Real-time step-by-step visualization via **SignalR**
- ⌘ **Command palette** algorithm selector — `Ctrl+K` to open
- 🔢 Randomizable input list
- 📊 Step counter and live status
- ⚡ Add new algorithms with **zero configuration** — just drop a class in `Algorithms/`

## How It Works

The backend uses a custom `ObservableList` that automatically records every mutation
(`swap`, `set`, `remove`, `insert`) and streams each state change to the frontend via SignalR.
Sorting classes have no awareness of the visualization layer — they just sort.

## Adding a New Algorithm

Create a class in `SortingVisualizer.API/Algorithms/`:
```csharp
using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

public class BubbleSort
{
    public void Sort(ObservableList list)
    {
        for (int i = 0; i < list.Count - 1; i++)
            for (int j = 0; j < list.Count - i - 1; j++)
                if (list[j] > list[j + 1])
                    list.Swap(j, j + 1);
    }
}
```

It will appear in the UI automatically. No other changes needed.

## Tech Stack

### Backend
- **.NET 8** / C#
- **ASP.NET Core** — REST API
- **SignalR** — real-time step streaming
- **Reflection** — auto-discovery of algorithm classes

### Frontend
- **React 19** + **Vite 6**
- **SignalR JS Client** — `@microsoft/signalr`
- **CSS Variables** — custom dark theme
- **Google Fonts** — Space Mono & Syne

## Project Structure
```
SortingVisualizer/
├── SortingVisualizer.API/
│   ├── Algorithms/          # Drop new sorting classes here
│   ├── Controllers/         # SortingController
│   ├── Hubs/                # SignalR SortingHub
│   └── Services/            # ObservableList, AlgorithmDiscovery, StepRecorder
└── sorting-visualizer-ui/
    └── src/
        ├── components/      # SortVisualizer, CommandPalette, SortBar, StatsPanel
        └── hooks/           # useSignalR
```

## API Endpoints

| Method | Endpoint | Body / Params |
|--------|----------|---------------|
| GET | `/api/sorting/algorithms` | — |
| GET | `/api/sorting/generate` | — |
| POST | `/api/sorting/sort` | `{ algorithmName, connectionId, list? }` |

## Running
```bash
# Backend
cd SortingVisualizer.API
dotnet run

# Frontend
cd sorting-visualizer-ui
npm install
npm run dev
```