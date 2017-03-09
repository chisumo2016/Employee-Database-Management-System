using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Employee_Database
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            System.Data.SqlClient.SqlConnection con;
            System.Data.SqlClient.SqlDataAdapter da;
            DataSet dsl;


        }

        int MaxRows = 0;
        int inc = 0;

        DataSet dsl = new DataSet();

    

      private void Form1_Load(object sender, EventArgs e)
        {

            //To call the function ObtainRecords
            ObtainRecords();
            // This is the field to be appeared in Search combox
            cmbSearch.Items.Add("Worker_ID");
            
            cmbSearch.Items.Add("Last_Name");
            cmbSearch.Items.Add("Age");
            cmbSearch.Items.Add("Job_Title");
            cmbSearch.Items.Add("Department");
            cmbSearch.Items.Add("Salay");

          // This is the Field to appear in the FilterCombox
            cmbFilter.Items.Add("First_Name");
            cmbFilter.Items.Add("Last_Name");
            cmbFilter.Items.Add("Age");
            cmbFilter.Items.Add("Job_Title");
            cmbFilter.Items.Add("Department");
            cmbFilter.Items.Add("Salary");

        }
        private void ObtainRecords()
        {


            // Function to obtain the record from database 
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();

            DataSet dsl = new DataSet();
            con = new System.Data.SqlClient.SqlConnection();
            dsl = new DataSet();
            // Accessing data from dataset

            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\user\\Desktop\\MyWorkers.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";


            con.Open();
            string sql = "SELECT*From tblWorkers";
            da = new System.Data.SqlClient.SqlDataAdapter(sql, con);


            // To Fill the dataset with records
            da.Fill(dsl, "Workers");

            //Dispaly the record fields(columns in Row
            MaxRows = dsl.Tables["Workers"].Rows.Count;
            DataRow dRow = dsl.Tables["Workers"].Rows[inc];
            txtWorkerID.Text = dRow.ItemArray.GetValue(0).ToString();
            txtFirstName.Text = dRow.ItemArray.GetValue(1).ToString();

            textBox1.Text = dRow.ItemArray.GetValue(2).ToString();
            txtAge.Text = dRow.ItemArray.GetValue(3).ToString();
            txtJobTitle.Text = dRow.ItemArray.GetValue(4).ToString();
            txtDepartment.Text = dRow.ItemArray.GetValue(5).ToString();
            txtSalary.Text = dRow.ItemArray.GetValue(6).ToString();
            con.Close();
            con.Dispose();

        }
        private void UpdateDB()
        {
            // This is the function to call the method UpdateDB()

            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dsl = new DataSet();
            dsl = new DataSet();

            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\user\\Desktop\\MyWorkers.mdf;Integrated Security=True;User Instance=True";
            string sql = "SELECT*From tblWorkers";
            da = new System.Data.SqlClient.SqlDataAdapter(sql, con);
            da.Fill(dsl, "Workers");
            da.Update(dsl, "Workers");
            System.Data.SqlClient.SqlCommandBuilder cb;
            cb = new System.Data.SqlClient.SqlCommandBuilder(da);
            cb.DataAdapter.Update(dsl.Tables["Workers"]);
            //MaxRows = MaxRows + 1;
            inc = MaxRows - 1;
            DataRow dRow = dsl.Tables["Workers"].Rows[inc];
            dRow[0] = txtWorkerID.Text;
            dRow[1] = txtFirstName.Text;
            dRow[2] = textBox1.Text;
            dRow[3] = txtAge.Text;
            dRow[4] = txtJobTitle.Text;
            dRow[5] = txtDepartment.Text;
            dRow[6] = txtSalary.Text;
        }
        private void SaveRecord()
        {
            // This is the function to call the method Save()
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dsl = new DataSet();
            con = new System.Data.SqlClient.SqlConnection();
            dsl = new DataSet();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\user\\Desktop\\MyWorkers.mdf;Integrated Security=True;User Instance=True";
            con.Open();
            string sql = "SELECT*From tblWorkers";
            da = new System.Data.SqlClient.SqlDataAdapter(sql, con);
            da.Fill(dsl, "Workers"); //Fill the dataset with records
            System.Data.SqlClient.SqlCommandBuilder cb;
            cb = new System.Data.SqlClient.SqlCommandBuilder(da);
            DataRow dRow = dsl.Tables["Workers"].NewRow();
            dRow[0] = txtWorkerID.Text;
            dRow[1] = txtFirstName.Text;
            dRow[2] = textBox1.Text;
            dRow[3] = txtAge.Text;
            dRow[4] = txtJobTitle.Text;
            dRow[5] = txtDepartment.Text;
            dRow[6] = txtSalary.Text;

            dsl.Tables["Workers"].Rows.Add(dRow);
            MaxRows = MaxRows + 1;
            inc = MaxRows - 1;
            da.Update(dsl, "Workers");
            UpdateDB();

        }

        private void search()
        {


            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dsl = new DataSet();
            con = new System.Data.SqlClient.SqlConnection();
            dsl = new DataSet();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\user\\Desktop\\MyWorkers.mdf;Integrated Security=True;User Instance=True";
            con.Open();
            string sql = "SELECT*From tblWorkers";
            da = new System.Data.SqlClient.SqlDataAdapter(sql, con);
            da.Fill(dsl, "Workers");

            string searchFor = txtSearch.Text;
            string searchOn = "";
            string searchString = "";
            int results = 0;

            if (txtSearch.Text.Trim() == "")
            {
                MessageBox.Show("Nothing to search For");
                return;
            }

            if (cmbSearch.Text == "Worker ID")
            {

                searchFor = txtSearch.Text.Trim();
                searchOn = "Worker_ID =";
                searchString = searchOn + "'" + searchFor + "'";
            }
            

            if (cmbSearch.Text == "Last_Name")
            {
                searchFor = txtSearch.Text.Trim();
                searchOn = "Last_Name=";
                searchString = searchOn + "'" + searchFor + "'";
            }
            if(cmbSearch.Text=="Age")
            {
                searchFor = txtSearch.Text.Trim();
                searchOn = "Age=";
                searchString = searchOn + "'" + searchFor + "'";
            }
            if(cmbSearch.Text=="Job Title")
            {
                searchFor = txtSearch.Text.Trim();
                searchOn = "Job_Title=";
                searchString = searchOn + "'" + searchFor + "'";
            }
            if (cmbSearch.Text=="Department")
            {
                searchFor = txtSearch.Text.Trim();
                searchOn = "Department=";
                searchString = searchOn + "'" + searchFor + "'";
            }
            if (cmbSearch.Text=="Salary")
            {
                searchFor = txtSearch.Text.Trim();
                searchOn = "Salary=";
                searchString = searchOn + "'" + searchFor + "'";

            }
            DataRow[] returnedRows;

            returnedRows = dsl.Tables["Workers"].Select(searchString);

            results = returnedRows.Length;
            if (results > 0)
            {
                string searchMessage = "";
                DataRow dr1;
                dr1 = returnedRows[0];
                searchMessage += " Worker ID:" + dr1["Worker_ID"].ToString();
              
                searchMessage +="Last Name:" + dr1["Last_Name"].ToString();
                searchMessage += "Job Title: " + dr1["Job_Title"].ToString();
                searchMessage += "Department: " + dr1["Department"].ToString();
                searchMessage += "Salary: " + dr1["Salary"].ToString();
                MessageBox.Show(searchMessage);
            }
            else
            {
                MessageBox.Show("No such Record");
            }
        }


        private void FilterRecord()
        {

            string Message="";
            // Function to obtain the record from dataset
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dsl = new DataSet();
            con = new System.Data.SqlClient.SqlConnection();
            dsl = new DataSet();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\user\\Desktop\\MyWorkers.mdf;Integrated Security=True;User Instance=True";
            con.Open();
            string sql = "SELECT*From tblWorkers";
            da = new System.Data.SqlClient.SqlDataAdapter(sql, con);
            
            // Fill the dataset with recors
            da.Fill(dsl, "Workers");

            //Fill the dataset with records
             string filterCombo=cmbFilter.Text;
            
            //Text from from text box for filter
             string filterFor=txtFilter.Text;

            int results=0;
            DataRow[] returnedRows;

            // Nested if statement for checking the value selected in the combobox and uses the selected value  to produce a sql satement for database
            if (cmbFilter.Text=="First Name")
            {
                filterCombo="First_ Name='";
            }
            else if (cmbFilter.Text=="Last Name")
            {
                filterCombo="Last_Name='";
            }
            else if(cmbFilter.Text=="Age")
            {
                filterCombo="Age='";
            }
            else if(cmbFilter.Text=="Job Title")
            {
                filterCombo="Job_Title='";
            }
            else if (cmbFilter.Text=="Department")
            {
                filterCombo="Department='";
            }
            else if (cmbFilter.Text=="Salary")
            {
                filterCombo="Salary='";
            }
            // Sql satement uses value from filtercombo and text from filferFor

            returnedRows=dsl.Tables["Workers"].Select(filterCombo + filterFor+"'");
            results=returnedRows.Length;

            //For Loop to find all values into a single string
            for(int i=0; i<returnedRows.Length; i++)
            {
                // To put all of the values into a single string
                Message = Message + "\n" + "First Name: " + returnedRows[i]["First Name"].ToString() + "\n" +
                    
                    
                    "Last Name:"+returnedRows[i]["Last_Name"].ToString()+"\n"+
                    "Age:"+returnedRows[i]["Age"].ToString()+"\n"+
                    "Job Tilte:"+returnedRows[i]["Job_Title"].ToString()+"\n"+
                    "Department:"+returnedRows[i]["Department"].ToString()+"\n"+
                    "Salary:"+returnedRows[i]["Salary"].ToString()+"\n";
            }
            if (results>0)
            {
                // Print the message BOX
                MessageBox.Show("The key word usde is :"+txtFilter.Text+"\n"+Message,"Workers");
            }
            else
            {
                MessageBox.Show("No Such Record");
            }

               
        


        }
        private void Delete()
        {



            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dsl = new DataSet();
            con = new System.Data.SqlClient.SqlConnection();
            dsl = new DataSet();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\user\\Desktop\\MyWorkers.mdf;Integrated Security=True;User Instance=True";
            //con.Open();
            string sql = "SELECT*From tblWorkers";
            da = new System.Data.SqlClient.SqlDataAdapter(sql, con);

            // Fill the dataset with recors
            da.Fill(dsl, "Workers");
            System.Data.SqlClient.SqlCommandBuilder cb;
            cb = new System.Data.SqlClient.SqlCommandBuilder(da);
            dsl.Tables["Workers"].Rows[inc].Delete();
            MaxRows--;
            inc = 0;
            da.Update(dsl, "Workers");
        
        }




        private void btnFirstRecord_Click(object sender, EventArgs e)
        {
                if (inc != 0)
                 {
                inc = 0;
                ObtainRecords();
                    }
                else
                {
                MessageBox.Show("First Record");
                }
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {
             if (inc != MaxRows - 1)
            {
                inc++;
                ObtainRecords();
            }
            else
            {
                MessageBox.Show("No more records ! ");
            }
        }

        private void btnPreviousRecord_Click(object sender, EventArgs e)
        {
            if (inc > 0)
            {
                inc--;
                ObtainRecords();
            }
            else
            {
                MessageBox.Show("First Record");
            }
        
        }

        private void btnLastRecord_Click(object sender, EventArgs e)
        {
           if (inc != MaxRows - 1)
            {
                inc = MaxRows - 1;
                ObtainRecords();
            }
            else
            {
                MessageBox.Show("This is the Last Record");
            }
        
        }


        private void btnNewRecord_Click(object sender, EventArgs e)
        {
            
            // This is the code to clear the text boxes
            txtWorkerID.Clear();
            txtFirstName.Clear();
            textBox1.Clear();
            txtAge.Clear();
            txtJobTitle.Clear();
            txtDepartment.Clear();
            txtSalary.Clear();

            // User clicks the Add New Record, the Add New will be disabled
            btnNewRecord.Enabled = false;
          
            btnSaveRecord.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            SaveRecord();
            MessageBox.Show("Entry Added");
            btnNewRecord.Enabled = true;
            btnSaveRecord.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        DialogResult cancel = new DialogResult();
            cancel = MessageBox.Show("Are you sure?", "?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (cancel == DialogResult.OK)
            {
                this.Close();
            
        }
        }

        private void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            UpdateDB();

            MessageBox.Show("Record  Updated");
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {

            DialogResult dlgResults;
            dlgResults = MessageBox.Show(
                                       "Are you sure?",
                                       "Delete Record",
                                       MessageBoxButtons.YesNoCancel,
                                       MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button1);
            if (dlgResults == DialogResult.Yes)
            {
                Delete();
                MessageBox.Show("Record Deleted");
            }


        

        }

        private void btnSearchRecord_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterRecord();
        }

        private void btnPrintRecord_Click(object sender, EventArgs e)
        {
            MessageBox.Show("------------------------------" + "\nYour current record is:" + 
                "\n------------------------------" + "\n\n" +
        "WorkerID: " + "\t" + txtWorkerID.Text + 
        "\nFirst Name: " + "\t" + txtFirstName.Text +
        "\nLast Name: " + "\t" + textBox1.Text + 
        "\nAge:" + "\t\t" + txtAge.Text 
         + "\nJob_Title:" + "\t\t" + txtJobTitle.Text +
        "\nDepartment:" + "\t" + txtDepartment.Text + 
        "\nSalary: " + "\t\t" + txtSalary.Text

        , "Record Printout:");
        
    }

        }
}
