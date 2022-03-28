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


                if (DropDownList1.SelectedItem.Value != "0")
                {
                    //Task 2
                    var quer1 = dx.student.Select(stud => new { stud.studentname, stud.id })
                        .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode, gr.grade1 })
                        .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursecode })
                        .Where(c => c.coursecode == DropDownList1.SelectedItem.Value);
                   
                    GridView1.DataSource = quer1.ToList();
                    GridView1.DataBind();
                }
                //Berre for å sjekke ka value'n va
                //TextBox1.Text = DropDownList1.SelectedItem.Value;
                if (DropDownList2.SelectedItem.Value != "0")
                {
                    char c = char.Parse(DropDownList2.SelectedItem.Text);
                    // Task 3
                    var query2 = dx.student.Select(stud => new { stud.studentname, stud.id })
                        .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode,gr.grade1 })
                        .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursename })
                        //.Where(g => g.grade1.CompareTo(DropDownList2.SelectedItem.Text) >= 0);
                        .Where(g => String.Compare(g.grade1, DropDownList2.SelectedItem.Text) <= 0);
                    
                    GridView2.DataSource = query2.ToList();
                    GridView2.DataBind();
                }
                
                Button1.Click += new EventHandler(this.Button1_Click);
                //EventHandler EventHandler = new EventHandler(GridView1_SelectedIndexChanged);

            }

            else
            {
                createDropItems();
                createGradeMenu();
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
        private void createGradeMenu()
        {
            DbSet<grade> gr = dx.grade;
            DropDownList2.Items.Add( new ListItem("A"));
            DropDownList2.Items.Add(new ListItem("B"));
            DropDownList2.Items.Add(new ListItem("C"));
            DropDownList2.Items.Add(new ListItem("D"));
            DropDownList2.Items.Add(new ListItem("E"));
            DropDownList2.Items.Add(new ListItem("F"));
            //foreach (grade g in gr)
            //{
            //    DropDownList2.Items.Add(new ListItem(g.grade1, g.coursecode));
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var query2 = dx.student.Select(stud => new { stud.studentname, stud.id })
                        .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode, gr.grade1 })
                        .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursename })
                        //.Where(g => g.grade1.CompareTo(DropDownList2.SelectedItem.Text) >= 0);
                        .Where(g => String.Compare(g.grade1, "F") == 0);

            GridView2.DataSource = query2.ToList();
            GridView2.DataBind();
        }
    }
}