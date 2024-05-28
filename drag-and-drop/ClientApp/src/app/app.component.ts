
import { Component, ViewChild } from '@angular/core';
import { GanttComponent, ToolbarItem, EditSettingsModel, } from '@syncfusion/ej2-angular-gantt';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import { Ajax } from '@syncfusion/ej2-base';


@Component({
  selector: 'app-root',
  template: `
    <ejs-gantt #gantt [dataSource]='data' [treeColumnIndex]='1' (rowDrop)="rowDrop($event)" 
    [taskFields]="taskSettings" [splitterSettings] = "splitterSettings" [allowRowDragAndDrop]=true [editSettings]="editSettings" [toolbar]="toolbar" height="450">
        <e-columns>
            <e-column field='TaskID' headerText='Task ID' [isPrimaryKey]='true' width='150'></e-column>
            <e-column field='TaskName' headerText='Task Name' width='150'></e-column>
            <e-column field='Duration' headerText='Duration' width='150' textAlign='Right'></e-column>
        </e-columns>
    </ejs-gantt>`
})
export class AppComponent {
  public data?: DataManager;
  public editSettings?: EditSettingsModel;
  public toolbar?: ToolbarItem[];
  @ViewChild('gantt')
  public gantt?: GanttComponent
  public taskSettings?: object;
  public splitterSettings?: object;

  ngOnInit(): void {
    this.data = new DataManager({
      url: '/Home/UrlDatasource',
      adaptor: new UrlAdaptor(),
      offline: true
    });
    this.taskSettings = {
      id: 'TaskID',
      name: 'TaskName',
      startDate: 'StartDate',
      duration: 'Duration',
      child: 'subtasks',
      parentID: 'ParentId',
    };
    this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, };
    this.toolbar = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search'];
    this.splitterSettings = {
      position: '75%'
    };

  }
  public rowDrop(args: any) {
    var drag_idmapping = (this.gantt as any).currentViewData[args.fromIndex][(this.gantt as any).taskFields.id]
    var drop_idmapping = (this.gantt as any).currentViewData[args.dropIndex][(this.gantt as any).taskFields.id]
    var data = args.data[0];
    var positions = { dragidMapping: drag_idmapping, dropidMapping: drop_idmapping, position: args.dropPosition };
    const ajax = new Ajax({
      url: '/Home/DragandDrop',
      type: 'POST',
      dataType: "json",
      contentType: 'application/json; charset=utf-8',
      data: JSON.stringify({ value: data, pos: positions })
    });
    (this.gantt as any).showSpinner();
    ajax.send();
    ajax.onSuccess = (data: string) => {
      (this.gantt as any).hideSpinner();
    };
  }
}
