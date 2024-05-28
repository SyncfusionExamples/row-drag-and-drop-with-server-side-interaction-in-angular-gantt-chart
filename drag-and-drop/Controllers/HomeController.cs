using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using System.Collections;
using Syncfusion.EJ2.Linq;
using static UrlAdaptor.Controllers.HomeController;


namespace UrlAdaptor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GanttItems.GetSelfData();
            DataOperations operation = new DataOperations();

            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting 
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering 
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<GanttItems>().Count();
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Ok(new { result = DataSource, count }) : Ok(DataSource);

        }

        //Here handle the code of row drag and drop operations
        public bool DragandDrop([FromBody] ICRUDModel value)
        {
            if (value.pos.position == "bottomSegment" || value.pos.position == "topSegment")
            {
                //for bottom and top segment drop position. If the dragged record is the only child for a particular record,
                //we need to set parentItem of dragged record to null and isParent of dragged record's parent to false 
                if (value.value.ParentId != null) // if dragged record has parent
                {
                    var childCount = 0;
                    int parent = (int)value.value.ParentId;
                    childCount += FindChildRecords(parent); // finding the number of child for dragged record's parent
                    if (childCount == 1) // if the dragged record is the only child for a particular record
                    {
                        var i = 0;
                        for (; i < GanttItems.GetSelfData().Count; i++)
                        {
                            if (GanttItems.GetSelfData()[i].TaskID == parent)
                            {
                                //set isParent of dragged record's parent to false 
                                GanttItems.GetSelfData()[i].isParent = false;
                                break;
                            }
                            if (GanttItems.GetSelfData()[i].TaskID == value.value.TaskID)
                            {
                                //set parentItem of dragged record to null
                                GanttItems.GetSelfData()[i].ParentId = null;
                                break;
                            }


                        }
                    }
                }
                GanttItems.GetSelfData().Remove(GanttItems.GetSelfData().Where(ds => ds.TaskID == value.pos.dragidMapping).FirstOrDefault());
                var j = 0;
                for (; j < GanttItems.GetSelfData().Count; j++)
                {
                    if (GanttItems.GetSelfData()[j].TaskID == value.pos.dropidMapping)
                    {
                        //set dragged records parentItem with parentItem of
                        //record in dropindex
                        value.value.ParentId = GanttItems.GetSelfData()[j].ParentId;
                        break;
                    }
                }
                if (value.pos.position == "bottomSegment")
                {
                    this.Insert(value, value.pos.dropidMapping);
                }
                else if (value.pos.position == "topSegment")
                {
                    this.InsertAtTop(value, value.pos.dropidMapping);
                }
            }
            else if (value.pos.position == "middleSegment")
            {
                GanttItems.GetSelfData().Remove(GanttItems.GetSelfData().Where(ds => ds.TaskID == value.pos.dragidMapping).FirstOrDefault());
                value.value.ParentId = value.pos.dropidMapping;
                FindDropdata(value.pos.dropidMapping);
                this.Insert(value, value.pos.dropidMapping);
            }
            return true;
        }

        public ActionResult Update([FromBody] ICRUDModel value)
        {
            if (value != null)
            {
                var val = GanttItems.GetSelfData().Where(ds => ds.TaskID == value.value.TaskID).FirstOrDefault();
                val.TaskName = value.value.TaskName;
                val.Duration = value.value.Duration;
                return Json(val);
            }
            else return Json(null);

        }

        public ActionResult Insert([FromBody] ICRUDModel value, int rowIndex)
        {
            var i = 0;
            if (value.Action == "insert")
            {
                rowIndex = value.relationalKey;
            }
            Random ran = new Random();
            int a = ran.Next(100, 1000);

            for (; i < GanttItems.GetSelfData().Count; i++)
            {
                if (GanttItems.GetSelfData()[i].TaskID == rowIndex)
                {
                    value.value.ParentId = rowIndex;
                    if (GanttItems.GetSelfData()[i].isParent == false)
                    {
                        GanttItems.GetSelfData()[i].isParent = true;
                    }
                    break;

                }
            }
            i += FindChildRecords(rowIndex);
            GanttItems.GetSelfData().Insert(i, value.value);

            return Json(value.value);
        }

        public void InsertAtTop([FromBody] ICRUDModel value, int rowIndex)
        {
            var i = 0;
            for (; i < GanttItems.GetSelfData().Count; i++)
            {
                if (GanttItems.GetSelfData()[i].TaskID == rowIndex)
                {
                    break;

                }
            }
            i += FindChildRecords(rowIndex);
            GanttItems.GetSelfData().Insert(i - 1, value.value);
        }

        public void FindDropdata(int key)
        {
            var i = 0;
            for (; i < GanttItems.GetSelfData().Count; i++)
            {
                if (GanttItems.GetSelfData()[i].TaskID == key)
                {
                    GanttItems.GetSelfData()[i].isParent = true;
                }
            }
        }

        public int FindChildRecords(int? id)
        {
            var count = 0;
            for (var i = 0; i < GanttItems.GetSelfData().Count; i++)
            {
                if (GanttItems.GetSelfData()[i].ParentId == id)
                {
                    count++;
                    count += FindChildRecords(GanttItems.GetSelfData()[i].TaskID);
                }
            }
            return count;
        }
        public void Remove([FromBody] ICRUDModel value)
        {
            if (value.Key != null)
            {
                // GanttItems value = key;
                GanttItems.GetSelfData().Remove(GanttItems.GetSelfData().Where(ds => ds.TaskID == double.Parse(value.Key.ToString())).FirstOrDefault());
            }

        }

        public class CustomBind : GanttItems
        {
            public GanttItems ParentId;
        }

        public class ICRUDModel
        {
            public GanttItems value;

            public GanttData pos;
            public int relationalKey { get; set; }
            public object Key { get; set; }

            public string Action { get; set; }
        }
        public class GanttData
        {
            public int dragidMapping { get; set; }
            public int dropidMapping { get; set; }
            public string position { get; set; }
        }
        public class GanttClass : GanttItems
        {
            public GanttItems taskData { get; set; }
        }
    }
    public class GanttItems
    {
        public GanttItems() { }
        public int TaskID { get; set; }



        public string TaskName { get; set; }
        public int Duration { get; set; }
        public int? ParentId { get; set; }
        public bool? isParent { get; set; }


        public static List<GanttItems> BusinessObjectCollection = new List<GanttItems>();



        public static List<GanttItems> GetSelfData()
        {
            if (BusinessObjectCollection.Count == 0)
            {
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 1,
                    TaskName = "Parent Task 1",
                    Duration = 10,
                    ParentId = null,
                    isParent = true
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 2,
                    TaskName = "Child task 1",
                    Duration = 4,
                    ParentId = 1,
                    isParent = false
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 3,
                    TaskName = "Child task 2",
                    Duration = 8,
                    ParentId = 1,
                    isParent = false
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 4,
                    TaskName = "Child task 3",
                    Duration = 10,
                    ParentId = 1,
                    isParent = false
                });


                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 5,
                    TaskName = "Parent Task 2",
                    Duration = 10,
                    ParentId = null,
                    isParent = true
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 6,
                    TaskName = "Child task 4",
                    Duration = 4,
                    ParentId = 5,
                    isParent = false
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 7,
                    TaskName = "Child task 5",
                    Duration = 4,
                    ParentId = 5,
                    isParent = false
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 8,
                    TaskName = "Child task 6",
                    Duration = 4,
                    ParentId = 5,
                    isParent = false
                });
                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 9,
                    TaskName = "Child task 7",
                    Duration = 4,
                    ParentId = 5,
                    isParent = false
                });



                BusinessObjectCollection.Add(new GanttItems()
                {
                    TaskID = 10,
                    TaskName = "Child Task 8",
                    Duration = 10,
                    ParentId = 5,
                    isParent = false
                });

            }

            return BusinessObjectCollection;
        }
    }
}