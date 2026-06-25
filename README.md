# Row Drag and Drop with Server-Side Interaction in Angular Gantt Chart

This sample application demonstrates row drag and drop in the [Angular Gantt Chart](https://www.syncfusion.com/angular-components/angular-gantt-chart) with server-side processing using ASP.NET Core. Task hierarchy changes triggered by drag-and-drop actions are sent to the server, where updates are processed and returned to the client.

## Overview

This repository provides a working example of an Angular SPA integrated with ASP.NET Core backend for Gantt chart functionality. It demonstrates server-side data binding and drag-and-drop updates.

## Features

- Angular Gantt Chart with hierarchical task support
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

## Related Links

- [Explore Angular Gantt Chart](https://www.syncfusion.com/angular-components/angular-gantt-chart)
- [Gantt Row Drag and Drop](https://ej2.syncfusion.com/angular/documentation/gantt/rows/drag-and-drop)
- [Angular Gantt Chart Getting Started Guide](https://ej2.syncfusion.com/angular/documentation/gantt/getting-started)
- [Gantt Chart Feature Overview](https://ej2.syncfusion.com/angular/documentation/gantt/overview)
- [Gantt API Documentation](https://ej2.syncfusion.com/angular/documentation/api/gantt/)
- [Gantt Chart Live Demos and Examples](https://ej2.syncfusion.com/angular/demos/#/tailwind3/gantt/drag-and-drop)
