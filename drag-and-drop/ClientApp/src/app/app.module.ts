import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

import { GanttModule, SelectionService, EditService, ToolbarService, RowDDService, } from '@syncfusion/ej2-angular-gantt';


@NgModule({

  imports: [
    BrowserModule, GanttModule,
  ],
  declarations: [AppComponent],
  providers: [SelectionService, EditService, ToolbarService, RowDDService,],
  bootstrap: [AppComponent]
})
export class AppModule { }
