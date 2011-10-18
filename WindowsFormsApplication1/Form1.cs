using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;

//using WindowsFormsApplication1.IMLSDataSetTableAdapters;
//using System.Data;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        static string foldername;
        static FileStream fs;
        static StreamWriter sw;
        
        update_database ud;
        

        //static string barcode, code;
        public Form1()
        {
            InitializeComponent();
            try
            {
                fs = new FileStream("c:\\program2_out.txt", FileMode.Append, FileAccess.Write, FileShare.Write);

                sw = new StreamWriter(fs);
                sw.AutoFlush = true;
                Console.SetOut(sw);
            }
            catch
            {
                Console.WriteLine("error");
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                ud = new update_database();
                foldername = this.folderBrowserDialog1.SelectedPath;
                label1.Text = foldername;
                watcher1 w1 = new watcher1(foldername,this,ud);
                Thread thread = new Thread(new ThreadStart(w1.WorkThreadFunction));
                thread.Start();
                Console.WriteLine("watcher1 started for {0}", foldername);
                Console.Out.Flush();
               
                
                

            }

        }


       protected void update(string code, string barcode)
        {

            //    .bookRow book = data.book.FindByBarcode(barcode);
            //book.Language = code;
            string[] value = { barcode, code, foldername };


           





        }

       private void toolStripMenuItem3_Click(object sender, EventArgs e)
       {
           DialogResult result = this.folderBrowserDialog1.ShowDialog();
           if (result == DialogResult.OK)
           {
               ud = new update_database();
               foldername = this.folderBrowserDialog1.SelectedPath;
               label1.Text = foldername;
               watcher3 w1 = new watcher3(foldername, this, ud);
               Thread thread = new Thread(new ThreadStart(w1.WorkThreadFunction));
               thread.Start();
               Console.WriteLine("watcher1 started for {0}", foldername);
               Console.Out.Flush();




           }
       }

       private void toolStripMenuItem1_Click(object sender, EventArgs e)
       {

       }

       private void toolStripMenuItem4_Click(object sender, EventArgs e)
       {
           Close();
           Application.Exit();
       }

       private void toolStripMenuItem2_Click(object sender, EventArgs e)
       {
           DialogResult result = this.folderBrowserDialog1.ShowDialog();
           if (result == DialogResult.OK)
           {
               ud = new update_database();
               foldername = this.folderBrowserDialog1.SelectedPath;
               label1.Text = foldername;
               watcher1 w1 = new watcher1(foldername, this, ud);
               Thread thread = new Thread(new ThreadStart(w1.WorkThreadFunction));
               thread.Start();
               Console.WriteLine("watcher1 started for {0}", foldername);
               Console.Out.Flush();




           }
       }

       private void Form1_Load(object sender, EventArgs e)
       {
           // TODO: This line of code loads data into the 'iMLSDataSet1.book' table. You can move, or remove it, as needed.
           this.bookTableAdapter1.Fill(this.iMLSDataSet1.book);
           // TODO: This line of code loads data into the 'iMLSDataSet.book' table. You can move, or remove it, as needed.
           this.bookTableAdapter.Fill(this.iMLSDataSet.book);

       }

       private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {

       }
       public void refresh()
       {
           // TODO: This line of code loads data into the 'iMLSDataSet1.book' table. You can move, or remove it, as needed.
          this.bookTableAdapter1.Fill(this.iMLSDataSet1.book);
           // TODO: This line of code loads data into the 'iMLSDataSet.book' table. You can move, or remove it, as needed.
           //this.bookTableAdapter.Fill(this.iMLSDataSet.book);
       }

       private void updateLanguageToolStripMenuItem_Click(object sender, EventArgs e)
       {
           FolderBrowserDialog fd = new FolderBrowserDialog();
           
           //fd.Description = "Select barcode";
           String barcode = "";
           DialogResult dg = fd.ShowDialog();
         if (dg == DialogResult.OK)
           {
                String path = fd.SelectedPath;
                int index = path.LastIndexOf("\\");
                //string temp = path.Substring(0, index);
                //Console.WriteLine("in set_project with temprORY substring {0}", temp);
                //Console.Out.Flush();
                int index2 = path.LastIndexOf("\\");
                String project = path.Substring(index2 + 1);
                Console.WriteLine("Project is " + project);
                Console.WriteLine("path is " +path);
                string[] filePaths = Directory.GetDirectories(@path);
                string input = Microsoft.VisualBasic.Interaction.InputBox("Language Code", "Input Language", "rom", 0, 0);
                Console.WriteLine("language code is " + input);
                update_database ud = new update_database();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    index = filePaths[i].LastIndexOf("\\");
                    barcode = filePaths[i].Substring(index + 1);
                    Console.WriteLine("doing book" + barcode + " for project " + project + " with language" + input);
                    Console.Out.Flush();
                    ud.updatebook(barcode, project, input, path);
                }
                

      

                //index=path.LastIndexOf("\\");
                //barcode = path.Substring(index + 1);
                //Console.WriteLine(barcode);
                //Console.Out.Flush();
                
                //ud.updatebook(barcode,project, "rom", path);

           }



       }

      


    }




    class watcher1
    {
        string path;
        static FileSystemWatcher fw1;
        static Form1 frm;
        static update_database ud;
        public watcher1(string path, Form1 frm,update_database ud)
        {
            this.path = path;
            watcher1.frm = frm;
            watcher1.ud = ud;

            fw1 = new FileSystemWatcher();

            fw1.Path = @path;


            fw1.EnableRaisingEvents = true;
            Console.WriteLine("in watcher1 constructor for {0}", path);
            Console.Out.Flush();
        }
        public void WorkThreadFunction()
        {
            try
            {

                //MessageBox.Show("in the constructor of new thread")
                ;            // do any background work
                //f.NotifyFilter = NotifyFilters.FileName ;
                Console.WriteLine("in watcher1 run for {0}", path);
                Console.Out.Flush();
                fw1.Created += new FileSystemEventHandler(watcher1_Created);
                //Console.WriteLine("handler registered");
                //file = new FileInfo(p);
                //Console.WriteLine("handler for file registered");
                //Console.Write(e.FullPath);
                for (; ; ) ;

            }
            catch
            {
                // log errors
            }
        }

        static void watcher1_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                string path1 = e.FullPath;
                watcher2 w2 = new watcher2(path1,frm,ud);
                Thread thread = new Thread(new ThreadStart(w2.WorkThreadFunction));
                thread.Start();
                Console.WriteLine("in watcher1 event handler, got  {0}",path1);
                Console.Out.Flush();
                //Thread thread = new Thread(new ThreadStart(filew.WorkThreadFunction));
                //thread.Start();



            }
            catch (Exception ee)
            {
                //Console.WriteLine("error in the handler for directory {0}", ee);
            }



        }
    }
    class watcher2
    {
        string Path;
        static FileSystemWatcher fw1;
        static Form1 frm;
        static update_database ud;
        public watcher2(string path,Form1 frm,update_database ud)
        {
            Console.WriteLine("in watcher2 constructor for {0}", Path);
            Console.Out.Flush();
            this.Path = path;
            watcher2.frm = frm;
            watcher2.ud = ud;

            fw1 = new FileSystemWatcher();

            fw1.Path = @path;


            fw1.EnableRaisingEvents = true;
        }
        public void WorkThreadFunction()
        {
            try
            {

                //MessageBox.Show("in the constructor of new thread")
                ;            // do any background work
                //f.NotifyFilter = NotifyFilters.FileName ;
                Console.WriteLine("in watcher2 run for {0}", Path);
                Console.Out.Flush();
                fw1.Created += new FileSystemEventHandler(watcher2_Created);
                //Console.WriteLine("handler registered");
                //file = new FileInfo(p);
                //Console.WriteLine("handler for file registered");
                //Console.Write(e.FullPath);
                for (; ; ) ;

            }
            catch
            {
                // log errors
            }
        }
        static void watcher2_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
               // fw1.EnableRaisingEvents = false;
                string path1 = e.FullPath;
                
                Console.WriteLine("in watcher2 event handler, got {0}", path1);
                Console.Out.Flush();
                int index = path1.LastIndexOf("\\");
                string barcode = path1.Substring(index + 1);
                XMLParser x = new XMLParser(path1, barcode,frm,ud);
                x.parsefile();
                
                //Thread thread = new Thread(new ThreadStart(filew.WorkThreadFunction));
                //thread.Start();



            }
            catch (Exception ee)
            {
                //Console.WriteLine("error in the handler for directory {0}", ee);
            }



        }
    }
    class watcher3
    {
        string path;
        static FileSystemWatcher fw1;
        static Form1 frm;
        static update_database ud;
        public watcher3(string path, Form1 frm, update_database ud)
        {
            this.path = path;
            watcher3.frm = frm;
            watcher3.ud = ud;

            fw1 = new FileSystemWatcher();

            fw1.Path = @path;


            fw1.EnableRaisingEvents = true;
            Console.WriteLine("in watcher1 constructor for {0}", path);
            Console.Out.Flush();
        }
        public void WorkThreadFunction()
        {
            try
            {

                //MessageBox.Show("in the constructor of new thread")
                ;            // do any background work
                //f.NotifyFilter = NotifyFilters.FileName ;
                Console.WriteLine("in watcher3 run for {0}", path);
                Console.Out.Flush();
                fw1.Created += new FileSystemEventHandler(watcher1_Created);
                //Console.WriteLine("handler registered");
                //file = new FileInfo(p);
                //Console.WriteLine("handler for file registered");
                //Console.Write(e.FullPath);
                for (; ; ) ;

            }
            catch
            {
                // log errors
            }
        }

        static void watcher1_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                string path1 = e.FullPath;
                

                Console.WriteLine("in watcher2 event handler, got {0}", path1);
                Console.Out.Flush();
                int index = path1.LastIndexOf("\\");
                string barcode = path1.Substring(index + 1);
                XMLParser x = new XMLParser(path1, barcode, frm, ud);
                x.parsefile();
                //Thread thread = new Thread(new ThreadStart(filew.WorkThreadFunction));
                //thread.Start();



            }
            catch (Exception ee)
            {
                //Console.WriteLine("error in the handler for directory {0}", ee);
            }



        }
    }
    class XMLParser
    {
        string path, barcode,project;
        Form1 frm;
        update_database ud;
        public XMLParser(string path, string barcode,Form1 frm, update_database ud)
        {
            Console.WriteLine("in XMLParser constructor for {0}", path);
            Console.Out.Flush();
            this.path = path;
            this.barcode = barcode;
            this.frm = frm;
            this.ud = ud;
            set_project(path);
        }
       public void parsefile()
        {
            Console.WriteLine("in XMLParser PARSEFILE for {0}", path);
            Console.Out.Flush();
           FileInfo f = null;
            try
            {
                Thread.Sleep(5000);

                Console.WriteLine("opening Dc.xml file at {0}", DateTime.Now.ToString("r"));
                Console.WriteLine(path + "\\METADATA\\" + barcode + "_DC.xml");
                Console.Out.Flush();
                //f = new FileInfo(path + "\\METADATA\\" + barcode + "_DC.xml");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
                Console.Out.Flush();
            }
            try
            {
                StreamReader r = new StreamReader(path + "\\METADATA\\" + barcode + "_DC.xml");
                // bool flag = false;
                string code = null, temp = null,line;
                int index1, index2;
                //string line = r.ReadLine();
                //Console.WriteLine("line is {0}",line);
                //Console.Out.Flush();
               // Console.WriteLine("Size of file is {0}", f.Length);
                //f = null;
                Console.WriteLine(r.Peek());
                
                Console.Out.Flush();
                while (r.Peek() > -1)
                {
                    Console.WriteLine("inside while loop in parseline");
                    Console.Out.Flush();
                    line = r.ReadLine();
                    Console.WriteLine(line);
                    Console.Out.Flush();
                    //readnextline
                    if (line.Contains("dc:language"))
                    {   
                        Console.WriteLine(line.StartsWith("<dc:language>"));
                        Console.WriteLine("line is {0}", line);
                        Console.Out.Flush();
                        index1 = line.IndexOf(">");
                        temp = line.Substring(index1 + 1);
                        index2 = temp.IndexOf("<");
                        code = temp.Substring(0, index2 );
                        Console.WriteLine("in parsefile the code is {0}", code);
                        Console.Out.Flush();
                        r.Close();

                        break;
                    }
                    
                }
                if (code != null)
                {
                    
                    if (code.Equals("heb") || code.Equals("jrb") || code.Equals("lad") || code.Equals("yid"))
                    {
                        code = "heb";
                        string path2 = "d:\\HEB_FIN\\" + project + "\\" + barcode;
                        ud.updatebook(barcode, project,code,path2);
                        Console.WriteLine("in XMLParser, language is {0}", code);
                        Console.Out.Flush();
                        if (!Directory.Exists(@"d:\\HEB_FIN\\" + project))
                        {
                            // Create the directory it does not exist.
                            Directory.CreateDirectory(@"d:\\HEB_FIN\\" + project);
                            //Thread.Sleep(1000);
                        }

                        
                        Directory.Move(@path, @path2);

                        // Directory.Move(path1, "d:\\Raw\\");
                    }
                    else if (code.Equals("ara") || code.Equals("bal") || code.Equals("bos") || code.Equals("hau") || code.Equals("kab") || code.Equals("kok") || code.Equals("kaz") || code.Equals("kur") || code.Equals("kir") || code.Equals("may") || code.Equals("per") || code.Equals("snd") || code.Equals("tar") || code.Equals("tur") || code.Equals("urd") || code.Equals("uig"))
                    {
                        code = "ara";
                        string path2 = "d:\\ARA_FIN\\" + project + "\\" + barcode;
                        ud.updatebook(barcode, project,code,path2);
                        Console.WriteLine("in XMLParser, language is {0}", code);
                        Console.Out.Flush();
                        if (Directory.Exists(@"d:\\ARA_FIN\\" + project)==false)
                        {
                            // Create the directory it does not exist.
                            
                            Directory.CreateDirectory(@"d:\\ARA_FIN\\" + project);
                            Console.WriteLine("directory created d:\\ARA_FIN\\" + project);
                            Console.Out.Flush();
                        }
                        
                        Console.WriteLine(" moving directory " + path + "to " + @path2);
                        Console.Out.Flush();
                        

                        Directory.Move(@path, @path2);
                        Console.WriteLine(" moved directory " + path + "to " + @path2);
                        Console.Out.Flush();
                    }
                    else
                    {
                        code = "rom";
                        ud.updatebook(barcode, project,code,path);
                        Console.WriteLine("in XMLParser, language is {0}", code);
                        Console.Out.Flush();
                    }

                }
                else
                {
                    Console.WriteLine("invalid DC xml file");
                    return;

                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);

                Console.WriteLine(e.Data);
                Console.Out.Flush();
            }
          
            // Update_Database u = new Update_Database(code, barcode);
            
            

        }
       void set_project(string path)
       {
           try
           {
               int index = path.LastIndexOf("\\");
               string temp = path.Substring(0, index );
               Console.WriteLine("in set_project with temprORY substring {0}", temp);
               Console.Out.Flush();
               int index2 = temp.LastIndexOf("\\");
               project = temp.Substring(index2 + 1);
               Console.WriteLine("in set_project with project name {0}", project);
               Console.Out.Flush();
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               Console.WriteLine(e.Message);
               Console.WriteLine(e.StackTrace);
               Console.Out.Flush();
           }
       }
    }

    class update_database
    {
        string barcode; string code;
        SqlConnection con;
        DataSet bookds,projectds;
        DataTable dt,dt1;
        SqlDataAdapter da,da1;
        SqlCommandBuilder cmdBuilder,cmdBuilder1;

        public update_database()
        {
            getconnected();
        }
        public void setbarcode(string barcode)
        {
            this.barcode=barcode;
            
        }
        public string getbarcode()
        {
            return barcode;
        }
        public void setcode(string code)
        {
            this.code = code;
        }
        public string getcode()
        {
            return code;
        }
        void getconnected()
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=GLS-KABIS\\SQLEXPRESS;Initial Catalog=IMLS;Persist Security Info=True;User ID=administrator;Password=server034techs**";
            con.Open();
            //Initialize the SqlDataAdapter object by specifying a Select command 
            //that retrieves data from the book

            da = new SqlDataAdapter("select * from book", con);
            da1 = new SqlDataAdapter("select * from project", con);
            //Initialize the SqlCommandBuilder object to automatically generate and initialize
            //the UpdateCommand, InsertCommand, and DeleteCommand properties of the SqlDataAdapter.

            cmdBuilder = new SqlCommandBuilder(da);
            cmdBuilder = new SqlCommandBuilder(da1);
            Console.WriteLine(cmdBuilder.GetUpdateCommand().CommandText);
            Console.Out.Flush();
            //Populate the DataSet by running the Fill method of the SqlDataAdapter.
            bookds = new DataSet("IMLS");
            //da.TableMappings.Add("Book");
            da.FillSchema(bookds, SchemaType.Source, "book");
            da.Fill(bookds, "Book");
            da1.FillSchema(bookds, SchemaType.Source, "project");
            da1.Fill(bookds, "Project");
            //DataColumn[] dc = new DataColumn[1];
            //dc[0]= bookds.Tables[0].Columns[1];
            //bookds.Tables[0].PrimaryKey = dc;
            DataColumn[] dc,dc1;
            dc = bookds.Tables["book"].PrimaryKey;
            dt = bookds.Tables["book"];
            dc1 = bookds.Tables["project"].PrimaryKey;
            dt1 = bookds.Tables["project"];
            


        }
        public void updatebook(string barcode,string project, string code,string path)
        {

            string path1 = path.Replace("d:", "z:");
            String[] st= new String[]{project,barcode};
            project="'"+project+"'";
            DataRow[] dr1 = bookds.Tables["project"].Select("project_name =" + project);
            int pid = (int)dr1[0].ItemArray[0];
            DataRow[] dr = bookds.Tables[0].Select("barcode = " + barcode + "AND project_id ="+pid);

            if (dr[0] != null)
            {
                Console.Out.WriteLine("new path is" + path1);
                Console.WriteLine("old path is " + path);
                Console.WriteLine(dr[0].ItemArray[0]);
                Console.WriteLine(dr[0].ItemArray[1]);
                Console.WriteLine(dr[0].ItemArray[2]);
                Console.WriteLine(dr[0].ItemArray[3]);
                Console.WriteLine(dr[0].ItemArray[4]);
                Console.WriteLine(dr[0].ItemArray[5]);
                Console.Out.Flush();
                dr[0].BeginEdit();
                dr[0]["Language"] = code;
                if (!code.Equals("rom"))
                    dr[0]["Path"] = path1;
                dr[0].EndEdit();

                DataSet ds = bookds.GetChanges(DataRowState.Modified);
                bookds.Merge(ds);
                da.Update(bookds, "book");
                da.Fill(bookds, "Book");
                //con.Close();
                Console.WriteLine("in updatebook, updated language to {0}", code);
                Console.Out.Flush();
            }
            else 
            {
                Console.WriteLine("Barcode " + barcode + " not found");
            }
        }
        

    }

}