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
                if (!TextBox1.Text.Equals("")) {
                    GridView3.DataSource = dx.student.Local.Where(s => s.studentname.Contains(TextBox1.Text)).ToList();
                    GridView3.DataBind();

                    GridView3.Visible = true;
                    GridView2.Visible = false;
                    GridView1.Visible = false;
                }


                if (DropDownList1.SelectedItem.Value != "0")
                {
                    //Task 2
                    var quer1 = dx.student.Select(stud => new { stud.studentname, stud.id })
                        .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode, gr.grade1 })
                        .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursecode })
                        .Where(c => c.coursecode == DropDownList1.SelectedItem.Value);
                   
                    GridView1.DataSource = quer1.ToList();
                    GridView1.DataBind();

                    GridView1.Visible = true;
                    GridView2.Visible = false;
                    GridView3.Visible = false;

                }

                if (DropDownList2.SelectedItem.Value != "0")
                {
                    char c = char.Parse(DropDownList2.SelectedItem.Text);
                    // Task 3
                    var query2 = dx.student.Select(stud => new { stud.studentname, stud.id })
                        .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode,gr.grade1 })
                        .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursename })
                        .Where(g => String.Compare(g.grade1, DropDownList2.SelectedItem.Text) <= 0);

                    GridView2.DataSource = query2.ToList();
                    GridView2.DataBind();
                    

                    GridView2.Visible = true;
                    GridView3.Visible = false;
                    GridView1.Visible = false;

                }
            }

            else
            {
                createDropItems();
                createGradeMenu();
                TextBox1.Text = "";
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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var query2 = dx.student.Select(stud => new { stud.studentname, stud.id })
                        .Join(dx.grade, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.coursecode, gr.grade1 })
                        .Join(dx.course, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursename })
                        .Where(g => String.Compare(g.grade1, "F") == 0);

            GridView2.DataSource = query2.ToList();
            GridView2.DataBind();


            GridView3.Visible = false;
            GridView2.Visible = true;
            GridView1.Visible = false;
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Delete")
            {
                int indeks = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridView1.Rows[indeks];
                TableCell tb = selectedRow.Cells[0];
                string studName = tb.Text;

                var que = dx.student.FirstOrDefault(s => s.studentname == studName);
                
                var queG1 = dx.grade.FirstOrDefault(g => g.studentid == que.id);
                var queG = dx.grade.FirstOrDefault(g => g.student.studentname == que.studentname);

                if (que != null)
                {

                    dx.grade.Remove(queG);

                    dx.SaveChanges();
                    GridView1.DataBind();
                    Response.Redirect(Request.Url.AbsoluteUri);
                    
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            grade g = new grade
            {
                studentid = Convert.ToInt32(TextBox2.Text),
                coursecode = TextBox3.Text,
                grade1 = TextBox4.Text
            };
            student s = new student
            {
                studentage = 20,
                studentname = "Ny",
                id = Convert.ToInt32(TextBox2.Text)
            };
            dx.student.Add(s);
            dx.grade.Add(g);
            dx.SaveChanges();
            Response.Redirect(Request.Url.AbsoluteUri);


        }
    }
}