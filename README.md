# Row Drag and Drop with Server-Side Interaction in Angular Gantt Chart

Demo app showing Syncfusion Angular Gantt drag/drop with an ASP.NET Core backend handling task updates.

## Overview

This sample shows an Angular SPA loading Gantt task data from the server via Syncfusion `UrlAdaptor`. Row drag/drop events are sent to backend endpoints for updating task hierarchy and CRUD state.

## Features

- Syncfusion EJ2 Angular Gantt Chart
- Server-side data retrieval via `UrlAdaptor`
- Row drag/drop posted to backend
- CRUD support in the Gantt toolbar
- ASP.NET Core and Angular SPA integration

## Prerequisites

- Visual Studio 2022 or later
- .NET 7.0 SDK
- Node.js and npm

## Installation

1. Open `drag-and-drop/URLAdaptor.sln`.
2. In `drag-and-drop/ClientApp`, run `npm install`.
3. Build and run the solution.

## Usage

The app loads data from `/Home/UrlDatasource`. Row drag/drop sends a POST request to `/Home/DragandDrop`.

## Notes

- Uses `@syncfusion/ej2-angular-gantt`, `@syncfusion/ej2-angular-treegrid`, and `@syncfusion/ej2-angular-grids`.
- Drag/drop logic is implemented in `Controllers/HomeController.cs`.
- The sample is configured for HTTPS development proxy.

## Files of interest

- `drag-and-drop/ClientApp/src/app/app.component.ts`
- `drag-and-drop/Controllers/HomeController.cs`
- `drag-and-drop/URLAdaptor.csproj`
- `drag-and-drop/ClientApp/package.json`
