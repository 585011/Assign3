using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assign3
{
    public partial class About : Page
    {
        dat154Entities dx = new dat154Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            dx.student.Load();
            

            if (IsPostBack)
            {

                // Task 1
                GridView1.DataSource = dx.student.Local.Where(s => s.studentname.Contains(TextBox1.Text)).ToList();
                GridView1.DataBind();

                
                //Task 2
                var query = dx.course
                    
                                        .Join(dx.grade, course => course.coursecode, grade => grade.coursecode, (course, grade) => new { course.coursecode, grade.studentid })
                                        .Join(dx.student, item => item.studentid, stud => stud.id, (item, stud) => new { stud.studentname, stud.grade, item.coursecode })
                                        .Where(c => c.coursecode == DropDownList1.SelectedItem.Value)
                                        .OrderBy(c => c.studentname);

                var quer1 = dx.student.Select(stud => new { stud.studentname, stud.id })
                    .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode, gr.grade1 })
                    .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursecode })
                    .Where(c => c.coursecode == DropDownList1.SelectedItem.Value);
                var que = "SELECT studentname, grade FROM dbo.course JOIN grade ON grade" +
                    " JOIN course ON course.coursecode = grade.coursecode JOINS student AS ins ON coursecode = ins.coursecode WHERE ins.coursecode = " + DropDownList1.SelectedItem.Value;
                GridView1.DataSource = quer1.ToList();
                GridView1.DataBind();
                //Berre for å sjekke ka value'n va
                TextBox1.Text = DropDownList1.SelectedItem.Value;

            }

            else
            {
                createDropItems();
                TextBox1.Text = "Search for student";
            }
        }

        private void createDropItems()
        {
            DbSet<course> ds = dx.course;
            foreach(course c in ds)
            {
                DropDownList1.Items.Add(new ListItem(c.coursename, c.coursecode));
            }
        }
    }
}