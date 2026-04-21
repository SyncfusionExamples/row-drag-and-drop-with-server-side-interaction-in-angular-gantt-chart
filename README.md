# Row Drag and Drop with Server-Side Interaction in Angular Gantt Chart

This sample application demonstrates row drag and drop in the Syncfusion Angular Gantt Chart with server-side processing using ASP.NET Core. Task hierarchy changes triggered by drag-and-drop actions are sent to the server, where updates are processed and returned to the client.

## Overview

This repository provides a working example of an Angular SPA integrated with ASP.NET Core backend for Gantt chart functionality. It demonstrates server-side data binding and drag-and-drop updates.

## Features

- Syncfusion Angular Gantt Chart with hierarchical task support
- Server-side data binding using UrlAdaptor
- Row drag and drop with server-side update handling
- Toolbar-enabled CRUD operations
- ASP.NET Core backend integrated with Angular frontend

## Prerequisites

Ensure the following tools are installed:

- Visual Studio 2022 or later
- .NET SDK (8.0 or later)
- Node.js (LTS or later) with npm

## Installation and Running the Sample

1. Open the solution file: `drag-and-drop/URLAdaptor.sln`

2. Install Angular dependencies by running `cd drag-and-drop/ClientApp && npm install`

3. Build and run the solution using Visual Studio or run `dotnet run`

4. Launch the application in the browser using the configured HTTPS URL.
